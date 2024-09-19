using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SkakSpil
{
    public partial class MainWindow : Window //version 1.4
    {
        private readonly Dictionary<string, ImageSource> imageSources = new Dictionary<string, ImageSource>
        {
            { "Bishop1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/bishop_white.png", UriKind.Relative)) },
            { "Horse1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/knight_white.png", UriKind.Relative)) },
            { "King1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/king_white.png", UriKind.Relative)) },
            { "Queen1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/queen_white.png", UriKind.Relative)) },
            { "Rock1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/rook_white.png", UriKind.Relative)) },
            { "Bishop2", new BitmapImage(new Uri(@"Assets/ChessP/Team White/bishop_white.png", UriKind.Relative)) },
            { "Horse2", new BitmapImage(new Uri(@"Assets/ChessP/Team White/knight_white.png", UriKind.Relative)) },
            { "Rock2", new BitmapImage(new Uri(@"Assets/ChessP/Team White/rook_white.png", UriKind.Relative)) },
            { "Pawn1", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn2", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn3", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn4", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn5", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn6", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn7", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn8", new BitmapImage(new Uri(@"Assets/ChessP/Team White/pawn_white.png", UriKind.Relative)) },
            { "Pawn9", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn10", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn11", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn12", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn13", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn14", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn15", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Pawn16", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/pawn_black.png", UriKind.Relative)) },
            { "Bishop3", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/bishop_black.png", UriKind.Relative)) },
            { "Bishop4", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/bishop_black.png", UriKind.Relative)) },
            { "Horse3", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/knight_black.png", UriKind.Relative)) },
            { "Horse4", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/knight_black.png", UriKind.Relative)) },
            { "King2", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/king_black.png", UriKind.Relative)) },
            { "Queen2", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/queen_black.png", UriKind.Relative)) },
            { "Rock3", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/rook_black.png", UriKind.Relative)) },
            { "Rock4", new BitmapImage(new Uri(@"Assets/ChessP/Team Black/rook_black.png", UriKind.Relative)) }
        };

        private readonly Dictionary<string, (int row, int col)> initialPositions = new Dictionary<string, (int row, int col)>
        {
            { "Bishop1", (0, 2) },
            { "Horse1", (0, 1) },
            { "King1", (0, 4) },
            { "Queen1", (0, 3) },
            { "Rock1", (0, 0) },
            { "Bishop2", (0, 5) },
            { "Horse2", (0, 6) },
            { "Rock2", (0, 7) },
            { "Pawn1", (1, 0) },
            { "Pawn2", (1, 1) },
            { "Pawn3", (1, 2) },
            { "Pawn4", (1, 3) },
            { "Pawn5", (1, 4) },
            { "Pawn6", (1, 5) },
            { "Pawn7", (1, 6) },
            { "Pawn8", (1, 7) },
            { "Pawn9", (6, 0) },
            { "Pawn10", (6, 1) },
            { "Pawn11", (6, 2) },
            { "Pawn12", (6, 3) },
            { "Pawn13", (6, 4) },
            { "Pawn14", (6, 5) },
            { "Pawn15", (6, 6) },
            { "Pawn16", (6, 7) },
            { "Bishop3", (7, 2) },
            { "Bishop4", (7, 5) },
            { "Horse3", (7, 1) },
            { "Horse4", (7, 6) },
            { "King2", (7, 4) },
            { "Queen2", (7, 3) },
            { "Rock3", (7, 0) },
            { "Rock4", (7, 7) }
        };

        private Rectangle highlightedRectangle;
        private Image selectedPiece;
        private int selectedRow, selectedColumn;

        public MainWindow()
        {
            InitializeComponent();
            CreateChessBoard();
        } 

        private void CreateChessBoard()
        {
            // defines custom green color
            SolidColorBrush customDarkGreen = new SolidColorBrush(Color.FromRgb(0x56, 0x8D, 0x38));
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = (row + col) % 2 == 0 ? Brushes.White : customDarkGreen;
                    rect.StrokeThickness = 2;
                    rect.Stroke = Brushes.Transparent;
                    Grid.SetRow(rect, row);
                    Grid.SetColumn(rect, col);
                    MainGrid.Children.Add(rect);
                }
            }

            foreach (var piece in initialPositions)
            {
                Image pieceImage = new Image
                {
                    Source = imageSources[piece.Key],
                    Width = 60,
                    Height = 60,
                    Tag = piece.Key
                };
                Grid.SetRow(pieceImage, piece.Value.row);
                Grid.SetColumn(pieceImage, piece.Value.col);
                MainGrid.Children.Add(pieceImage);
            }
        }

       
        private void RestartApplication()
        {
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            Process.Start(appPath);
            Application.Current.Shutdown();
        }
   


        private void PlayAgain2(object sender, RoutedEventArgs e)
        {
            RestartApplication();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(MainGrid);
            int row = (int)(mousePosition.Y / (MainGrid.ActualHeight / MainGrid.RowDefinitions.Count));
            int column = (int)(mousePosition.X / (MainGrid.ActualWidth / MainGrid.ColumnDefinitions.Count));

            
            if (selectedPiece == null)
            {
                SelectPiece(row, column);
            }
            
            else
            {
                MovePiece(row, column);
            }
        }

        private void SelectPiece(int row, int column)
        {
            // Find the piece at the selected position
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image image && Grid.GetRow(image) == row && Grid.GetColumn(image) == column)
                {
                    string Selected = image.Tag.ToString().ToLower();
                    if (whiteorblack(Selected) == "White" && Player_turn == false || whiteorblack(Selected) == "Black" && Player_turn == true)
                    {
                        selectedPiece = image;
                        selectedRow = row;
                        selectedColumn = column;
                        HighlightGridCell(row, column);
                        break;

                    }
                }
            }
        }

        private string whiteorblack(string Selected)
        {
            switch (Selected)
            {
                case "pawn1":
                case "pawn2":
                case "pawn3":
                case "pawn4":
                case "pawn5":
                case "pawn6":
                case "pawn7":
                case "pawn8":
                case "rock1":
                case "rock2":
                case "horse1":
                case "horse2":
                case "bishop1":
                case "bishop2":
                case "queen1":
                case "king1":
                    return "White";

                case "pawn9":
                case "pawn10":
                case "pawn11":
                case "pawn12":
                case "pawn13":
                case "pawn14":
                case "pawn15":
                case "pawn16":
                case "rock3":
                case "rock4":
                case "horse3":
                case "horse4":
                case "bishop3":
                case "bishop4":
                case "queen2":
                case "king2":
                    return "Black";
            }
            return "Error";
        }

        private Boolean Player_turn = false;
        private void Change_turn()
        {
            if (Player_turn == true)
            {
                Player_turn = false;
            }
            else if (Player_turn == false)
            {
                Player_turn = true;
            }
        }
        private void MovePiece(int row, int column)
        {


            // Check if the move is valid
            if (IsValidMove(selectedPiece, selectedRow, selectedColumn, row, column))
            {
                System.Diagnostics.Debug.WriteLine("hey33");


                CapturePieceAt(row, column);
                Change_turn();
                Grid.SetRow(selectedPiece, row);

                Grid.SetColumn(selectedPiece, column);

            }

            // Deselect the piece
            selectedPiece = null;
            highlightedRectangle.Stroke = Brushes.Transparent;
            highlightedRectangle = null;
        }

        private void CapturePieceAt(int row, int column)
        {
            // Create a list to store the elements to be removed
            List<UIElement> elementsToRemove = new List<UIElement>();

            
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image image && Grid.GetRow(image) == row && Grid.GetColumn(image) == column)
                { 
                    elementsToRemove.Add(image); // Add the element to the list to be removed later

                    if (image.Tag.ToString() == "King1")
                    {
                        Win_logic("Black");
                    }
                    else if (image.Tag.ToString() == "King2")
                    {
                        Win_logic("White");
                    }
                }
            }

           
            foreach (UIElement element in elementsToRemove)
            {
                MainGrid.Children.Remove(element);
            }
        }

        private void Win_logic(String Winner)
        {
            EndGamePanel.Visibility = Visibility.Visible;
            TextBlock Text = (TextBlock)one.FindName("WinnerColor");
            if (Winner == "Black")
            {
                Text.Text = "Black just better";
            }
            if (Winner == "White")
            {
                Text.Text = "White just better";
            }
        }

        private bool IsValidMove(Image piece, int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (fromRow == toRow && fromColumn == toColumn)
            {
                return false;
            }
            string pieceType = piece.Tag.ToString().ToLower();
            System.Diagnostics.Debug.WriteLine("This is for sure");
            switch (pieceType)
            {
                case "pawn1":
                case "pawn2":
                case "pawn3":
                case "pawn4":
                case "pawn5":
                case "pawn6":
                case "pawn7":
                case "pawn8":
                    return IsValidPawnMove(fromRow, fromColumn, toRow, toColumn);
                case "pawn9":
                case "pawn10":
                case "pawn11":
                case "pawn12":
                case "pawn13":
                case "pawn14":
                case "pawn15":
                case "pawn16":
                    return IsValidPawnBlack(fromRow, fromColumn, toRow, toColumn);
                case "rock1":
                case "rock2":
                    return IsValidRookWhite(fromRow, fromColumn, toRow, toColumn);
                case "rock3":
                case "rock4":
                    return IsValidRookBlack(fromRow, fromColumn, toRow, toColumn);
                case "horse1":
                case "horse2":
                    return IsValidKnightWhite(fromRow, fromColumn, toRow, toColumn);
                case "horse3":
                case "horse4":
                    return IsValidKnightBlack(fromRow, fromColumn, toRow, toColumn);
                case "bishop1":
                case "bishop2":
                    return IsValidBishopWhite(fromRow, fromColumn, toRow, toColumn);
                case "bishop3":
                case "bishop4":
                    return IsValidBishopBlack(fromRow, fromColumn, toRow, toColumn);
                case "queen1":
                    return IsValidQueenWhite(fromRow, fromColumn, toRow, toColumn);
                case "queen2":
                    return IsValidQueenBlack(fromRow, fromColumn, toRow, toColumn);
                case "king1":
                    return IsValidKingWhite(fromRow, fromColumn, toRow, toColumn);
                case "king2":
                    return IsValidKingBlack(fromRow, fromColumn, toRow, toColumn);
            }
            return false;
        }
        #region 

        private bool IsValidPawnBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsBlackPieceAt(toRow, toColumn))
            {
                return false;
            }

            if (IsWhitePieceAt2(toRow, toColumn))
            {
                if ((fromRow - toRow) == 1 && 1 == Math.Abs(fromColumn - toColumn))  //Kill
                {
                    return true;
                }

            }
            if (fromRow - toRow == 1 && fromRow > toRow && fromColumn == toColumn) //standard forward
            {
                if (fromColumn == toColumn && (IsWhitePieceAt2(toRow, toColumn) == true))
                {
                    return false;
                }
                return true;
            }
            if ((fromRow > toRow) && (fromRow - toRow) == 2 && (fromRow == 6) && (toColumn == fromColumn)) // initial sprint
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        private bool IsValidPawnMove(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsWhitePieceAt2(toRow, toColumn))
            {
                return false;
            }
            if (IsBlackPieceAt(toRow, toColumn))
            {
                if ((fromRow - toRow) == -1 && 1 == Math.Abs(fromColumn - toColumn))  //Kill
                {
                    return true;
                }

            }
            if (fromRow - toRow == -1 && fromRow < toRow && fromColumn == toColumn) //standard forward
            {
                if (IsBlackPieceAt(toRow, toColumn) == true)
                {
                    return false;
                }
                return true;
            }
            if ((fromRow < toRow) && (fromRow - toRow) == -2 && (fromRow == 1) && (toColumn == fromColumn)) // initial sprint
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        private bool IsValidRookWhite(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsWhitePieceAt2(toRow, toColumn))
            {
                return false;
            }
           
            return (fromRow == toRow || fromColumn == toColumn) && IsPathClear(fromRow, fromColumn, toRow, toColumn);
        }

        private bool IsValidRookBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsBlackPieceAt(toRow, toColumn))
            {
                return false;
            }
            return (fromRow == toRow || fromColumn == toColumn) && IsPathClear(fromRow, fromColumn, toRow, toColumn);
        }

        private bool IsValidKnightWhite(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsWhitePieceAt2(toRow, toColumn))
            {
                return false;
            }
            int rowDiff = Math.Abs(fromRow - toRow);
            int colDiff = Math.Abs(fromColumn - toColumn);
            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        private bool IsValidKnightBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsBlackPieceAt(toRow, toColumn))
            {
                return false;
            }
            int rowDiff = Math.Abs(fromRow - toRow);
            int colDiff = Math.Abs(fromColumn - toColumn);
            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        private bool IsValidBishopWhite(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsWhitePieceAt2(toRow, toColumn))
            {
                return false;
            }
            System.Diagnostics.Debug.WriteLine("Bishop got past this");
            return Math.Abs(fromRow - toRow) == Math.Abs(fromColumn - toColumn) && IsPathClear(fromRow, fromColumn, toRow, toColumn);
        }
        private bool IsValidBishopBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsBlackPieceAt(toRow, toColumn))
            {
                return false;
            }
            return Math.Abs(fromRow - toRow) == Math.Abs(fromColumn - toColumn) && IsPathClear(fromRow, fromColumn, toRow, toColumn);
        }
        private bool IsValidQueenWhite(int fromRow, int fromColumn, int toRow, int toColumn)
        {

            return IsValidRookWhite(fromRow, fromColumn, toRow, toColumn) || IsValidBishopWhite(fromRow, fromColumn, toRow, toColumn);
        }

        private bool IsValidQueenBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {

            return IsValidRookBlack(fromRow, fromColumn, toRow, toColumn) || IsValidBishopBlack(fromRow, fromColumn, toRow, toColumn);
        }

        private bool IsValidKingWhite(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsWhitePieceAt2(toRow, toColumn))
            {
                return false;
            }
            int rowDiff = Math.Abs(fromRow - toRow);
            int colDiff = Math.Abs(fromColumn - toColumn);
            return rowDiff <= 1 && colDiff <= 1;
        }

        private bool IsValidKingBlack(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (IsBlackPieceAt(toRow, toColumn))
            {
                return false;
            }

            int rowDiff = Math.Abs(fromRow - toRow);
            int colDiff = Math.Abs(fromColumn - toColumn);
            return rowDiff <= 1 && colDiff <= 1;
        }

        #endregion
        private bool IsPathClear(int fromRow, int fromColumn, int toRow, int toColumn)
        {

            int rowDifference = toRow - fromRow;
            int colDifference = toColumn - fromColumn;


            int rowStep = rowDifference == 0 ? 0 : (rowDifference > 0 ? 1 : -1);
            int colStep = colDifference == 0 ? 0 : (colDifference > 0 ? 1 : -1);


            if (rowDifference != 0 && colDifference != 0 && Math.Abs(rowDifference) != Math.Abs(colDifference))
            {
                return false;
            }

            int currentRow = fromRow + rowStep;
            int currentColumn = fromColumn + colStep;

            while (currentRow != toRow || currentColumn != toColumn)
            {
                if (IsPieceAt(currentRow, currentColumn))
                {
                    return false;
                }
                currentRow += rowStep;
                currentColumn += colStep;
            }


            return true;
        }
        private bool IsPieceAt(int row, int column)
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image && Grid.GetRow(element) == row && Grid.GetColumn(element) == column)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWhitePieceAt2(int row, int column)
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image image && Grid.GetRow(image) == row && Grid.GetColumn(image) == column)
                {
                    if (image.Tag.ToString().Substring(0, 4) == "Pawn" && int.Parse(image.Tag.ToString().Substring(4)) <= 8)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Rock" && int.Parse(image.Tag.ToString().Substring(4)) <= 2)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Bish" && int.Parse(image.Tag.ToString().Substring(6)) <= 2)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Quee" && int.Parse(image.Tag.ToString().Substring(5)) <= 1)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "King" && int.Parse(image.Tag.ToString().Substring(4)) <= 1)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Hors" && int.Parse(image.Tag.ToString().Substring(5)) <= 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsBlackPieceAt(int row, int column)
        {
            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Image image && Grid.GetRow(image) == row && Grid.GetColumn(image) == column)
                {
                    if (image.Tag.ToString().Substring(0, 4) == "Pawn" && int.Parse(image.Tag.ToString().Substring(4)) >= 9)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Rock" && int.Parse(image.Tag.ToString().Substring(4)) >= 3)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Bish" && int.Parse(image.Tag.ToString().Substring(6)) >= 3)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Quee" && int.Parse(image.Tag.ToString().Substring(5)) >= 2)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "King" && int.Parse(image.Tag.ToString().Substring(4)) >= 2)
                    {
                        return true;
                    }
                    if (image.Tag.ToString().Substring(0, 4) == "Hors" && int.Parse(image.Tag.ToString().Substring(5)) >= 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void HighlightGridCell(int row, int column)
        {
            if (highlightedRectangle != null)
            {
                highlightedRectangle.Stroke = Brushes.Transparent;
            }

            foreach (UIElement element in MainGrid.Children)
            {
                if (element is Rectangle rect && Grid.GetRow(element) == row && Grid.GetColumn(element) == column)
                {
                    rect.Stroke = Brushes.Yellow;
                    highlightedRectangle = rect;
                    break;
                }
            }
        }

    }
}
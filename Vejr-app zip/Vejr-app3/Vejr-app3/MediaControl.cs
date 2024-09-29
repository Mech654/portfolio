using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;



namespace vejr_app3
{
    public class MediaControl
    {
        public MediaElement _mediaElement;

        public MediaControl(MediaElement mediaElement)
        {
            _mediaElement = mediaElement;

            // Subscribe to the MediaEnded event to enable looping
            _mediaElement.MediaEnded += MediaElement_MediaEnded;
        }

        public void SetWidth(double width)
        {
            _mediaElement.Width = width;
        }

        public void SetHeight(double height)
        {
            _mediaElement.Height = height;
        }



        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Restart the media playback
            _mediaElement.Position = TimeSpan.Zero;
            _mediaElement.Play();
        }

        public void Raining()
        {

            string sourceOG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources2\Rain.mp4");
            sourceOG = Path.GetFullPath(sourceOG);
            //string source1 = @"c:\users\drahm\onedrive\dokumenter\c#\h1_csharp_opgaver\vejr-app3\vejr-app3\resources2\rain.mp4";
            //string source2 = @"C:\Users\ahme1636\OneDrive\Dokumenter\C#\H1_CSHARP_OPGAVER\Vejr-app3\Vejr-app3\Resources2\Rain.mp4";


            try
            {
                if (File.Exists(sourceOG))
                {
                    Console.WriteLine("WOLLA found");
                    _mediaElement.Source = new Uri(sourceOG);
                    Console.WriteLine("WOLLA found and set.");

                }
                else
                {
                    Console.WriteLine("Neither sourceOG, source1, nor source2 exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            _mediaElement.Play();
        }

        public void Sun()
        {
            string sourceOG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources2\Sunny.mp4");
            sourceOG = Path.GetFullPath(sourceOG);
            try
            {
                if (File.Exists(sourceOG))
                {
                    Console.WriteLine("WOLLA found");
                    _mediaElement.Source = new Uri(sourceOG);
                    Console.WriteLine("WOLLA found and set.");

                }
                else
                {
                    Console.WriteLine("Neither sourceOG, source1, nor source2 exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            _mediaElement.Play();

        }
        public void Snow()
        {
            string sourceOG = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources2\Snow.mp4");
            sourceOG = Path.GetFullPath(sourceOG);
            try
            {
                if (File.Exists(sourceOG))
                {
                    Console.WriteLine("WOLLA found");
                    _mediaElement.Source = new Uri(sourceOG);
                    Console.WriteLine("WOLLA found and set.");

                }
                else
                {
                    Console.WriteLine("Neither sourceOG, source1, nor source2 exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            _mediaElement.Play();

        }
    }
}

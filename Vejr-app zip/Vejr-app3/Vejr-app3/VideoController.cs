using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Vejr_app2
{
    internal class VideoController
    {
        BackgroundVideo.Source = new Uri("pack://application:,,,/Resources/Rain.mp4", UriKind.Absolute);
        BackgroundVideo.play();


    }
}

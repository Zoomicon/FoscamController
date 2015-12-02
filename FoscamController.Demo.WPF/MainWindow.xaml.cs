//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151202

//Note: When using FOSCAM_HD_CAMERA, the VLC libraries have to be placed in a "LibVlc" subfolder, located in the
// same folder as the application executable (the bin\Debug folder when using Visual Studio).
// Can override that path using an optional parameter passed to FoscamHDVideo class constructor (there is also an
// extra optional parameter at that class for VLC options)
// That subfolder can be copied from the repository available at http://github.com/birbilis/xZune.Vlc


#define USE_FOSCAM_HD_CAMERA //uncomment this to use a Foscam HD Camera model instead of an MJPEG model

using System.Windows;
using System.Windows.Controls;

namespace Camera.Foscam
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region --- Constants ---

    private const string CAMERA_URL = "http://192.168.241.14:88"; //Foscam HD video controller knows how to replace the HTTP:// with RTSP:// to get to the RTSP video stream, so use the base URL of the camera here (the one it's administration page uses), plus don't use a "/" char at the end
    private const string USERNAME = "admin1";
    private const string PASSWORD = "admin1";

    #endregion

    #region --- Initialization ---

    public MainWindow()
    {
      InitializeComponent();
      this.Loaded += MainWindow_Loaded;
      this.Closing += MainWindow_Closing;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      FoscamCameraType cameraType =
        #if USE_FOSCAM_HD_CAMERA
        FoscamCameraType.FoscamHD;
        #else
        FoscamCameraType.FoscamMJPEG;
        #endif

      ptz.VideoController = FoscamVideo.CreateFoscamVideoController(cameraType, CAMERA_URL, USERNAME, PASSWORD);
      ptz.MotionController = FoscamMotion.CreateFoscamMotionController(cameraType, CAMERA_URL, USERNAME, PASSWORD);
      ptz.ZoomController = FoscamZoom.CreateFoscamZoomController(cameraType, CAMERA_URL, USERNAME, PASSWORD);

      if (ptz.VideoController != null)
      {
        UIElement player = ptz.VideoController.VideoDisplay;
        player.SetValue(Grid.RowProperty, 0);
        //player.SetValue(Canvas.ZIndexProperty, -1);
        LayoutRoot.Children.Add(player);
        ptz.VideoController.StartVideo();
      }
    }

    #endregion

    #region --- Cleanup ---

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (ptz.VideoController != null)
      {
        ptz.VideoController.StopVideo();
        ptz.VideoController = null;
      }
    }

    #endregion

  }

}

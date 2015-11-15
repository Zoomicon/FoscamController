//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151111

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

    private const string CAMERA_URL = "http://someIPorDomainName:somePort"; //Foscam HD video controller knows how to replace the HTTP:// with RTSP:// to get to the RTSP video stream, so use the base URL of the camera here (the one it's administration page uses), plus don't use a "/" char at the end
    private const string USERNAME = "user";
    private const string PASSWORD = "pwd";

    #endregion

    #region --- Fields ---

    private IMotionController _motion;
    private IVideoController _video;

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

      _video = FoscamVideo.CreateFoscamVideoController(cameraType, CAMERA_URL, USERNAME, PASSWORD);
      _motion = FoscamMotion.CreateFoscamMotionController(cameraType, CAMERA_URL, USERNAME, PASSWORD);

      if (_video != null)
      {
        UIElement player = _video.VideoPlayer;
        player.SetValue(Grid.RowProperty, 0);
        //player.SetValue(Canvas.ZIndexProperty, -1);
        LayoutRoot.Children.Add(player);
        _video.StartVideo();
      }
    }

    #endregion

    #region --- Cleanup ---

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (_video != null)
      {
        _video.StopVideo();
        _video = null;
      }
    }

    #endregion

    #region --- Events ---

    private void btnCenter_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionGotoCenter();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionUp();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionDown();
    }

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionLeft();
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionRight();
    }

    #endregion
  }

}

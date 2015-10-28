//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151028

//#define USE_FOSCAM_HD_CAMERA //uncomment this to use a Foscam HD Camera model instead of an MJPEG model (note that the video won't work in that case, just the motion control for now)

#if USE_FOSCAM_HD_CAMERA
using Camera.Foscam.HD;
#else
using Camera.Foscam.MJPEG;
#endif

using System.Windows;

namespace Camera.Foscam
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region --- Constants ---

    private const string CAMERA_URL = "http://cameraAddressAndPort";
    private const string USERNAME = "username";
    private const string PASSWORD = "password";

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
      #if USE_FOSCAM_HD_CAMERA
      _video = null;
      _motion = new FoscamHDMotion(CAMERA_URL, USERNAME, PASSWORD);
      #else
      _video = new FoscamMJPEGVideo(CAMERA_URL, USERNAME, PASSWORD);
      _motion = new FoscamMJPEGMotion(CAMERA_URL, USERNAME, PASSWORD);
      #endif

      if (_video != null)
      {
        _video.ImageReady += dec_FrameReady;
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

    private void dec_FrameReady(object sender, ImageReadyEventArgs args)
    {
      imgStream.Source = args.Image;
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionRight();
    }

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionLeft();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionDown();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionUp();
    }

    #endregion
  }

}

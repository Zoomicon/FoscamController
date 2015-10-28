//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151028

using Camera.Foscam.MJPEG;
using Camera.Foscam.HD;
using System.Windows;

namespace Camera.Foscam
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region --- Constants ---

    private const string CAMERA_URL = "http://cameraURL:cameraPort";
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
      _motion =
        new FoscamMJPEGMotion(CAMERA_URL, USERNAME, PASSWORD);
        //new FoscamHDMotion(CAMERA_URL, USERNAME, PASSWORD);

      _video = new FoscamMJPEGVideo(CAMERA_URL, USERNAME, PASSWORD);
      _video.ImageReady += dec_FrameReady;
      _video.StartVideo();
    }

    #endregion

    #region --- Cleanup ---

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      _video.StopVideo();
    }

    #endregion

    #region --- Events ---

    private void dec_FrameReady(object sender, ImageReadyEventArgs args)
    {
      imgStream.Source = args.Image;
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
      _motion.MotionRight();
    }

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      _motion.MotionLeft();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      _motion.MotionDown();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      _motion.MotionUp();
    }

    #endregion
  }

}

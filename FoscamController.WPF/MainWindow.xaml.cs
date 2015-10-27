//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151027

using System.Windows;

namespace Camera.Foscam
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region --- Constants ---

    private const string CAMERA_URL = "http://yoururl";
    private const string USERNAME = "yourusername";
    private const string PASSWORD = "yourpassword";

    #endregion

    #region --- Fields ---

    private IPanTiltController _pantilt;
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
      _pantilt = new FoscamPanTilt(CAMERA_URL, USERNAME, PASSWORD);

      _video = new FoscamMJPEG(CAMERA_URL, USERNAME, PASSWORD);
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
      _pantilt.PanRight();
    }

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      _pantilt.PanLeft();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      _pantilt.TiltDown();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      _pantilt.TiltUp();
    }

    #endregion
  }

}

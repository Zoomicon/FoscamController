//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MainWindow.xaml.cs
//Version: 20151026

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

    private IFoscamController _controller;

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
      _controller = new FoscamController(CAMERA_URL, USERNAME, PASSWORD);
      _controller.ImageReady += dec_FrameReady;
      _controller.StartVideo();
    }

    #endregion

    #region --- Cleanup ---

    private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      _controller.StopVideo();
    }

    #endregion

    #region --- Events ---

    private void dec_FrameReady(object sender, ImageReadyEventArgs args)
    {
      imgStream.Source = args.Image;
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
      _controller.PanRight();
    }

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      _controller.PanLeft();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      _controller.TiltDown();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      _controller.TiltUp();
    }

    #endregion
  }

}

//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: PTZControl.xaml.cs
//Version: 20151201

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Camera
{
  /// <summary>
  /// Interaction logic for PTZControl.xaml
  /// </summary>
  public partial class PTZControl : UserControl
  {

    #region --- Fields ---

    protected IVideoController _video;
    protected IMotionController _motion;
    protected IZoomController _zoom;

    #endregion

    #region --- Initialization ---
    
    public PTZControl()
    {
      InitializeComponent();
    }

    #endregion

    #region --- Properties ---

    public IVideoController VideoController
    {
      get { return _video; }
      set { _video = value; }
    }

    public IMotionController MotionController
    {
      get { return _motion; }
      set { _motion = value; }
    }

    public IZoomController ZoomController
    {
      get { return _zoom; }
      set { _zoom = value; }
    }

    #endregion

    #region --- Events ---

    #region 1st row

    private void btnUpLeft_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionUpLeft();
    }

    private void btnUp_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionUp();
    }

    private void btnUpRight_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionUpRight();
    }

    #endregion

    #region 2nd row

    private void btnLeft_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionLeft();
    }

    private void btnCenter_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionGotoCenter();
    }

    private void btnRight_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionRight();
    }

    #endregion

    #region 3rd row

    private void btnDownLeft_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionDownLeft();
    }

    private void btnDown_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionDown();
    }

    private void btnDownRight_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionDownRight();
    }

    #endregion

    #region 4th row

    private void btnZoomOut_Click(object sender, RoutedEventArgs e)
    {
      if (_zoom != null)
        _zoom.ZoomOut();
    }

    /// <summary>
    /// On stop button click, stop any current motion and zooming
    /// </summary>
    private void btnStop_Click(object sender, RoutedEventArgs e)
    {
      if (_motion != null)
        _motion.MotionStop();

      if (_zoom != null)
        _zoom.ZoomStop();
    }

    private void btnZoomIn_Click(object sender, RoutedEventArgs e)
    {
      if (_zoom != null)
        _zoom.ZoomIn();
    }

    #endregion

    #endregion
  }
}

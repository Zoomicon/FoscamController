//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IZoomController.cs
//Version: 20151115

namespace Camera
{
  public interface IZoomController
  {

    #region --- Properties ---

    double Zoom { get; set; }

    #endregion

    #region --- Methods ---

    void ZoomStop();
    void ZoomIn();
    void Zoomout();

    #endregion
  }

}
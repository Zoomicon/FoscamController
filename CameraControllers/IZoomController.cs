//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IZoomController.cs
//Version: 20151120

namespace Camera
{
  public interface IZoomController
  {

    #region --- Methods ---

    void ZoomStop();
    void ZoomIn();
    void ZoomOut();

    #endregion
  }

}
//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IPanTiltController.cs
//Version: 20151026

namespace Camera
{
  public interface IPanTiltController
  {

    #region --- Methods ---

    void PanLeft();
    void PanRight();

    void TiltDown();
    void TiltUp();

    #endregion
  }

}
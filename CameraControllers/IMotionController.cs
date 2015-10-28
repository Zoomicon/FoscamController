//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IMotionController.cs
//Version: 20151028

namespace Camera
{
  public interface IMotionController
  {

    #region --- Methods ---

    void MotionStop();
    void MotionUp();
    void MotionDown();
    void MotionLeft();
    void MotionRight();
    void MotionUpLeft();
    void MotionUpRight();
    void MotionDownLeft();
    void MotionDownRight();
    void MotionGotoCenter();
    void MotionGotoPreset(string name);

    #endregion
  }

}
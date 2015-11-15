//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamMotion.cs
//Version: 20151115

using Camera.Foscam.HD;
using Camera.Foscam.MJPEG;

namespace Camera.Foscam
{
  public static class FoscamMotion
  {

    #region --- Factory methods ---

    public static IMotionController CreateFoscamMJPEGMotionController(string url, string username, string password)
    {
      return new FoscamMJPEGMotion(url, username, password);
    }

    public static IMotionController CreateFoscamHDMotionController(string url, string username, string password)
    {
      return new FoscamHDMotion(url, username, password);
    }

    public static IMotionController CreateFoscamMotionController(FoscamCameraType cameraType, string url, string username, string password)
    {
      switch (cameraType)
      {
        case FoscamCameraType.FoscamMJPEG:
          return CreateFoscamMJPEGMotionController(url, username, password);
        case FoscamCameraType.FoscamHD:
          return CreateFoscamHDMotionController(url, username, password);
        default:
          return null;
      }
    }

    #endregion

  }

}

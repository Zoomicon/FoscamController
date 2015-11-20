//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamZoom.cs
//Version: 20151120

using Camera.Foscam.HD;
using Camera.Foscam.MJPEG;

namespace Camera.Foscam
{
  public static class FoscamZoom
  {

    #region --- Factory methods ---

    public static IZoomController CreateFoscamMJPEGZoomController(string url, string username, string password)
    {
      return new FoscamMJPEGZoom(url, username, password);
    }

    public static IZoomController CreateFoscamHDZoomController(string url, string username, string password)
    {
      return new FoscamHDZoom(url, username, password);
    }

    public static IZoomController CreateFoscamZoomController(FoscamCameraType cameraType, string url, string username, string password)
    {
      switch (cameraType)
      {
        case FoscamCameraType.FoscamMJPEG:
          return CreateFoscamMJPEGZoomController(url, username, password);
        case FoscamCameraType.FoscamHD:
          return CreateFoscamHDZoomController(url, username, password);
        default:
          return null;
      }
    }

    #endregion

  }

}

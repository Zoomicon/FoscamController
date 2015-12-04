//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamVideo.cs
//Version: 20151204

using Camera.Foscam.HD;
using Camera.Foscam.MJPEG;

namespace Camera.Foscam
{
  public static class FoscamVideo
  {

    #region --- Factory methods ---

    public static IVideoController CreateFoscamMJPEGVideoController(string url, string username, string password)
    {
      return new FoscamMJPEGVideo(url, username, password);
    }

    public static IVideoController CreateFoscamHDVideoController(string url, string username, string password, string libVlcPath = null, string[] vlcOptions = null) //can use null for libVlcPath and/or vlcOptions to use their defaults
    {
      return new FoscamHDVideo(url, username, password, libVlcPath, vlcOptions);
    }

    public static IVideoController CreateFoscamVideoController(FoscamCameraType cameraType, string url, string username, string password, string[] options = null) //can use null for options to use any defaults
        {
      switch (cameraType)
      {
        case FoscamCameraType.FoscamMJPEG:
          return CreateFoscamMJPEGVideoController(url, username, password); //no extra options for this one
        case FoscamCameraType.FoscamHD:
          return CreateFoscamHDVideoController(url, username, password, null, options); //using default libVlcPath and using options for VlcOptions parameter
        default:
          return null;
      }
    }

    #endregion

  }

}

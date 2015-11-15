﻿//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamVideo.cs
//Version: 20151115

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

    public static IVideoController CreateFoscamHDVideoController(string url, string username, string password)
    {
      return new FoscamHDVideo(url, username, password);
    }

    public static IVideoController CreateFoscamVideoController(FoscamCameraType cameraType, string url, string username, string password)
    {
      switch (cameraType)
      {
        case FoscamCameraType.FoscamMJPEG:
          return CreateFoscamMJPEGVideoController(url, username, password);
        case FoscamCameraType.FoscamHD:
          return CreateFoscamHDVideoController(url, username, password);
        default:
          return null;
      }
    }

    #endregion

  }

}

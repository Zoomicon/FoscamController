//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IVideoController.cs
//Version: 20151026

using System;

namespace Camera
{
  public interface IVideoController
  {
    #region --- Methods ---

    void StartVideo();
    void StopVideo();

    #endregion

    #region --- Events ---

    event EventHandler<ImageReadyEventArgs> ImageReady;

    #endregion
  }

}
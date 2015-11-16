//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IVideoController.cs
//Version: 20151116

using System.Windows;

namespace Camera
{
  public interface IVideoController
  {

    #region --- Properties ---

    UIElement VideoDisplay { get; }

    #endregion

    #region --- Methods ---

    void StartVideo();
    void StopVideo();

    #endregion

  }

}
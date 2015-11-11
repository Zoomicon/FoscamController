//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IVideoController.cs
//Version: 20151111

using System.Windows;

namespace Camera
{
  public interface IVideoController
  {

    #region --- Properties ---

    UIElement VideoPlayer { get; }

    #endregion

    #region --- Methods ---

    void StartVideo();
    void StopVideo();

    #endregion

  }

}
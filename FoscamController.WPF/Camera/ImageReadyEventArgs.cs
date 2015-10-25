//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: ImageReadyEventArgs.cs
//Version: 20151026

using System;
using System.Windows.Media.Imaging;

namespace Camera
{
  public class ImageReadyEventArgs : EventArgs
  {

    #region --- Properties ---

    public BitmapImage Image { get; set; }

    #endregion
  }

}

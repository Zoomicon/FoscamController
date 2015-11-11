//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: ImageReadyEventArgs.cs
//Version: 20151111

using System;
using System.Windows.Media.Imaging;

namespace Images
{
  public class ImageReadyEventArgs : EventArgs
  {

    #region --- Properties ---

    public BitmapImage Image { get; set; }

    #endregion
  }

}

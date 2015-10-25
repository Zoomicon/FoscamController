//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: PartReadyEventArgs.cs
//Version: 20151026

using System;

namespace Camera.MJPEG
{
  public class PartReadyEventArgs : EventArgs
  {

    #region --- Properties ---

    public byte[] Part { get; set; }

    #endregion
  }

}

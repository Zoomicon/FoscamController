//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: PartReadyEventArgs.cs
//Version: 20151025

using System;

namespace Camera.Foscam
{
  public class PartReadyEventArgs : EventArgs
  {

    #region --- Properties ---

    public byte[] Part { get; set; }

    #endregion
  }

}

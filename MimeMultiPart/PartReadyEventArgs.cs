//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: PartReadyEventArgs.cs
//Version: 20151027

using System;

namespace Mime.MultiPart
{
  public class PartReadyEventArgs : EventArgs
  {

    #region --- Properties ---

    public byte[] Part { get; set; }

    #endregion
  }

}

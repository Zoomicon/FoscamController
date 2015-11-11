//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IImageReadyEventProvider.cs
//Version: 20151111

using System;

namespace Images
{
  public interface IImageReadyEventProvider
  {

    #region --- Events ---

    event EventHandler<ImageReadyEventArgs> ImageReady;

    #endregion
  }

}

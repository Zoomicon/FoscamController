//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: IImageReadyEventHandler.cs
//Version: 20151111

namespace Images
{
  public interface IImageReadyEventHandler
  {

    #region --- Events ---

    void OnImageReady(object sender, ImageReadyEventArgs args);

    #endregion

  }

}

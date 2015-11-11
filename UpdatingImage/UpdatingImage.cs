//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: UpdatingImage.cs
//Version: 20151111

using System.Windows.Controls;

namespace Images
{
  public class UpdatingImage : Image, IImageReadyEventHandler
  {

    public void OnImageReady(object sender, ImageReadyEventArgs args)
    {
      Source = args.Image;
    }

  }

}

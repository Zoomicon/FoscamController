//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: UpdatingImage.cs
//Version: 20151227

using System;
using System.Windows.Controls;

namespace Images
{
  public class UpdatingImage : Image, IImageReadyEventHandler
  {

    public void OnImageReady(object sender, ImageReadyEventArgs args)
    {
      Dispatcher.BeginInvoke((Action)(()=> Source = args.Image)); //updating the (parent) Image control from the UI thread //note: make sure Freeze has been called on the BitmapImage object used as ImageSource
    }

  }

}

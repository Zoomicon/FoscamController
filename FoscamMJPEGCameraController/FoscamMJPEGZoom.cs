//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamMJPEGZoom.cs
//Version: 20151120

//note: commands found from https://wiki.zoneminder.com/Foscam_Clones

using System;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace Camera.Foscam.MJPEG
{

  public class FoscamMJPEGZoom : IZoomController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";

    private const string _relativeUri = "/decoder_control.cgi?command={0}";

    public const int COMMAND_ZOOM_STOP = COMMAND_ZOOM_IN_STOP; //should work for all zoom stops
    public const int COMMAND_ZOOM_IN = 16;
    public const int COMMAND_ZOOM_IN_STOP = 17;
    public const int COMMAND_ZOOM_OUT = 18;
    public const int COMMAND_ZOOM_OUT_STOP = 19;

    #endregion

    #region --- Fields ---

    private string _url;
    private bool _zooming;
    private HttpClient _client;

    #endregion

    #region --- Initialization ---

    public FoscamMJPEGZoom(string url, string username, string password)
    {
      WebRequestHandler handler = new WebRequestHandler();
      handler.Credentials = new NetworkCredential(username, password);
      _url = url;
      _client = new HttpClient(handler);
      _client.BaseAddress = new Uri(_url);
      _client.Timeout = TimeSpan.FromMilliseconds(-1);
    }

    #endregion

    #region --- Methods ---

    private async void SendCommand(int commandNumber)
    {
      try {
        HttpResponseMessage result;
        result = await _client.GetAsync(string.Format(_relativeUri, commandNumber));
        result.EnsureSuccessStatusCode();
      }
      catch(Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
      {
        MessageBox.Show(e.Message + "\n\n" + ERROR_CONNECTION, ERROR_TITLE);
      }
    }

    public void ZoomStop()
    {
      SendCommand(COMMAND_ZOOM_STOP);
    }

    public void ZoomIn()
    {
      int command = _zooming ? COMMAND_ZOOM_IN_STOP : COMMAND_ZOOM_IN;
      SendCommand(command);
      _zooming = !_zooming;
    }

    public void ZoomOut()
    {
      int command = _zooming ? COMMAND_ZOOM_OUT_STOP : COMMAND_ZOOM_OUT;
      SendCommand(command);
      _zooming = !_zooming;
    }

    #endregion

  }

}

//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamHDZoom.cs
//Version: 20151120

using System;
using System.Net.Http;
using System.Windows;

namespace Camera.Foscam.HD
{
  public class FoscamHDZoom : IZoomController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";

    private const string COMMAND_ZOOM_STOP = "zoomStop";
    private const string COMMAND_ZOOM_UP = "zoomIn";
    private const string COMMAND_ZOOM_DOWN = "zoomOut";

    #endregion

    #region --- Fields ---

    private string _commandRelativeUri = "/cgi-bin/CGIProxy.fcgi?usr={0}&pwd={1}&cmd={2}"; //the constructor expects first param in this format string to be user, 2nd to be password and third to be a placeholder for commands

    private string _url;
    private HttpClient _client;

    #endregion

    #region --- Initialization ---

    public FoscamHDZoom(string url, string username, string password)
    {
      WebRequestHandler handler = new WebRequestHandler();
      //handler.Credentials = new NetworkCredential(username, password); //not used
      _commandRelativeUri = String.Format(_commandRelativeUri, username, password, "{0}"); //we pass "{0}" at the end since we're generating a new format string

      _url = url;
      _client = new HttpClient(handler);
      _client.BaseAddress = new Uri(_url);
      _client.Timeout = TimeSpan.FromMilliseconds(-1);
    }

    #endregion

    #region --- Methods ---

    private async void SendCommand(string command)
    {
      try
      {
        HttpResponseMessage result;
        result = await _client.GetAsync(string.Format(_commandRelativeUri, command));
        result.EnsureSuccessStatusCode();
      }
      catch (Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
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
      ZoomStop();
      SendCommand(COMMAND_ZOOM_UP);
    }

    public void ZoomOut()
    {
      ZoomStop();
      SendCommand(COMMAND_ZOOM_DOWN);
    }

    #endregion

  }

}

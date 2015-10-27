//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamPanTilt.cs
//Version: 20151027

using System;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace Camera.Foscam
{
  public class FoscamPanTilt : IPanTiltController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";

    //Foscam specific:
    private const string _panningRelativeUri = "/decoder_control.cgi?command={0}";

    #endregion

    #region --- Fields ---

    private string _url;
    private bool _panning;
    private HttpClient _client;

    #endregion

    #region --- Initialization ---

    public FoscamPanTilt(string url, string username, string password)
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

    private async void SendPanCommand(int commandNumber)
    {
      try {
        HttpResponseMessage result;
        result = await _client.GetAsync(string.Format(_panningRelativeUri, commandNumber));
        result.EnsureSuccessStatusCode();
      }
      catch(Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
      {
        MessageBox.Show(e.Message + "\n\n" + ERROR_CONNECTION, ERROR_TITLE);
      }
    }

    public void TiltUp()
    {
      int command = _panning ? 1 : 0;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void TiltDown()
    {
      int command = _panning ? 3 : 2;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void PanRight()
    {
      int command = _panning ? 5 : 4;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void PanLeft()
    {
      int command = _panning ? 7 : 6;
      SendPanCommand(command);
      _panning = !_panning;
    }

    #endregion

  }

}

//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamMJPEGMotion.cs
//Version: 20151028

using System;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace Camera.Foscam.MJPEG
{
  public class FoscamMJPEGMotion : IMotionController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";

    private const string _panningRelativeUri = "/decoder_control.cgi?command={0}";

    #endregion

    #region --- Fields ---

    private string _url;
    private bool _panning;
    private HttpClient _client;

    #endregion

    #region --- Initialization ---

    public FoscamMJPEGMotion(string url, string username, string password)
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

    public void MotionStop()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionUp()
    {
      int command = _panning ? 1 : 0;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void MotionDown()
    {
      int command = _panning ? 3 : 2;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void MotionLeft()
    {
      int command = _panning ? 7 : 6;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void MotionRight()
    {
      int command = _panning ? 5 : 4;
      SendPanCommand(command);
      _panning = !_panning;
    }

    public void MotionUpLeft()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionUpRight()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionDownLeft()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionDownRight()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionGotoCenter()
    {
      throw new NotImplementedException(); //TODO
    }

    public void MotionGotoPreset(string name)
    {
      throw new NotImplementedException(); //TODO
    }

    #endregion

  }

}

//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamMJPEGMotion.cs
//Version: 20151228

//note: commands found from http://blogs.infosupport.com/writing-an-ip-camera-viewer-in-c-5-0 and https://wiki.zoneminder.com/Foscam_Clones
//      tested with BIONICS Robocam 2+ (a FOSCAM MJPEG IP camera clone)

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

    private const string _relativeUri = "/decoder_control.cgi?command={0}";

    public const int COMMAND_MOTION_STOP = COMMAND_MOTION_UP_STOP; //should work for all motion stops
    public const int COMMAND_MOTION_UP_STOP = 1;
    public const int COMMAND_MOTION_UP = 0;
    public const int COMMAND_MOTION_DOWN_STOP = 3;
    public const int COMMAND_MOTION_DOWN = 2;
    public const int COMMAND_MOTION_LEFT_STOP = 5;
    public const int COMMAND_MOTION_LEFT = 4;
    public const int COMMAND_MOTION_RIGHT_STOP = 7;
    public const int COMMAND_MOTION_RIGHT = 6;
    public const int COMMAND_MOTION_UP_LEFT = 90;
    public const int COMMAND_MOTION_UP_RIGHT = 91;
    public const int COMMAND_MOTION_DOWN_LEFT = 92;
    public const int COMMAND_MOTION_DOWN_RIGHT = 93;
    public const int COMMAND_MOTION_GOTO_CENTER = 25;

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

    private void SendCommand(int command)
    {
      SendCommand(command.ToString());
    }

    private async void SendCommand(string command)
    {
      try {
        HttpResponseMessage result;
        result = await _client.GetAsync(string.Format(_relativeUri, command));
        result.EnsureSuccessStatusCode();
      }
      catch(Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
      {
        MessageBox.Show(e.Message + "\n\n" + ERROR_CONNECTION, ERROR_TITLE);
      }
    }

    public void MotionStop()
    {
      SendCommand(COMMAND_MOTION_UP_STOP);
    }

    public void MotionUp()
    {
      int command = _panning ? COMMAND_MOTION_UP_STOP : COMMAND_MOTION_UP;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionDown()
    {
      int command = _panning ? COMMAND_MOTION_DOWN_STOP : COMMAND_MOTION_DOWN;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionLeft()
    {
      int command = _panning ? COMMAND_MOTION_LEFT_STOP : COMMAND_MOTION_LEFT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionRight()
    {
      int command = _panning ? COMMAND_MOTION_RIGHT_STOP : COMMAND_MOTION_RIGHT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionUpLeft()
    {
      int command = _panning ? COMMAND_MOTION_STOP : COMMAND_MOTION_UP_LEFT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionUpRight()
    {
      int command = _panning ? COMMAND_MOTION_STOP : COMMAND_MOTION_UP_RIGHT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionDownLeft()
    {
      int command = _panning ? COMMAND_MOTION_STOP : COMMAND_MOTION_DOWN_LEFT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionDownRight()
    {
      int command = _panning ? COMMAND_MOTION_STOP : COMMAND_MOTION_DOWN_RIGHT;
      SendCommand(command);
      _panning = !_panning;
    }

    public void MotionGotoCenter()
    {
      SendCommand(COMMAND_MOTION_GOTO_CENTER);
    }

    public void MotionGotoPreset(string name)
    {
      SendCommand(name);
    }

    #endregion

  }

}

//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamHDMotion.cs
//Version: 20151120

//commands found from http://www.ipcamcontrol.net/files/Foscam%20IPCamera%20CGI%20User%20Guide-V1.0.4.pdf

using System;
using System.Net.Http;
using System.Windows;

namespace Camera.Foscam.HD
{
  public class FoscamHDMotion : IMotionController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";

    private const string COMMAND_MOTION_STOP = "ptzStopRun";
    private const string COMMAND_MOTION_UP = "ptzMoveUp";
    private const string COMMAND_MOTION_DOWN = "ptzMoveDown";
    private const string COMMAND_MOTION_LEFT = "ptzMoveLeft";
    private const string COMMAND_MOTION_RIGHT = "ptzMoveRight";
    private const string COMMAND_MOTION_UP_LEFT = "ptzMoveTopLeft";
    private const string COMMAND_MOTION_UP_RIGHT = "ptzMoveTopRight";
    private const string COMMAND_MOTION_DOWN_LEFT = "ptzMoveBottomLeft";
    private const string COMMAND_MOTION_DOWN_RIGHT = "ptzMoveBottomRight";
    private const string COMMAND_MOTION_GOTO_CENTER = "ptzReset";
    private const string COMMAND_MOTION_GOTO_PRESET = "ptzGotoPresetPoint";

    private const string PRESET_TOPMOST = "TopMost";
    private const string PRESET_BOTTOMMOST = "BottomMost";
    private const string PRESET_LEFTMOST = "LeftMost";
    private const string PRESET_RIGHTMOST = "RightMost";

    #endregion

    #region --- Fields ---

    private string _commandRelativeUri = "/cgi-bin/CGIProxy.fcgi?usr={0}&pwd={1}&cmd={2}"; //the constructor expects first param in this format string to be user, 2nd to be password and third to be a placeholder for commands

    private string _url;
    private HttpClient _client;

    #endregion

    #region --- Initialization ---

    public FoscamHDMotion(string url, string username, string password)
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

    public void MotionStop()
    {
      SendCommand(COMMAND_MOTION_STOP);
    }

    public void MotionUp()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_UP);
    }

    public void MotionDown()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_DOWN);
    }

    public void MotionLeft()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_LEFT);
    }

    public void MotionRight()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_RIGHT);
    }

    public void MotionUpLeft()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_UP_LEFT);
    }

    public void MotionUpRight()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_UP_RIGHT);
    }

    public void MotionDownLeft()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_DOWN_LEFT);
    }

    public void MotionDownRight()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_DOWN_RIGHT);
    }

    public void MotionGotoCenter()
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_GOTO_CENTER);
    }

    public void MotionGotoPreset(string name)
    {
      MotionStop();
      SendCommand(COMMAND_MOTION_GOTO_PRESET + "&name=" + name);
    }

    #endregion

  }

}

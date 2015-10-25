//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamController.cs
//Version: 20151025

using Camera.MJPEG;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Camera.Foscam
{
  public class FoscamController : IFoscamController
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";
    private const string ERROR_NO_MJPEG = "The camera did not return a MJPEG stream";

    //Foscam specific:
    private const string _videoRelativeUri = "/videostream.cgi?resolution=32&rate=0";
    private const string _panningRelativeUri = "/decoder_control.cgi?command={0}";

    #endregion

    #region --- Fields ---

    private string _url;
    private bool _panning;
    private HttpClient _client;
    private AutomaticMultiPartReader _reader;
    private BitmapImage _currentFrame;

    #endregion

    #region --- Initialization ---

    public FoscamController(string url, string username, string password)
    {
      _url = url;
      WebRequestHandler handler = new WebRequestHandler();
      handler.Credentials = new NetworkCredential(username, password);
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

    public async void StartVideo()
    {
      try {
        HttpResponseMessage resultMessage = await _client.GetAsync(_videoRelativeUri, HttpCompletionOption.ResponseHeadersRead);
        //because of the configure await the rest of this method happens on a background thread.
        resultMessage.EnsureSuccessStatusCode();
        // check the response type
        if (!resultMessage.Content.Headers.ContentType.MediaType.Contains("multipart"))
          throw new ArgumentException(ERROR_NO_MJPEG); //TODO: can a caller catch this or is there an issue with async? (if it can't should show message here and return from method)
        else
        {
          _reader = new AutomaticMultiPartReader(new MultiPartStream(await resultMessage.Content.ReadAsStreamAsync()));
          _reader.PartReady += _reader_PartReady;
          _reader.StartProcessing();
        }
      }
      catch(Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
      {
        MessageBox.Show(e.Message + "\n\n" + ERROR_CONNECTION, ERROR_TITLE);
      }
    }

    public void StopVideo()
    {
      if (_reader != null)
        _reader.StopProcessing();
    }

    #endregion

    #region --- Events ---

    public event EventHandler<ImageReadyEventArgs> ImageReady;

    protected void OnImageReady()
    {
      if (ImageReady != null)
        ImageReady(this, new ImageReadyEventArgs() { Image = _currentFrame });
    }

    private void _reader_PartReady(object sender, PartReadyEventArgs e)
    {
      //let's get this events back on the UI thread
      Stream frameStream = new MemoryStream(e.Part);
      App.Current.Dispatcher.Invoke(new Action(() =>
      {
        _currentFrame = new BitmapImage();
        _currentFrame.BeginInit();
        _currentFrame.StreamSource = frameStream;
        _currentFrame.EndInit();
        OnImageReady();
      }));
    }

    #endregion

  }
}

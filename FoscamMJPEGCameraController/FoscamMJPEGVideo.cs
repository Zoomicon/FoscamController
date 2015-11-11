//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamMJPEGVideo.cs
//Version: 20151111

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Mime.MultiPart;
using Images;

namespace Camera.Foscam.MJPEG
{
  public class FoscamMJPEGVideo : IVideoController, IImageReadyEventProvider
  {

    #region --- Constants ---

    private const string ERROR_TITLE = "Error";
    private const string ERROR_CONNECTION = "Have you set the correct values for Camera URL and Username/Password in the code?";
    private const string ERROR_NO_MJPEG = "The camera did not return a MJPEG stream";

    private const string VIDEO_RELATIVE_URL = "/videostream.cgi?resolution=32&rate=0";

    #endregion

    #region --- Fields ---

    private string _url;
    private HttpClient _client;
    private AutomaticMultiPartReader _reader;
    private BitmapImage _currentFrame;
    private UpdatingImage player;

    #endregion

    #region --- Initialization ---

    public FoscamMJPEGVideo(string url, string username, string password)
    {
      WebRequestHandler handler = new WebRequestHandler();
      handler.Credentials = new NetworkCredential(username, password);
      _url = url;
      _client = new HttpClient(handler);
      _client.BaseAddress = new Uri(_url);
      _client.Timeout = TimeSpan.FromMilliseconds(-1);
    }

    #endregion

    #region --- Properties ---

    public UIElement VideoPlayer
    {
      get
      {
        if (player == null)
        {
          player = new UpdatingImage();
          ImageReady += player.OnImageReady;
        }
        return player;
      }
    }

    #endregion

    #region --- Methods ---

    public async void StartVideo()
    {
      try
      {
        HttpResponseMessage resultMessage = await _client.GetAsync(VIDEO_RELATIVE_URL, HttpCompletionOption.ResponseHeadersRead);
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
      catch (Exception e) //TODO: if caller can catch the exception (if no issue with async), maybe let it pass through and show message at caller
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

    public event EventHandler<ImageReadyEventArgs> ImageReady; //implementing IImageReadyEventProvider interface

    protected void OnImageReady()
    {
      if (ImageReady != null)
        ImageReady(this, new ImageReadyEventArgs() { Image = _currentFrame });
    }

    private void _reader_PartReady(object sender, PartReadyEventArgs e)
    {
      //let's get this events back on the UI thread
      Stream frameStream = new MemoryStream(e.Part);
      Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
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

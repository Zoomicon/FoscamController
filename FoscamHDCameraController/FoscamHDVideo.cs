//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamHDVideo.cs
//Version: 20151116

using System;
using System.Windows;

using xZune.Vlc.Wpf;

//Note: By default the VLC libraries have to be placed in a "LibVlc" subfolder (see DEFAULT_LIBVLC_PATH constant),
//located in the same folder as the application executable (the bin\Debug folder when using Visual Studio).
//That subfolder can be copied from the repository available at http://github.com/birbilis/xZune.Vlc

namespace Camera.Foscam.HD
{
  public class FoscamHDVideo : IVideoController
  {

    #region --- Constants ---

    private const string VIDEO_RELATIVE_URL = "/videoMain";
    private const string DEFAULT_LIBVLC_PATH = "LibVlc";
    private readonly string[] DEFAULT_VLC_OPTIONS = new string[] { "-I dummy", "--ignore-config", "--no-video-title", "--file-logging", "--logfile=log.txt", "--verbose=2", "--no-sub-autodetect-file", "--rtsp-tcp" };

    #endregion

    #region --- Fields ---

    private string _url;
    private string _libVlcPath;
    private string[] _vlcOptions;
    private VlcPlayer _player;

    #endregion

    #region --- Initialization ---

    public FoscamHDVideo(string url, string username, string password, string libVlcPath = DEFAULT_LIBVLC_PATH, string[] vlcOptions = null)
    {
      string urlPrefix = "rtsp://" + username + ":" + password + "@";
      _url = url.Replace("http://", urlPrefix).Replace("https://", urlPrefix);
      _url += VIDEO_RELATIVE_URL;
      _libVlcPath = libVlcPath;
      _vlcOptions = vlcOptions ?? DEFAULT_VLC_OPTIONS;
    }

    #endregion

    #region --- Properties ---

    public UIElement VideoDisplay
    {
      get
      {
        if (_player == null)
          _player = new VlcPlayer() {
            LibVlcPath = _libVlcPath,
            VlcOption = _vlcOptions
          }; //option --rtsp-tcp is needed to pass RTSP through a VPN
        return _player;
      }
    }

    #endregion

    #region --- Methods ---

    public void StartVideo()
    {
      _player.BeginStop((ar) =>
      {
        _player.LoadMedia(new Uri(_url));
        _player.Play();
      });
    }

    public void StopVideo()
    {
      _player.Stop();
    }

    #endregion

  }

}

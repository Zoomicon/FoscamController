//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: FoscamHDVideo.cs
//Version: 20151228

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
    private readonly string[] DEFAULT_VLC_OPTIONS = new string[]
    {
      "-I dummy",
      "--ignore-config",
      "--no-video-title",
      "--file-logging",
      "--logfile=log.txt",
      "--verbose=2",
      "--no-sub-autodetect-file",
      //"--rtsp-tcp", //needed to pass RTSP through a VPN
      //"--rtsp-frame-buffer-size=500000", //needed to avoid Live555 error when using --rtsp-tcp (RTCPInstance error: Hit limit when reading incoming packet over TCP. Increase "maxRTCPPacketSize")
      "--network-caching=500" //caching value for network resources in msec (needed for low frame lag - if broken frames need to increase it)
    };

    /* Other maybe useful parameters from https://wiki.videolan.org/VLC_command-line_help

      --rtsp-kasenna, --no-rtsp-kasenna
                                 Kasenna RTSP dialect(default disabled)
          Kasenna servers use an old and nonstandard dialect of RTSP.With this
          parameter VLC will try this dialect, but then it cannot connect to
          normal RTSP servers. (default disabled)
      --rtsp-wmserver, --no-rtsp-wmserver
                                 WMServer RTSP dialect(default disabled)
          WMServer uses a nonstandard dialect of RTSP.Selecting this parameter
          will tell VLC to assume some options contrary to RFC 2326 guidelines.
          (default disabled)
    */

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
      _libVlcPath = libVlcPath ?? DEFAULT_LIBVLC_PATH;
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
          };
        return _player;
      }
    }

    #endregion

    #region --- Methods ---

    public void StartVideo()
    {
      _player.BeginStop(() =>
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

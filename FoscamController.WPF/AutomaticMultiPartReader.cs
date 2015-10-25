//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: AutomaticMultiPartReader.cs
//Version: 20151025

using System;

namespace Camera.Foscam
{
  public class AutomaticMultiPartReader
  {

    #region --- Fields ---

    private MultiPartStream _mps;
    private bool _reading = false;
    private byte[] _currentPart;

    #endregion

    #region --- Initialization ---

    public AutomaticMultiPartReader(MultiPartStream stream)
    {
      _mps = stream;
    }

    #endregion

    #region --- Methods ---

    public async void StartProcessing()
    {
      _reading = true;
      while (_reading)
      {
        _currentPart = await _mps.NextPartAsync().ConfigureAwait(false);
        OnPartReady();
      }
    }

    public void StopProcessing()
    {
      _mps.Close();
      _reading = false;
    }

    #endregion

    #region --- Events ---

    public event EventHandler<PartReadyEventArgs> PartReady;

    protected virtual void OnPartReady()
    {
      if (PartReady != null)
      {
        PartReadyEventArgs args = new PartReadyEventArgs();
        args.Part = _currentPart;
        PartReady(this, args);
      }
    }

    #endregion

  }
}

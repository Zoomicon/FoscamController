//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: AutomaticMultiPartReader.cs
//Version: 20151228

using System;

namespace Mime.MultiPart
{
  public class AutomaticMultiPartReader
  {

    #region --- Fields ---

    private MultiPartStream _mps;
    private bool _reading = false;

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
        OnPartReady(await _mps.NextPartAsync().ConfigureAwait(false));
      _mps.Close(); //not calling Close from StopProcessing, waiting for current part to finish before calling Close (anyway, NextPartAsync returns null in case of error and OnPartReady is ignoring null argument)
    }

    public void StopProcessing()
    {
      _reading = false;
    }

    #endregion

    #region --- Events ---

    public event EventHandler<PartReadyEventArgs> PartReady;

    protected virtual void OnPartReady(byte[] currentPart)
    {
      if ((currentPart != null) && (PartReady != null))
        PartReady(this, new PartReadyEventArgs() { Part = currentPart });
    }

    #endregion

  }
}

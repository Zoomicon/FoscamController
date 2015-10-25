//Project: FoscamController (http://FoscamController.codeplex.com)
//Filename: MultiPartStream.cs
//Version: 20151026

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Camera.MJPEG
{
  public class MultiPartStream
  {

    #region --- Fields ---

    //Specs say that the body of each part and it's header are separated by two CRLFs
    private byte[] _seperatorBytes = Encoding.UTF8.GetBytes("\r\n\r\n");
    private byte[] _headerbytes = new byte[100];
    private Regex _contRegex = new Regex("Content-Length: (?<length>[0-9]+)\r\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private BinaryReader _reader;

    #endregion

    #region --- Initialization ---

    public MultiPartStream(Stream multipartStream)
    {
      _reader = new BinaryReader(new BufferedStream(multipartStream));
    }

    #endregion

    #region --- Cleanup ---

    public void Close()
    {
      _reader.Dispose();
    }

    #endregion

    #region --- Methods ---

    private int GetPartLength(string headerSection)
    {
      Match m = _contRegex.Match(headerSection);
      return int.Parse(m.Groups["length"].Value);
    }

    private bool SeperatorBytesExistsInArray(int position, byte[] array)
    {
      bool result = false;
      for (int i = position, j = 0; j < _seperatorBytes.Length; i++, j++)
      {
        result = array[i] == _seperatorBytes[j];
        if (!result)
          break;
      }
      return result;
    }

    private string ReadContentHeaderSection(BinaryReader stream)
    {
      // headers and content in multi part are separated by two \r\n
      bool found = false;

      int count = 4;
      stream.Read(_headerbytes, 0, 4);
      for (int i = 0; i < _headerbytes.Length; i++)
      {
        found = SeperatorBytesExistsInArray(i, _headerbytes);
        if (!found)
        {

          _headerbytes[count] = stream.ReadByte();
          count++;
        }
        else
          break;
      }
      return Encoding.UTF8.GetString(_headerbytes, 0, count);
    }

    public Task<byte[]> NextPartAsync()
    {
      return Task.Run(() =>
        {
          // every part has it's own headers
          string headerSection = ReadContentHeaderSection(_reader);
          // let's parse the header section for the content-length
          int length = GetPartLength(headerSection);
          // now let's get the image
          return _reader.ReadBytes(length);
        });
    }

    #endregion
  }
}

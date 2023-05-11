using Zen.System.FileHandling.Interfaces;

namespace Zen.System.FileHandling;

public class TapFileLoader : IFileLoader
{
    private int _position;

    public void Load(string filename)
    {
        _position = 0;

        var data = File.ReadAllBytes(filename);

        byte[] block;

        while ((block = ReadBlock(data)).Length > 0)
        {
            var isHeader = block[0] == 0;
        }
    }

    private byte[] ReadBlock(byte[] data)
    {
        if (_position > data.Length)
        {
            return Array.Empty<byte>();
        }

        var length = data[_position] | (data[_position + 1] << 8);

        if (_position + length > data.Length)
        {
            throw new Exceptions.FileLoadException("Block length is greater than file size.");
        }

        return data[(_position + 2)..(_position + 2 + length)];
    }
}
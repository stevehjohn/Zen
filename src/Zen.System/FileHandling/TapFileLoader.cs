namespace Zen.System.FileHandling;

public class TapFileLoader
{
    private int _position;

    private byte[] _data = [];

    public void StageFile(string filename)
    {
        _position = 0;

        _data = File.ReadAllBytes(filename);
    }

    public byte[] ReadNextBlock()
    {
        if (_position >= _data.Length)
        {
            return [];
        }

        var length = _data[_position] | (_data[_position + 1] << 8);

        if (_position + length > _data.Length)
        {
            throw new Exceptions.FileLoadException("Block length is greater than file size.");
        }

        var data = _data[(_position + 3)..(_position + 1 + length)];

        _position += length + 2;

        return data;
    }
}
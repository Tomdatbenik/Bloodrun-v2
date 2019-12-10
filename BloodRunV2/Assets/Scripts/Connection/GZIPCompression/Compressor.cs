using System.IO;
using System.IO.Compression;

public class Compressor
{
    public static byte[] Compress(byte[] data)
    {
        MemoryStream output = new MemoryStream();
        using (GZipStream dstream = new GZipStream(output, System.IO.Compression.CompressionLevel.Optimal))
        {
            dstream.Write(data, 0, data.Length);
        }

        return output.ToArray();
    }

    public static byte[] Decompress(byte[] data)
    {
        MemoryStream input = new MemoryStream(data);
        MemoryStream output = new MemoryStream();
        using (GZipStream dstream = new GZipStream(input, CompressionMode.Decompress))
        {
            dstream.CopyTo(output);
        }
        return output.ToArray();
    }
}

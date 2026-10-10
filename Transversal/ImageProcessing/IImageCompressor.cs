public interface IImageCompressor
{
    byte[] CompressToJpeg(byte[] originalImage, int maxWidth = 1000, int quality = 75);
}

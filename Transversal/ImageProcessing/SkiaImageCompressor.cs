using SkiaSharp;

public class SkiaImageCompressor : IImageCompressor
{
    public byte[] CompressToJpeg(byte[] originalImage, int maxWidth = 1000, int quality = 75)
    {
        using var original = SKBitmap.Decode(originalImage);
        if (original is null)
        {
            return originalImage;
        }

        var bitmapToEncode = original;
        SKBitmap? resized = null;

        if (original.Width > maxWidth)
        {
            var newHeight = (int)(original.Height * (maxWidth / (float)original.Width));
            resized = original.Resize(new SKImageInfo(maxWidth, newHeight), SKFilterQuality.Medium);
            if (resized is not null)
            {
                bitmapToEncode = resized;
            }
        }

        using var image = SKImage.FromBitmap(bitmapToEncode);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, quality);

        resized?.Dispose();

        return data.ToArray();
    }
}

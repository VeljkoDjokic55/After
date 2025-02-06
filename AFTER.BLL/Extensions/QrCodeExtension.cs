using Microsoft.Extensions.Configuration;
using Net.Codecrete.QrCodeGenerator;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace After.BLL.Extensions
{
    public static class QrCodeExtension
    {


        public static string GenerateImage(this QrCode qr,string url,string name)
        {
            qr = QrCode.EncodeText(url, QrCode.Ecc.Medium);
            string svg = qr.ToSvgString(4);
            // Write the SVG content to a file
            string filePath = $"tickets\\{name}.png";

            File.WriteAllText(filePath, svg);

            Console.WriteLine($"SVG file saved to {filePath}");

            return filePath;
        }

    }
}

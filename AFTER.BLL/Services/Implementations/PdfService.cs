using AFTER.BLL.Services.Interfaces;

using AFTER.Shared.Common;
using AFTER.Shared.Constants;

using System.Threading.Tasks;
using Net.Codecrete.QrCodeGenerator;
using After.BLL.Extensions;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using System;
using Svg;
using System.Drawing.Imaging;
using System.Drawing;
using SkiaSharp;
using Svg.Skia;

namespace AFTER.BLL.Services.Implementations
{
    public class PdfService : IPdfService
    {


        public PdfService()
        {
          
        }

       

        public async Task<ResponsePackage<string>> GeneratePdf(string qrCode, string fileName)
        {
            ResponsePackage<string> retval = new ResponsePackage<string>(ResponseStatus.OK, "Ticket saved Successfully.");
            QrCode qr = QrCode.EncodeText(string.Empty, QrCode.Ecc.Medium);
            var pathToQrCode = qr.GenerateImage(qrCode,fileName);
            MergeTicketWithQRCode("tickets\\after.png", pathToQrCode, pathToQrCode.Replace(qrCode, $"{qrCode}-PDF"));
            return retval;
        }
        //public static void MergeImages(string svgPath, string pngPath, string outputPath, int width, int height)
        //{
        //    try
        //    {

        //        // Load SVG
        //        SvgDocument svgDocument = SvgDocument.Open(svgPath);
        //        using (Bitmap svgBitmap = svgDocument.Draw(width, height))
        //        {
        //            // Load PNG
        //            using (Bitmap pngImage = new Bitmap(pngPath))
        //            {
        //                using (Graphics graphics = Graphics.FromImage(svgBitmap))
        //                {
        //                    // Draw PNG on top of the SVG
        //                    graphics.DrawImage(pngImage, new Rectangle(50, 50, pngImage.Width, pngImage.Height));
        //                }

        //                // Save merged image
        //                svgBitmap.Save(outputPath, ImageFormat.Png);
        //                Console.WriteLine("Merged image saved successfully!");
        //            }
        //        }
        //    }
        //    catch (Exception e) 
        //    {

        //    }
        //}

        static void MergeTicketWithQRCode(string pngPath, string svgPath, string outputPath)
        {
            // Load PNG
            using (var pngStream = File.OpenRead(pngPath))
            using (var pngBitmap = SKBitmap.Decode(pngStream))
            {
                int ticketWidth = pngBitmap.Width;
                int ticketHeight = pngBitmap.Height;

                // Load and resize SVG (QR Code)
                var svg = new SKSvg();
                svg.Load(svgPath);
                int qrSize = ticketHeight; // QR code should match the ticket height
                float scaleFactor = qrSize / (float)svg.Picture.CullRect.Width;

                // Final merged image dimensions
                int finalWidth = ticketWidth + qrSize;
                int finalHeight = ticketHeight;

                using (var surface = SKSurface.Create(new SKImageInfo(finalWidth, finalHeight)))
                {
                    var canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);

                    // Draw PNG (Ticket) on the left
                    canvas.DrawBitmap(pngBitmap, new SKRect(0, 0, ticketWidth, ticketHeight));

                    // Draw QR Code (SVG) on the right
                    canvas.Save();
                    canvas.Translate(ticketWidth, 0);  // Move to the right
                    canvas.Scale(scaleFactor);        // Scale QR code proportionally
                    canvas.DrawPicture(svg.Picture);
                    canvas.Restore();

                    // Save the merged image
                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    using (var fileStream = File.OpenWrite(outputPath))
                    {
                        data.SaveTo(fileStream);
                    }
                }
            }
        }
    }
}

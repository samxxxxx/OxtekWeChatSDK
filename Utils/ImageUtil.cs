using ImageMagick;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Utils
{
    public class ImageUtil
    {
        //https://github.com/dlemstra/Magick.NET
        /// <summary>
        /// 将图片以水印的形式合成到背景图片中
        /// </summary>
        /// <param name="backgroundImagePath"></param>
        /// <param name="qrcodePath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void SetImage(string backgroundImagePath, string watermarkImagePath, int x, int y, int watermarkWidth = -1, int watermarkHeight = -1)
        {
            //Bitmap bmp = new Bitmap(filePath);
            //Graphics g = Graphics.FromImage(bmp);

            //using (MagickImageCollection images = new MagickImageCollection())
            //{
            //    // Add the first image
            //    MagickImage first = new MagickImage(backgroundImagePath);
            //    images.Add(first);

            //    // Add the second image
            //    MagickImage second = new MagickImage(qrcodePath);
            //    images.Add(second);

            //    // Create a mosaic from both images
            //    using (IMagickImage result = images.Mosaic())
            //    {
            //        // Save the result
            //        result.Write(saveFileName);
            //    }
            //}
            // Read image that needs a watermark
            using (MagickImage image = new MagickImage(backgroundImagePath))
            {
                // Read the watermark that will be put on top of the image
                using (MagickImage watermark = new MagickImage(watermarkImagePath))
                {
                    // Draw the watermark in the bottom right corner
                    //image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);

                    // Optionally make the watermark more transparent
                    //watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);

                    // Or draw the watermark at a specific location
                    //缩放图片大小
                    if (watermarkWidth != -1 && watermarkHeight != -1)
                        watermark.Resize(watermarkWidth, watermarkHeight);

                    image.Composite(watermark, x, y, CompositeOperator.Src);
                }

                // Save the result
                image.Write(backgroundImagePath);
            }
        }
        /// <summary>
        /// 在图片上添加文字
        /// </summary>
        /// <param name="img">要设置的图片</param>
        /// <param name="text">要设置的文字</param>
        /// <param name="x">文字顶部x的座标</param>
        /// <param name="y">文字底部y的座标</param>
        /// <param name="fontSize"></param>
        public static void SetText(string img, string text, int x, int y, int fontSize)
        {

            using (MagickImage image = new MagickImage(img))
            {
                new Drawables()
                 // Draw text on the image
                 .FontPointSize(fontSize)
                 .Font("仿宋")
                 .StrokeColor(new MagickColor("yellow"))
                 .FillColor(MagickColors.Orange)
                 .TextAlignment(TextAlignment.Left)
                 .Text(x, y, text)
                 .TextEncoding(Encoding.UTF8)
                 // Add an ellipse
                 .StrokeColor(new MagickColor(0, Quantum.Max, 0))
                 .FillColor(MagickColors.SaddleBrown)
                 //.Ellipse(256, 96, 192, 8, 0, 360)
                 .Draw(image);

                image.Write(img);
            }

        }
    }
}

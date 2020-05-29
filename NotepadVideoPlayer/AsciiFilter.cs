using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotepadVideoPlayer
{
    class AsciiFilter
    {
        private VideoFrameProvider source;
        private float scale;

        public AsciiFilter(VideoFrameProvider source, float scale)
        {
            this.source = source;
            this.scale = scale;
        }

        public string GetNextFrame()
        {
            Bitmap nextFrame = source.GetNextFrame();
            if (nextFrame != null)
            {
                return ConvertToAscii(ResizeBitmap(nextFrame, scale, InterpolationMode.Low));
            }
            return null;
        }

        // convert frames to ascii
        private static string ConvertToAscii(Bitmap image)
        {
            string[] _AsciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", " " };

            Boolean toggle = false;

            StringBuilder sb = new StringBuilder();
            for (int h = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    Color pixelColor = image.GetPixel(w, h);

                    //Average out the RGB components to find the Gray Color
                    int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(red, green, blue);

                    //Use the toggle flag to minimize height-wise stretch
                    if (!toggle)
                    {
                        int index = (grayColor.R * 10) / 255;
                        sb.Append(_AsciiChars[index]);
                    }
                }

                if (!toggle)
                {
                    sb.Append("\r\n");
                    toggle = true;
                }

                else
                {
                    toggle = false;
                }
            }
            return sb.ToString();
        }

        // resize image
        private static Bitmap ResizeBitmap(Image source, float scale, InterpolationMode quality)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            // Figure out the new size.
            var width = (int)(source.Width * scale);
            var height = (int)(source.Height * scale);

            // Create the new bitmap.
            // Note that Bitmap has a resize constructor, but you can't control the quality.
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = quality;
                g.DrawImage(source, new Rectangle(0, 0, width, height));
                g.Save();
            }

            return bmp;
        }
    }
}

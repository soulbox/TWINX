using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PrintoothCore.Devices.Bases
{
    public abstract class DeviceManagerBase<ModelBase>
    {
        public DeviceManagerBase(ModelBase model)
        {
            Model = model;
        }
        internal ModelBase Model { get; set; }
        internal int FontAColumn { get; set; }
        internal int FontBColumn { get; set; }
        internal int ImageMultiplier { get; set; }
        internal byte[] Reciept { get; set; }
        internal abstract byte[] RecieptFontA();
        internal abstract byte[] RecieptFontB();
        public abstract byte[] GetReciept();
        public static SKBitmap GetFromResource(string imagename)
        {
            var assembly = typeof(Model.ModelBase).GetTypeInfo().Assembly;
            string resourceID = $"PrintoothCore.Images.{imagename}";
            var resStream = assembly.GetManifestResourceStream(resourceID);
            return SKBitmap.Decode(resStream);
        }
        internal byte[] Getlogo( SKBitmap Bitmap, int Multiplier)
        {
            var bitmap = Bitmap;
            var threshold = 127;
            var index = 0;
            double multiplier = Multiplier; // this depends on your printer model. 
            double scale = (double)(multiplier / (double)bitmap.Width);
            int xheight = (int)(bitmap.Height * scale);
            int xwidth = (int)(bitmap.Width * scale);
            var dimensions = xwidth * xheight;
            var dots = new BitArray(dimensions);
            for (var y = 0; y < xheight; y++)
            {
                for (var x = 0; x < xwidth; x++)
                {
                    var _x = (int)(x / scale);
                    var _y = (int)(y / scale);
                    var color = bitmap.GetPixel(_x, _y);
                    var luminance = (int)(color.Red * 0.3 + color.Green * 0.59 + color.Blue * 0.11);
                    dots[index] = (luminance < threshold);
                    index++;
                }
            }



            BitmapData data = new BitmapData()
            {
                Dots = dots,
                Height = (int)(bitmap.Height * scale),
                Width = (int)(bitmap.Width * scale)
            };

            BitArray dotss = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write((char)0x1B);
            bw.Write('@');

            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)24);

            while (offset < data.Height)
            {
                bw.Write((char)0x1B);
                bw.Write('*');         // bit-image mode
                bw.Write((byte)33);    // 24-dot double-density
                bw.Write(width[0]);  // width low byte
                bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }

                        bw.Write(slice);
                    }
                }
                offset += 24;
                bw.Write((char)0x0A);
            }
            // Restore the line spacing to the default of 30 dots.
            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)30);

            bw.Flush();
            byte[] bytes = stream.ToArray();
            return bytes;
        }
        internal  List<string> Wrap(string text, int margin)
        {
            int start = 0, end;
            var lines = new List<string>();
            text = Regex.Replace(text, @"\s", " ").Trim();

            while ((end = start + margin) < text.Length)
            {
                while (text[end] != ' ' && end > start)
                    end -= 1;

                if (end == start)
                    end = start + margin;

                lines.Add(text.Substring(start, end - start));
                start = end + 1;
            }

            if (start < text.Length)
                lines.Add(text.Substring(start));

            return lines;
        }
        class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}

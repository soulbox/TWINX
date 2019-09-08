using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Model
{
    public class Teknisyen : IAdSoyad
    {

        public string AdSoyad { get; set; } = "Bekir Topuz";
        public SKBitmap Bitmap { get; set; } = Utils.Utils.GetFromResource("Teknisyen.png");

    }
}

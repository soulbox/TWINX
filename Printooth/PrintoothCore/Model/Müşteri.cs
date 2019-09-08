using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Model
{
    public class Müşteri : IAdSoyad, IAdres, ITel
    {

        public string Tel { get; set; } = "0212 211 86 44 - 0532 464 00 52";
        public string Adress { get; set; } = "Dap Vadisi Z Ofis, Kağıthane Cd. Seçkin Sk. 2-4/A No: 79 Kağıthane / İstanbul";
        public string AdSoyad { get; set; } = "Bekir Topuz";
        public string Şikayet { get; set; } = "Dondurucu bölümü kapısı hasarlı";
        public SKBitmap Bitmap { get; set; } = Utils.Utils.GetFromResource("musteri.png");

    }
}

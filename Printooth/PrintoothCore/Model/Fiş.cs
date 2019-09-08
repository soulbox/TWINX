using SkiaSharp;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using PrintoothCore.Utils;
namespace PrintoothCore.Model
{

    public class Fiş
    {
        public Fiş()
        {
            Firma = new Firma();
            Müşter = new Müşteri();
            Teknisyen = new Teknisyen();
            Ücret = new Ücret();
            Ürün = new Ürün();
            Ziyaret = new Ziyaret();
            //var assembly = typeof(Fiş).GetTypeInfo().Assembly;
            //string resourceID = "PrintoothCore.Images.logo2.png";
            //var resStream = assembly.GetManifestResourceStream(resourceID);
            //Bitmap = Utils.Utils.GetFromResource("logo2.png");


        }
        public SKBitmap Bitmap { get; set; } = Utils.Utils.GetFromResource("krank.png");
        public Firma Firma { get; set; }
        public Müşteri Müşter { get; set; }
        public Teknisyen Teknisyen { get; set; }
        public Ücret Ücret { get; set; }
        public Ürün Ürün { get; set; }
        public Ziyaret Ziyaret { get; set; }


    }


}

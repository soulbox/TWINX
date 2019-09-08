using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Devices
{
    using static ESCPOS.Commands;
    using ESCPOS;
    using ESCPOS.Utils;
    using System.Linq;
    using static Utils.Utils;
    public class Twinix : DeviceBase
    {
        Model.Fiş Fiş;
        readonly int FontAColumn, FontBcolumn, ImageMultiplier;
        public Twinix(Model.Fiş fiş)
        {           
            Fiş = fiş;
            FontAColumn = 32 ;
            FontBcolumn = 42;
            ImageMultiplier = 380;
            Reciept = RecieptFontA();
        }
        byte[] RecieptFontA()
        {
            var lineA = new string('-', FontAColumn).ToBytes();
            Reciept = Reciept.Add(
                Fiş.Bitmap.Getlogo(ImageMultiplier),
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Center),
                LF,
                string.Join("\n", Wrap(Fiş.Firma.Ünvan, FontAColumn)).ToBytes(),
                SelectPrintMode(PrintMode.Reset),
                LF,
                string.Join("\n", Wrap(Fiş.Firma.Adress, FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap(Fiş.Firma.Tel, FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                PrintBarCode(BarCodeType.CODE128, Fiş.Firma.Barcode, 54),
                Fiş.Firma.Barcode.ToBytes(),
                LF,
                lineA,
                LF,
                SelectJustification(Justification.Left),
                "Müşteri Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"Adı Soyadı:{Fiş.Müşter.AdSoyad}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Telefon   :{Fiş.Müşter.Tel}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Adres     :{Fiş.Müşter.Adress}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ziyaret Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"İşlem / Teslim Tarihi:{Fiş.Ziyaret.TeslimTarihi}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"İşlem Bitiş Zamanı   :{Fiş.Ziyaret.BitişZamanı}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Şikayeti".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Fiş.Müşter.Şikayet}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ürün Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Fiş.Ürün.ÜrünBilgileri
                .Select(x => string.Format("{0}\n", string.Join("\n", Wrap(x, FontAColumn)))).ToArray()).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ücret Bilgileri".ToBytes(),
                LF, LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Fiş.Ücret.Bilgiler.Select(x => string.Format("{0,-19}{1,10:N2} TL\n", x.Servis, x.Fiyat)).ToArray()).ToBytes(),
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Right), "Toplam".ToBytes(),
                LF,
                string.Format("{0:N2} TL", Fiş.Ücret.Bilgiler.Sum(x => x.Fiyat)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectJustification(Justification.Left),
                "Teknisyen Adı ve İmzası".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Fiş.Teknisyen.AdSoyad}", FontAColumn)).ToBytes(),
                LF, 
                Fiş.Teknisyen.Bitmap.Getlogo(ImageMultiplier),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Adı ve İmzası".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Fiş.Müşter.AdSoyad}", FontAColumn)).ToBytes(),
                LF,
                Fiş.Müşter.Bitmap.Getlogo(ImageMultiplier),
                LF,
                lineA,
                LF, LF,
                SelectJustification(Justification.Center),
                string.Join("\n", Wrap(Fiş.Firma.Adress, FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap(Fiş.Firma.Tel, FontAColumn)).ToBytes(),
                LF,
                PrintQRCode(Fiş.Firma.Barcode, QRCodeModel.Model2, QRCodeCorrection.Percent7, QRCodeSize.Large),
                LF,
                CarriageReturn,
                CarriageReturn,
                CarriageReturn
                );


            return Reciept;
        }       

 
    }
}

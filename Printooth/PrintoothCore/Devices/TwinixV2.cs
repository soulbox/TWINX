using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Devices
{
    using static ESCPOS.Commands;
    using ESCPOS;
    using ESCPOS.Utils;
    using System.Linq;
    public class Twinix : Bases.DeviceManagerBase<Model.Fiş>
    //where Modal :Model.ModelBase
    {
        public Twinix(Model.Fiş modal)
            : base(modal)
        {     // char width                   
            FontAColumn = 32;
            FontBColumn = 42;
            ImageMultiplier = 380; //380 dots max
        }
        public override byte[] GetReciept()
        {
            return RecieptFontA();
        }

        internal override byte[] RecieptFontA()
        {
            var lineA = new string('-', FontAColumn).ToBytes();
            Reciept = Reciept.Add(
                Getlogo(Model.Firma.Bitmap, ImageMultiplier),
                //Fiş.Bitmap.Getlogo(ImageMultiplier),
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Center),
                LF,
                string.Join("\n", Wrap(Model.Firma.Ünvan, FontAColumn)).ToBytes(),
                SelectPrintMode(PrintMode.Reset),
                LF,
                string.Join("\n", Wrap(Model.Firma.Adress, FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap(Model.Firma.Tel, FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                PrintBarCode(BarCodeType.CODE128, Model.Firma.Barcode, 54),
                Model.Firma.Barcode.ToBytes(),
                LF,
                lineA,
                LF,
                SelectJustification(Justification.Left),
                "Müşteri Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"Adı Soyadı:{Model.Müşter.AdSoyad}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Telefon   :{Model.Müşter.Tel}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Adres     :{Model.Müşter.Adress}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ziyaret Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"İşlem / Teslim Tarihi:{Model.Ziyaret.TeslimTarihi}", FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"İşlem Bitiş Zamanı   :{Model.Ziyaret.BitişZamanı}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Şikayeti".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Model.Müşter.Şikayet}", FontAColumn)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ürün Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Model.Ürün.ÜrünBilgileri
                .Select(x => string.Format("{0}\n", string.Join("\n", Wrap(x, FontAColumn)))).ToArray()).ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ücret Bilgileri".ToBytes(),
                LF, LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Model.Ücret.Bilgiler.Select(x => string.Format("{0,-19}{1,10:N2} TL\n", x.Servis, x.Fiyat)).ToArray()).ToBytes(),
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Right), "Toplam".ToBytes(),
                LF,
                string.Format("{0:N2} TL", Model.Ücret.Bilgiler.Sum(x => x.Fiyat)).ToBytes(),
                LF,
                lineA,
                LF,
                SelectJustification(Justification.Left),
                "Teknisyen Adı ve İmzası".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Model.Teknisyen.AdSoyad}", FontAColumn)).ToBytes(),
                LF,
                //Fiş.Teknisyen.Bitmap.Getlogo(ImageMultiplier),
                Getlogo(Model.Teknisyen.Bitmap, ImageMultiplier),

                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Adı ve İmzası".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Wrap($"{Model.Müşter.AdSoyad}", FontAColumn)).ToBytes(),
                LF,
                //Fiş.Müşter.Bitmap.Getlogo(ImageMultiplier),
                Getlogo(Model.Müşter.Bitmap, ImageMultiplier),

                LF,
                lineA,
                LF, LF,
                SelectJustification(Justification.Center),
                string.Join("\n", Wrap(Model.Firma.Adress, FontAColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap(Model.Firma.Tel, FontAColumn)).ToBytes(),
                LF,
                PrintQRCode(Model.Firma.Barcode, QRCodeModel.Model2, QRCodeCorrection.Percent30, QRCodeSize.Large),
                LF,
                CarriageReturn,
                CarriageReturn,
                CarriageReturn
                );

            return Reciept;
        }

        internal override byte[] RecieptFontB()
        {
            throw new NotImplementedException();

            //Reciept = Reciept.Add();
            //return Reciept;

        }
    }


}

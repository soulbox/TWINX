using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Devices
{
    using static ESCPOS.Commands;
    using ESCPOS;
    using ESCPOS.Utils;
    using System.Linq;
    using Utils;
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


        public override byte[] GetRecieptFontA()
        {
            return RecieptFontA();
        }

        public override byte[] GetRecieptFontB()
        {
            return RecieptFontB();

        }

        public byte[] TestBarcode(string barcode) => Reciept=Reciept
            .Add(SelectJustification(Justification.Center),
            PrintBarCode(BarCodeType.CODE128, barcode, 60),
            barcode.ToBytes(),
            CarriageReturn,
            CarriageReturn,
            CarriageReturn);
        public byte[] TestQrcode(string qrcode) => Reciept = Reciept
       .Add(SelectJustification(Justification.Center),
       PrintQRCode(qrcode,qrCodeSize:QRCodeSize.Large),
       CarriageReturn,
       CarriageReturn,
       CarriageReturn);

        internal override byte[] RecieptFontA()
        {
            Reciept = new byte[0];
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
                PrintBarCode(BarCodeType.CODE128, Model.Firma.Barcode, 120),

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
                string.Join("\n", Model.Ürün.ÜrünBilgileri.Select(x => string.Join("\n", Wrap(x, FontAColumn)))).ToENG().ToBytes(),
                LF,
                lineA,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ücret Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                string.Join("\n", Model.Ücret.Bilgiler.Select(x => string.Format("{0,-19}{1,10:N2} TL", x.Servis, x.Fiyat))).ToBytes(),
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
            //throw new NotImplementedException();
            Reciept = new byte[0];
            var lineB = new string('-', FontBColumn).ToBytes();
            Reciept = Reciept.Add(
                Getlogo(Model.Firma.Bitmap, ImageMultiplier),
                SelectCharacterFont(Font.B),
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Center),
                LF,
                string.Join("\n", Wrap(Model.Firma.Ünvan.ToENG(), FontBColumn)).ToBytes(),
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                SelectJustification(Justification.Center),
                LF,
                string.Join("\n", Wrap(Model.Firma.Adress.ToENG(), FontBColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap(Model.Firma.Tel.ToENG(), FontBColumn)).ToBytes(),
                LF,
                SelectCharacterFont(Font.B),
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                PrintBarCode(BarCodeType.CODE128, Model.Firma.Barcode, 60),
                LF,
                Model.Firma.Barcode.ToBytes(),
                LF,
                SelectCharacterFont(Font.B),
                SelectJustification(Justification.Left),

                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Bilgileri".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Wrap($"Adı Soyadı:{Model.Müşter.AdSoyad}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Telefon   :{Model.Müşter.Tel}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"Adres     :{Model.Müşter.Adress}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ziyaret Bilgileri".ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Wrap($"İşlem / Teslim Tarihi:{Model.Ziyaret.TeslimTarihi}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                string.Join("\n", Wrap($"İşlem Bitiş Zamanı   :{Model.Ziyaret.BitişZamanı}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Şikayeti".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Wrap($"{Model.Müşter.Şikayet}".ToENG(), FontBColumn)).ToBytes(),
                LF,
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ürün Bilgileri".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Model.Ürün.ÜrünBilgileri.Select(x => string.Join("\n", Wrap(x, FontBColumn)))).ToENG().ToBytes(),
                LF,
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Ücret Bilgileri".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Model.Ücret.Bilgiler.Select(x => string.Format("{0,-29}{1,10:N2} TL", x.Servis, x.Fiyat).ToENG())).ToBytes(),
                LF,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                SelectJustification(Justification.Right), 
                "Toplam".ToBytes(),
                LF,
                string.Format("{0:N2} TL", Model.Ücret.Bilgiler.Sum(x => x.Fiyat)).ToBytes(),
                LF,
                SelectCharacterFont(Font.B),
                SelectJustification(Justification.Left),
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Teknisyen Adı ve İmzası".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Wrap($"{Model.Teknisyen.AdSoyad.ToENG()}", FontBColumn)).ToBytes(),
                LF,
                Getlogo(Model.Teknisyen.Bitmap, ImageMultiplier),
                LF,
                SelectCharacterFont(Font.B),
                lineB,
                LF,
                SelectPrintMode(PrintMode.EmphasizedOn),
                "Müşteri Adı ve İmzası".ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                SelectCharacterFont(Font.B),
                string.Join("\n", Wrap($"{Model.Müşter.AdSoyad.ToENG()}", FontBColumn)).ToBytes(),
                LF,
                Getlogo(Model.Müşter.Bitmap, ImageMultiplier),
                LF,
                SelectCharacterFont(Font.B),
                lineB,
                LF, LF,
                SelectJustification(Justification.Center),
                string.Join("\n", Wrap(Model.Firma.Adress, FontBColumn)).ToENG().ToBytes(),
                LF,
                string.Join("\n", Wrap(Model.Firma.Tel, FontBColumn)).ToENG().ToBytes(),
                LF,
                SelectPrintMode(PrintMode.Reset),
                PrintQRCode(Model.Firma.Barcode, QRCodeModel.Model2, QRCodeCorrection.Percent30, QRCodeSize.Large),
                LF,
                CarriageReturn,
                CarriageReturn,
                CarriageReturn
                );

            return Reciept;

        }
    }


}

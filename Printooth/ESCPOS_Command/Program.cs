using PrinterUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESCPOS_Command
{
    using static ESCPOS.Commands;
    using ESCPOS;
    using ESCPOS.Utils;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            PrinterUtility.EscPosEpsonCommands.EscPosEpson obj = new PrinterUtility.EscPosEpsonCommands.EscPosEpson();
            byte[] bytes = new byte[0];

            var asdg = ESCPOS();
            string sep = new String('-', 32) + "\n";
            //bytes = PrintExtensions.AddBytes(bytes, obj.FontSelect.);
            //bytes = PrintExtensions.AddBytes(bytes, obj.Cols.ToString("5"));
            bytes = PrintExtensions.AddBytes(bytes, obj.Alignment.Center());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes(sep));
            bytes = PrintExtensions.AddBytes(bytes, obj.Separator());

            bytes = PrintExtensions.AddBytes(bytes, obj.CharSize.DoubleWidth2());
            bytes = PrintExtensions.AddBytes(bytes, obj.FontSelect.FontA());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("AVRUPA MERKEZ SERVİS HİZMETLERİ\n"));
            bytes = PrintExtensions.AddBytes(bytes, obj.CharSize.Nomarl());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("AVRUPA MERKEZ SERVİS HİZMETLERİ\n"));
            bytes = PrintExtensions.AddBytes(bytes, obj.CharSize.DoubleWidth2());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("AVRUPA VE ASYA ARIZA ONARIM EKİPLERİ MERKEZ SERVİS AMİRLİĞİ\n"));
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Avcılar / İstanbul\n"));
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("0212 690 64 44\n"));

            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes(sep));
            bytes = PrintExtensions.AddBytes(bytes, obj.QrCode.Print("5E327F1624", PrinterUtility.Enums.QrCodeSize.Medio));

            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Fiş No: 5E327F1624\n"));
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes(sep));

            bytes = PrintExtensions.AddBytes(bytes, obj.Alignment.Left());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Tüketici Bilgileri\n"));
            bytes = PrintExtensions.AddBytes(bytes, obj.CharSize.Nomarl());
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Adı Soyadı:Kadir Aygün\n"));
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Telefonu  :534 852 40 46\n"));
            bytes = PrintExtensions.AddBytes(bytes, Encoding.UTF8.GetBytes("Adresi    :Hamitler Mh. Melek Sk. Gül Apt. D:17 K.2 Osmangazi / Bursa\n"));


            bytes = PrintExtensions.AddBytes(bytes, obj.Alignment.Left());
            //bytes = PrintExtensions.AddBytes(bytes, obj.BarCode.Code128());
            bytes = PrintExtensions.AddBytes(bytes, obj.Lf());
            var a = string.Join("\n", Wrap("AVRUPA MERKEZ SERVİS HİZMETLERİ", 16));




            string converted = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            Console.WriteLine($"bytes=>[{converted}]{converted.Length}");

            Console.ReadLine();
        }
        static string ESCPOS()
        {

            var line = "--------------------------------".ToBytes();
            byte[] array = new byte[0];
            byte[] turkis = { 27, 116, 20 };       
            array = array.Add(SelectCharacterFont(Font.B));//seperator

            array = array.Add(SelectCharacterFont(Font.B), new string('-', 42).ToBytes());//seperator
            array = array.Add(SelectJustification(Justification.Center));
            array = array.Add(SelectCharacterFont(Font.A));
            array = array.Add(SelectPrintMode(PrintMode.EmphasizedOn));
            array = array.Add(string.Join("\n", Wrap("AVRUPA MERKEZ SERVİS HİZMETLERİ", 32)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("AVRUPA VE ASYA ARIZA ONARIM EKİPLERİ MERKEZ SERVİS AMİRLİĞİ", 32)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("Avcılar / İstanbul", 32)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("0212 690 64 44", 32)).ToBytes());
            array = array.Add(LF, line, LF);
            array = array.Add(PrintBarCode(BarCodeType.CODE128, "5E327F1624", 54));
            array = array.Add("Fiş No:5E327F1624".ToBytes());
            array = array.Add(LF, line, LF);
            array = array.Add(SelectJustification(Justification.Left));
            array = array.Add(string.Join("\n", Wrap("Tüketici Bilgileri", 32)).ToBytes());
            array = array.Add(SelectPrintMode(PrintMode.Reset));
            array = array.Add(SelectCharacterFont(Font.B), turkis);
            array = array.Add(LF, string.Join("\n", Wrap("Adı Soyadı:Kadir Aygün", 42)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("Telefonu  :534 852 40 46", 42)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("Adresi    :Hamitler Mh. Melek Sk. Gül Apt. D:17 K.2 Osmangazi / Bursa", 42)).ToBytes());
            array = array.Add(LF, line, LF);
            array = array.Add(SelectPrintMode(PrintMode.EmphasizedOn));
            array = array.Add(string.Join("\n", Wrap("Ziyaret Bilgileri", 32)).ToBytes());
            array = array.Add(SelectPrintMode(PrintMode.Reset));
            array = array.Add(SelectCharacterFont(Font.B), turkis);
            array = array.Add(LF, string.Join("\n", Wrap("Başvuru Tarihi       :17 Ekim 2017 Salı", 42)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("İşlem / Teslim Tarihi:18 Ekim 2017 Çarşamba", 42)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("İşlem Bitiş Zamanı   :15:08", 32)).ToBytes());
            array = array.Add(LF, string.Join("\n", Wrap("Müşteri Şikayeti     :Dondurucu bölümü kapısı hasarlı", 42)).ToBytes());
            array = array.Add(LF, line, LF);

            array = array.Add(LF, LF, LF, LF, LF ,line, LF);
           

            return Encoding.UTF8.GetString(array, 0, array.Length);
        }
        static List<string> Wrap(string text, int margin)
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
    }
}

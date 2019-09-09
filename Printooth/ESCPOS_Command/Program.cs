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
    using PrintoothCore.Devices;
    class Program
    {
        
        static void Main(string[] args)
        {
            var fişs = new PrintoothCore.Model.Fiş();
            //var tww = new TwinixV2();
            

            //DeviceManager<TwinixV2<Mo>> dev = new DeviceManager<TwinixV2>(fişs);


            //dev.GetReciept();
            
            
            //var str = Convert.ToBase64String(asd);
         
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

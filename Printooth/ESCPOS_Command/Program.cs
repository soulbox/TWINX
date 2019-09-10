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
            var tw = new Twinix(fişs);
            //var a = tw.test();

            Console.ReadLine();
         
        }

    }
}

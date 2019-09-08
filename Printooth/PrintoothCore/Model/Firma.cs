using System;
using System.Collections.Generic;

using System.Text;

namespace PrintoothCore.Model
{
    public class Firma:IAdres,ITel
    {
        public string Ünvan { get; set; } = "Krank Bilişim Teknolojileri Ltd. Şti.";
        public string Tel { get; set; } = "0212 211 86 44 - 0532 464 00 52";
        public string Adress { get; set; } = "Merkez Mh. Hasat Sk. No: 52/1 Şişli / İstanbul Şişli / İstanbul";
        public string Barcode { get; set; } = "105B3BB5B2 - 253311";
     
    }
}

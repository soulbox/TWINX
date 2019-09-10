using System;
using System.Collections.Generic;
using System.Text;

namespace PrintoothCore.Model
{
   public  class Ücret
    {
        public List<Data> Bilgiler { get; set; } = new List<Data>()
        {
            new Data("Bakım",35.8f),
            new Data("Servis Ücreti",10.8f),
            new Data("Yedek Parça Ücreti",120.2f),
            new Data("KDV %18",200.8f),

        };

        public class Data
        {
            public Data(string Key,float Value)
            {
                Servis = Key;
                Fiyat = Value;
            }
            public string  Servis { get ; set; }
            public float  Fiyat { get; set; }

        }
    }
}

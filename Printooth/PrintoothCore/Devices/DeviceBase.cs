using PrintoothCore.Model;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
namespace PrintoothCore.Devices
{
    public class DeviceBase : IDeviceBase
    {
        public byte[] Reciept { get; set; } = new byte[0];

    }


}

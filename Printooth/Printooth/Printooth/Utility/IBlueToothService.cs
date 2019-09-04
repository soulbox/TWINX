using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Printooth.Utility
{
    public interface IBlueToothService
    {
        IList<string> GetDeviceList();
        Task Print(string deviceName, string text);
    }
}

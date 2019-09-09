using Printooth.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


namespace Printooth
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class PrintPageViewModel
    {

        private readonly IBlueToothService _blueToothService;

        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;
            }
            set
            {
                _deviceList = value;
            }
        }

        private string _printMessage;
        public string PrintMessage
        {
            get
            {
                return _printMessage;
            }
            set
            {
                _printMessage = value;
            }
        }

        private string _selectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }

        /// <summary>
        /// Print text-message
        /// </summary>
 
        public Task Print()
        {


            return _blueToothService.Print(SelectedDevice, PrintMessage);
        }
        public Task Print(byte[] bytes)
        {


            return _blueToothService.Print(SelectedDevice, bytes);
        }
        public PrintPageViewModel()
        {
            _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();
        }

        /// <summary>
        /// Get Bluetooth device list with DependencyService
        /// </summary>
        void BindDeviceList()
        {

            var list = _blueToothService.GetDeviceList();
            DeviceList.Clear();
            if (list != null)
            {
                foreach (var item in list)
                    DeviceList.Add(item);
            }
            else
                DeviceList.Add("Bağlanılacak Cihaz Yok");
        }
    }
}

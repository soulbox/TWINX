﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Printooth.Droid.Utility;
using Printooth.Utility;
[assembly: Xamarin.Forms.Dependency(typeof(AndroidBlueToothService))]
namespace Printooth.Droid.Utility
{
    class AndroidBlueToothService : IBlueToothService
    {
        /// <summary>
        /// We have to use local device Bluetooth adapter.
        /// BondedDevices returns BluetoothDevice collection anyway I need to take just device name.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetDeviceList()
        {
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                var btdevice = bluetoothAdapter?.BondedDevices.Select(i => i.Name).ToList();
                return btdevice;
            }
        }

        /// <summary>
        /// We have to use local device Bluetooth adapter.
        /// We need to find Bluetooth Device with selected device name.
        /// Now, we use BluetoothSocket class with most common UUID
        /// Try to connect BluetoothSocket then convert your text-message to bytearray
        /// Last step write your bytearray by way of bluetoothSocket
        /// </summary>
        /// <param name="deviceName">Selected deviceName</param>
        /// <param name="text">My printed text-message</param>
        /// <returns></returns>
        public async Task Print(string deviceName, string text)
        {
            try
            {


                using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
                {
                    BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                              where bd?.Name == deviceName
                                              select bd).FirstOrDefault();
                    try
                    {
                        using (BluetoothSocket bluetoothSocket = device?
                            .CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                        {
                            bluetoothSocket?.Connect();
                            byte[] buffer = Encoding.UTF8.GetBytes(text);
                            bluetoothSocket?.OutputStream.Write(buffer, 0, buffer.Length);                            
                            bluetoothSocket.Close();
                        }
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        public Task Print(string deviceName, byte[] bytes)
        {
            try
            {


                using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
                {
                    BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                              where bd?.Name == deviceName
                                              select bd).FirstOrDefault();
                    try
                    {
                        using (BluetoothSocket bluetoothSocket = device?
                            .CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                        {
                            bluetoothSocket?.Connect();
                            byte[] buffer = bytes;
                            bluetoothSocket?.OutputStream.Write(buffer, 0, buffer.Length);
                            bluetoothSocket.Close();
                        }
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }
                }
        }
            catch (Exception exp)
            {

                throw exp;
            }
            return Task.FromResult<object>(null);
        }
    }

}
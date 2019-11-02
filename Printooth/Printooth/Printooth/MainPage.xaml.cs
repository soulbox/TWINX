using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Printooth
{
    using static ESCPOS.Commands;
    using ESCPOS;
    using ESCPOS.Utils;
    using System.Text.RegularExpressions;
    using PrintoothCore.Model;
    using System.Reflection;

    public partial class MainPage : ContentPage
    {
        PrintPageViewModel prinrpage = new PrintPageViewModel();

        public MainPage()
        {
            InitializeComponent();
            prinrpage.SelectedDevice = "BT-II";
            picker.BindingContext = prinrpage;
            var assembly = typeof(Fiş).GetTypeInfo().Assembly;
            string resourceID = "PrintoothCore.Images.logo2.png";
            var resStream = assembly.GetManifestResourceStream(resourceID);
            mybitimage.Source = ImageSource.FromResource(resourceID, assembly);
            txtText.Text = "105B3BB5B2 - 253311";
        }

        private async void btnTextPrint(object sender, EventArgs e)
        {
            //var fiş = new PrintoothCore.Model.Fiş();
            //PrintoothCore.Devices.Twinix tw = new PrintoothCore.Devices.Twinix(fiş);
            //var yaz = tw.GetReciept();
            prinrpage.PrintMessage = txtText.Text;
            var yaz = tw.TestBarcode(txtText.Text,Convert.ToInt32(lblbarhigh.Text));

            await prinrpage.Print(yaz);
        }

        PrintoothCore.Devices.Twinix tw = new PrintoothCore.Devices.Twinix(new PrintoothCore.Model.Fiş());
        private async void btnFontA(object sender, EventArgs e)
        {
            var yaz = tw.GetRecieptFontA();
            await prinrpage.Print(yaz);
        }

        private async void btnFontB(object sender, EventArgs e)
        {
            var yaz = tw.GetRecieptFontB();
            await prinrpage.Print(yaz);
        }

        private async void BtnImageClick(object sender, EventArgs e)
        {
            await prinrpage.Print(tw.TestImage());
        }
    }


}

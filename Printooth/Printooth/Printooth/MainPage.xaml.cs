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

        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            var fiş = new PrintoothCore.Model.Fiş();
            PrintoothCore.Devices.Twinix tw = new PrintoothCore.Devices.Twinix(fiş);
            var yaz = tw.GetReciept();

            prinrpage.PrintMessage += "";

            await prinrpage.Print(yaz);
        }

      
    }


}

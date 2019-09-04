using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Printooth
{
    public partial class MainPage : ContentPage
    {
        PrintPageViewModel prinrpage = new PrintPageViewModel();

        public MainPage()
        {
            InitializeComponent();
            picker.BindingContext = prinrpage;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await prinrpage.Print();
        }
    }
}

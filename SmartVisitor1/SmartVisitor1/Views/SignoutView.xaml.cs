using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartVisitor1.Views
{
    public partial class SignoutView : ContentPage
    {
        public SignoutView()
        {
            InitializeComponent();
        }
        private void DoneButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LandingVIew());
        }
    }
}

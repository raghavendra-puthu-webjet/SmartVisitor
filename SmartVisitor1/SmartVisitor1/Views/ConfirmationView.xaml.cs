using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartVisitor1.Views;
using Xamarin.Forms;

namespace SmartVisitor1.Views
{
    public partial class ConfirmationView : ContentPage
    {
        public ConfirmationView()
        {
            InitializeComponent();
            Title = "FINISH";

            //VisitorName
        }

        private void DoneButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LandingVIew());
        }
    }
}

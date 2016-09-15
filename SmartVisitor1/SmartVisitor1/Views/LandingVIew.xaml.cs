using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartVisitor1.Views
{
    public partial class LandingVIew : ContentPage
    {
        public LandingVIew()
        {
            InitializeComponent();

            DateLabel.Text = DateTime.Now.Date.ToString("dd MMMM yyyy");
            TimeLabel.Text = DateTime.Now.ToString("hh:mm");
        }


        private void StartButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VisitorDetailsView());
        }
    }
}

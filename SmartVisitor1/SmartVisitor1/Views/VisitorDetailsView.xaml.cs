using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SmartVisitor1.Views
{
    public partial class VisitorDetailsView : ContentPage
    {
        public VisitorDetailsView()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            App.FirstName = FirstNameEntry.Text;
        }
    }
}

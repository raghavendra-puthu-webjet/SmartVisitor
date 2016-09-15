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

            DateLabel.Text = DateTime.Now.Date.ToString("dd MMMM yyyy");
            TimeLabel.Text = DateTime.Now.ToString("hh:mm");
        }

        private void NextButton_OnClicked(object sender, EventArgs e)
        {
            App.FirstName = FirstNameEntry.Text;
            App.LastName = LastNameEntry.Text;
            App.Company = CompanyEntry.Text;

            Navigation.PushAsync(new Employee());
        }
    }
}

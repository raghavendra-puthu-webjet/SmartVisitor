using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SmartVisitor1.Views
{
    public class VisitorEmployeeApiRequest
    {
        public EmployeeDetail Employee { get; set; }
        public VisitorDetail Visitor { get; set; }
    }

    public class VisitorDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }

    public class EmployeeDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public EmployeeDetail(string firstName, string lastName, string address, string mobileNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = address;
            this.PhoneNumber = mobileNumber;
        }

        public EmployeeDetail(){}
    }

    public partial class Employee : ContentPage
    {
        EmployeeDetail employeeSelected = new EmployeeDetail();

        // Dictionary to get Color from color name.
        Dictionary<string, EmployeeDetail> employees = new Dictionary<string, EmployeeDetail>
        {
            { "Raghavendra Puthu", new EmployeeDetail("Raghavendra", "Puthu", "raghavendra.puthu@webjet.com.au", "0433944255") },
            { "Vinh Ngo", new EmployeeDetail("Vinh", "Ngo", "vinh.ngo@webjet.com.au", "0403556223") },
            { "Tammy Helg", new EmployeeDetail("Tammy", "Helg", "Tammy.Helg@webjet.com.au", "0415511471") },
            { "Thamer AlMerry", new EmployeeDetail("Thamer", "AlMerry", "thamer.almerry@webjet.com.au", "0") },
            { "Robert Santoro", new EmployeeDetail("Robert", "Santaro", "robert.santaro@webjet.com.au", "0") },
            { "Shelley Beasley", new EmployeeDetail("Shelly", "Beasley", "shelley.beasley@webjet.com.au", "0407720772")},
            { "Micheal Sheehy", new EmployeeDetail("Micheal", "Sheehy", "micheal.sheehy@webjet.com.au" , "0412240081") }
        };

        public Employee()
        {
            Label header = new Label()
            {
                Text = "Who are you meeting with?",
                FontSize = 15,
                TextColor = Color.FromHex("#333"),
            };

            Picker picker = new Picker
            {
                Title = "Choose an employee",
                TextColor = Color.FromHex("#9B9B9B"),
                BackgroundColor = Color.FromHex("#F2F2F2"),
            };

            foreach (string colorName in employees.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            Label boxView = new Label()
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    
                    boxView.Text = "No one is selected";
                }
                else
                {
                    string employeeName = picker.Items[picker.SelectedIndex];
                    boxView.Text = employees[employeeName].FirstName;
                    employeeSelected = employees[employeeName];
                }
            };

            Button submitButton = new Button()
            {
                Text = "SUBMIT",
                FontSize = 21,
                BackgroundColor = Color.FromHex("#2DB300"),
                TextColor = Color.White,

            };
            submitButton.Clicked += OnSubmitButtonClicked;

            // Accomodate iPhone status bar.
            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                BackgroundColor=Color.White,
                Padding=20,
                Children =
                {
                    header,
                    picker,
                    submitButton
                }
            };

        }

        void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var success = RunAsync();
            Navigation.PushAsync(new ConfirmationView());
        }

        async Task<Boolean> RunAsync()
        {
            using (var client = new HttpClient())
            {
                VisitorEmployeeApiRequest request = new VisitorEmployeeApiRequest()
                {
                    Employee = employeeSelected,
                    Visitor = new VisitorDetail()
                    {
                        FirstName = App.FirstName,
                        LastName = App.LastName,
                        Company = App.Company
                    }

                };

                var str = JsonConvert.SerializeObject(request);
                var response = await client.PostAsync("http://smartvisitorbackend.azurewebsites.net/api/register",
                    new StringContent(str, Encoding.UTF8, "text/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

            }

            return false;
        }
    }
}

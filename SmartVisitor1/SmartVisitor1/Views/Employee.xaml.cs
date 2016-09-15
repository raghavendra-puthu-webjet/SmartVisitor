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
        public long PhoneNumber { get; set; }

        public EmployeeDetail(string firstName, string lastName, string address, long mobileNumber)
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
            { "Raghavendra Puthu", new EmployeeDetail("Raghavendra", "Puthu", "raghavendra.puthu@webjet.com.au", 0433944255) },
            { "Vinh Ngo", new EmployeeDetail("Vinh", "Ngo", "vinh.ngo@webjet.com.au", 1) },
            { "Tammy Helg", new EmployeeDetail() },
            { "Thamer AlMerry", new EmployeeDetail() },
            { "Robert Santoro", new EmployeeDetail() }
        };

        public Employee()
        {
            Label header = new Label()
            {
                Text = "Who are you meeting with?",
                FontSize = 12
            };

            Picker picker = new Picker
            {
                Title = "Choose an employee",
                VerticalOptions = LayoutOptions.CenterAndExpand
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
                HorizontalOptions = LayoutOptions.Center
            };
            submitButton.Clicked += OnSubmitButtonClicked;

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    picker,
                    boxView,
                    submitButton
                }
            };

        }

        void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            RunAsync();
        }

        async Task RunAsync()
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

            }
        }
    }
}

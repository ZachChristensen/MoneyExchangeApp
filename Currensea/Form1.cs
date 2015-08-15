using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace MoneyApiApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getData("NZD");
        }


        //http://api.fixer.io/2015-08-08?base=NZD


        async void getData(string Currency)
        {
            // Let the user know something is happening so that they don't think the
            // app has frozen.
            string error="";
            double temp;
            // We need a try/catch wrapped around our API resquest just incase an error occurs
            // while calling the weather API. If an error does occur the code inside the catch
            // statement is called, otherwise the app skips it and continues with the code.
            // Without a try/catch, if an error does occur the app would not know how to handle it
            // resulting in the app crashing. A try/catch prevents the app from crashing and can be
            // used to inform the user what went wrong.
            try
            {
                // Initializing HTTPClient.
                HttpClient client = new HttpClient();
                // Creating a new Weather Object to bind the results from our API call.
                CurrClass.RootObject rootObject1;
                CurrClass.RootObject rootObject2;
                CurrClass.RootObject rootObject3;
                // Call data from api for today.
                string x = await client.GetStringAsync(new Uri("http://api.fixer.io/latest?base=NZD"));
                // Bind data
                rootObject1 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
                // output data
                lbl_Curr1.Text = rootObject1.@base;
                lbl_Curr1Value.Text = 1.ToString();
                lbl_Curr2.Text = "JPY";
                lbl_Curr2Value.Text = rootObject1.rates.JPY.ToString();

                //30 days ago
                DateTime newDate = DateTime.Now;
                string Date30Str = newDate.AddDays(-30).ToString("yyyy-MM-dd");
                x = await client.GetStringAsync(new Uri("http://api.fixer.io/"+ Date30Str +"?base=NZD"));
                rootObject2 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
                temp = (rootObject2.rates.JPY - rootObject1.rates.JPY) / rootObject1.rates.JPY;
                lbl_30Percent.Text = Math.Round((temp * 100),2).ToString() + "%";
                lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.JPY, 2).ToString();
                if (temp > 0)
                {
                    pictureBox1up.Visible = true;
                }
                else if (temp < 0)
                {
                    pictureBox1down.Visible = true;
                }

                //360 days ago
                string Date360Str = newDate.AddYears(-1).ToString("yyyy-MM-dd");
                x = await client.GetStringAsync(new Uri("http://api.fixer.io/" + Date360Str + "?base=NZD"));
                rootObject3 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
                temp = (rootObject3.rates.JPY - rootObject1.rates.JPY) / rootObject1.rates.JPY ;
                lbl_360Percent.Text = Math.Round(( temp * 100), 2).ToString() + "%";
                lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.JPY, 2).ToString();
                if( temp > 0)
                {
                    pictureBox2up.Visible = true;
                }
                else if (temp < 0)
                {
                    pictureBox2down.Visible = true;
                }
                

                /*
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"C:\Users\Zach\Desktop\WriteLines2.txt", true))
                {
                    file.WriteLine("Test output");
                }
                */
            }

            catch (Exception ex)
            {
                // Assigning the string error to whatever exception occured.
                error = ex.Message;
            }
            // Checks if the error string is not set to null.
            if (error != "")
            {
                // Displays a message box informing the user if an error occured.
                MessageBox.Show("Error: " + error);
            }
        }
    }
}


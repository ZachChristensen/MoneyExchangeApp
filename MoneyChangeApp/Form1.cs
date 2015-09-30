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
		//From the combo box
		public string currentCurrency;
		// Creating a new Weather Object to bind the results from our API call.
		public CurrClass.RootObject rootObject1;
		public CurrClass.RootObject rootObject2;
		public CurrClass.RootObject rootObject3;


        public Form1()
        {
            InitializeComponent();
			currentCurrency = comboBox1.Text;
            getData("NZD");
        }


        //http://api.fixer.io/2015-08-08?base=NZD
        async void getData(string Currency)
        {
            // Let the user know something is happening so that they don't think the
            // app has frozen.
            string error="";
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
                
                // Call data from api for today.
                string x = await client.GetStringAsync(new Uri("http://api.fixer.io/latest?base=NZD"));
                // Bind data
                rootObject1 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
                setCurrentValue();

                //30 days ago
                DateTime newDate = DateTime.Now;
                string Date30Str = newDate.AddDays(-30).ToString("yyyy-MM-dd");
                x = await client.GetStringAsync(new Uri("http://api.fixer.io/"+ Date30Str +"?base=" + Currency));
                rootObject2 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
				set30Value();


                //360 days ago
                string Date360Str = newDate.AddYears(-1).ToString("yyyy-MM-dd");
                x = await client.GetStringAsync(new Uri("http://api.fixer.io/" + Date360Str + "?base=NZD"));
                rootObject3 = JsonConvert.DeserializeObject<CurrClass.RootObject>(x);
				set360Value();
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

		public void setCurrentValue()
		{
			// output data
			lbl_Curr1.Text = rootObject1.@base;
			lbl_Curr1Value.Text = 1.ToString();
			switch (currentCurrency)
			{
				case "JPN":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.JPY, 2).ToString();
					break;
				case "AUD":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.AUD, 2).ToString();
					break;
				case "GBP":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.GBP, 2).ToString();
					break;
				case "USD":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.USD, 2).ToString();
					break;
				case "HKD":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.HKD, 2).ToString();
					break;
				case "CAD":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.CAD, 2).ToString();
					break;
				case "EUR":
					lbl_Curr2Value.Text = Math.Round(rootObject1.rates.EUR, 2).ToString();
					break;
			}
		}

		public void set30Value()
		{
			double temp = 0.00;
			switch (currentCurrency)
			{
				case "JPN":
					temp = (rootObject2.rates.JPY - rootObject1.rates.JPY) / rootObject1.rates.JPY;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.JPY, 2).ToString();
					break;
				case "AUD":
					temp = (rootObject2.rates.AUD - rootObject1.rates.AUD) / rootObject1.rates.AUD;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.AUD, 2).ToString();
					break;
				case "GBP":
					temp = (rootObject2.rates.GBP - rootObject1.rates.GBP) / rootObject1.rates.GBP;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.GBP, 2).ToString();
					break;
				case "USD":
					temp = (rootObject2.rates.USD - rootObject1.rates.USD) / rootObject1.rates.USD;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.USD, 2).ToString();
					break;
				case "HKD":
					temp = (rootObject2.rates.HKD - rootObject1.rates.HKD) / rootObject1.rates.HKD;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.HKD, 2).ToString();
					break;
				case "CAD":
					temp = (rootObject2.rates.CAD - rootObject1.rates.CAD) / rootObject1.rates.CAD;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.CAD, 2).ToString();
					break;
				case "EUR":
					temp = (rootObject2.rates.EUR - rootObject1.rates.EUR) / rootObject1.rates.EUR;
					lbl_30Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value30.Text = Math.Round(rootObject2.rates.EUR, 2).ToString();
					break;
			}
			
			if (temp > 0)
			{
				pictureBox1down.Visible = true;
				pictureBox1up.Visible = false;
			}
			else if (temp < 0)
			{
				pictureBox1up.Visible = true;
				pictureBox1down.Visible = false;
			}
		}

		public void set360Value()
		{

			double temp = 0.00;
			switch (currentCurrency)
			{
				case "JPN":
					temp = (rootObject3.rates.JPY - rootObject1.rates.JPY) / rootObject1.rates.JPY;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.JPY, 2).ToString();
					break;
				case "AUD":
					temp = (rootObject3.rates.AUD - rootObject1.rates.AUD) / rootObject1.rates.AUD;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.AUD, 2).ToString();
					break;
				case "GBP":
					temp = (rootObject3.rates.GBP - rootObject1.rates.GBP) / rootObject1.rates.GBP;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.GBP, 2).ToString();
					break;
				case "USD":
					temp = (rootObject3.rates.USD - rootObject1.rates.USD) / rootObject1.rates.USD;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.USD, 2).ToString();
					break;
				case "HKD":
					temp = (rootObject3.rates.HKD - rootObject1.rates.HKD) / rootObject1.rates.HKD;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.HKD, 2).ToString();
					break;
				case "CAD":
					temp = (rootObject3.rates.CAD - rootObject1.rates.CAD) / rootObject1.rates.CAD;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.CAD, 2).ToString();
					break;
				case "EUR":
					temp = (rootObject3.rates.EUR - rootObject1.rates.EUR) / rootObject1.rates.EUR;
					lbl_360Percent.Text = Math.Round((temp * 100), 2).ToString() + "%";
					lbl_Curr2Value360.Text = Math.Round(rootObject3.rates.EUR, 2).ToString();
					break;
			}

			if (temp > 0)
			{
				pictureBox2down.Visible = true;
				pictureBox2up.Visible = false;
			}
			else if (temp < 0)
			{
				pictureBox2up.Visible = true;
				pictureBox2down.Visible = false;
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			currentCurrency = comboBox1.Text;
			setCurrentValue();
			set30Value();
			set360Value();
		}
    }
}


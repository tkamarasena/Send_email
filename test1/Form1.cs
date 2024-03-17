using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;
using System.Web;

namespace test1
{
    public partial class Form1 : Form
    {
        NetworkCredential login; //****************************************
        SmtpClient client;
        MailMessage message;

        Functions Con;
        public Form1()
        {
            InitializeComponent();
            Con = new Functions();
            ShowContacts();
            
        }

        public void ShowContacts()
        {
            try
            {
                string Query = "SELECT * FROM ContactsTbl";
                dgvContacts.DataSource = Con.GetData(Query); //take values from database and display in data gride view
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtEmail.Text == "" || txtMobileNumber.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string name = txtName.Text;
                    string email = txtEmail.Text;
                    string phone = txtMobileNumber.Text;

                    string Query = "INSERT INTO  ContactsTbl VALUES('{0}','{1}','{2}')";
                    Query = string.Format(Query, name, email, phone);
                    Con.SetData(Query);

                    ShowContacts(); //display updated table
                    MessageBox.Show("Details Is Added!!");
                    //clear text boxes
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtMobileNumber.Text = "";
                   
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int key = 0;
        private void dgvContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Show all data in text boxes which is selected row of data gride view
            txtName.Text = dgvContacts.SelectedRows[0].Cells[1].Value.ToString();
            txtEmail.Text = dgvContacts.SelectedRows[0].Cells[2].Value.ToString();
            txtMobileNumber.Text = dgvContacts.SelectedRows[0].Cells[3].Value.ToString();



            if (txtName.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(dgvContacts.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtName.Text == "" || txtEmail.Text == "" || txtMobileNumber.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string name = txtName.Text;  //*************************************************
                    string email = "tharindakaushalya778@gmail.com";  //mail reciever
                    string mobile = txtMobileNumber.Text;
                    string userName = "tharindakaushalya98"; //mail sender
                    string password = "App password"; // enter your app pasword generated for sender email
                    string smtp = "smtp.gmail.com";
                    
                    login = new NetworkCredential(userName,password); // txtusername, txtpasswrd
                    client = new SmtpClient(smtp);//txtsmtp
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Credentials = login;


                    //start send mail
                    message = new MailMessage { From = new MailAddress (userName + smtp.Replace("smtp.", "@"), "Employee Management System", Encoding.UTF8) }; //txtusrname+txtsmtp.repalace
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Test Subject";
                    message.Body = "Test Body";
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Priority=MailPriority.Normal;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallBack);
                    string userState = "Sending...";
                    client.SendAsync(message, userState);
                   
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtMobileNumber.Text = "";

                  


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private static void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)  //**************************************************
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send cancelled", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(e.Error != null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Your message has been successfully sent!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}

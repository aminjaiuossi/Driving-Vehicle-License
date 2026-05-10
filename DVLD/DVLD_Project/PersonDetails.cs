using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class PersonDetails : UserControl
    {
        private int _PersonId;
        public PersonDetails()
        {
            InitializeComponent();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonInformationEntry frm = new PersonInformationEntry(_PersonId);
            frm.ShowDialog();
            LoadPerson(_PersonId);
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {

        }

        public void LoadPerson(int personId)
            { 
            _PersonId = personId;
            try
            {
                PeopleServices service = new PeopleServices();
                DataRow person = service.GetPersonByPersonID(personId);
                if (person == null) return;

                // الاسم الكامل
                lblName.Text = $"{person["FirstName"]} {person["SecondName"]} {person["ThirdName"]} {person["LastName"]}";

                // الرقم الوطني
                lblNationalNo.Text = person["NationalNo"].ToString();

                // تاريخ الميلاد
                lblDateOfBirth.Text = Convert.ToDateTime(person["DateOfBirth"]).ToShortDateString();

                // العنوان
                lblAddress.Text = person["Address"].ToString();

                // الهاتف
                lblPhone.Text = person["Phone"].ToString();

                // الإيميل
                lblEmail.Text = person["Email"].ToString();

                lblPersonID.Text = personId.ToString();
                // الجنسية
                //lblNationality.Text = person["NationalityCountryID"].ToString(); // أو اسم الدولة لو عندك lookup

                // الجنس
                byte gender = Convert.ToByte(person["Gendor"]);

                if (gender == 0)
                    lblGendor.Text = "Male";
                else if (gender == 1)
                    lblGendor.Text = "Female";
                else
                    lblGendor.Text = "Unknown";
                // الصورة
                string imagePath = person["ImagePath"] == DBNull.Value ? null : person["ImagePath"].ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(Path.Combine(@"C:\DVLD_Pictures\", imagePath)))
                {
                    pbPerson.Image = Image.FromFile(Path.Combine(@"C:\DVLD_Pictures\", imagePath));
                }
                else
                {
                    // صورة افتراضية حسب الجنس
                    pbPerson.Image = null;
                }

                int countryId = Convert.ToInt32(person["NationalityCountryID"]);
                PeopleServices Country = new PeopleServices();
                lblCountry.Text = Country.GetCountryByID(countryId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading person data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void gbPersonDe_Enter(object sender, EventArgs e)
        {

        }
    }
}

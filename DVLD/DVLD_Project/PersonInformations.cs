using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static DVLD_Project.PersonInformationEntry;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace DVLD_Project
{
    public partial class PersonInformations : UserControl
    {
        public PersonInformations()
        {
            InitializeComponent();
        }

        public enMode CurrentMode { get; set; } = enMode.AddNew;
        public int PersonIDBeingEdited { get; set; } // لو update

        //public enum enMode { AddNew =1 , Update =2};

        public event Action<string, string> OnPersonSaved;

        private void PersonInformations_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                LoadCountries();
            }
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);

            if (pbPerson.Image != null)
                llRemove.Visible = true;

        }

        public string NationalNo { get { return txtNationalNumber.Text; } }

        public string NationalNumber
        {
            get { return txtNationalNumber.Text; }
        }

        private void LoadCountries()
        {
            DataTable dt = PeopleServices.GetAllCountries();

            cmbCountries.DataSource = dt;
            cmbCountries.DisplayMember = "CountryName";
            cmbCountries.ValueMember = "CountryID";

            // تحديد الأردن كخيار افتراضي
            foreach (DataRow row in dt.Rows)
            {
                if (row["CountryName"].ToString().Equals("Jordan", StringComparison.OrdinalIgnoreCase))
                {
                    cmbCountries.SelectedValue = row["CountryID"];
                    break;
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            pbPerson.Image = Properties.Resources.person_man1;
        }

        private void rdFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbPerson.Image = Properties.Resources.person_girl;
        }

        private void ValidateField(Control control, string message)
        {
            if (control is TextBox txt)
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                    errorProvider1.SetError(txt, message);
                else
                    errorProvider1.SetError(txt, "");
            }
            //else if (control is ComboBox cmb)
            //{
            //    if (cmb.SelectedIndex == -1)
            //        errorProvider1.SetError(cmb, message);
            //    else
            //        errorProvider1.SetError(cmb, "");
            //}

        }


        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            ValidateField(txtFirstName, "Please Enter First Name");
            //if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            //    errorProvider1.SetError(txtFirstName, "This Should Be Filled !!!");
            //else
            //    errorProvider1.SetError(txtFirstName, ""); 
        }

        private void txtSecondName_Leave(object sender, EventArgs e)
        {
            ValidateField(txtSecondName, "Please Second Enter Name");
            //if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            //    errorProvider1.SetError(txtSecondName, "This Should Be Filled !!!");
            //else
            //    errorProvider1.SetError(txtSecondName, "");
        }

        private void txtThirdName_Leave(object sender, EventArgs e)
        {
            ValidateField(txtThirdName, "Please Enter Third Name");
            //if (string.IsNullOrWhiteSpace(txtThirdName.Text))
            //    errorProvider1.SetError(txtThirdName, "This Should Be Filled !!!");
            //else
            //    errorProvider1.SetError(txtThirdName, "");
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            ValidateField(txtLastName, "Please Enter Last Name");
            //if (string.IsNullOrWhiteSpace(txtLastName.Text))
            //    errorProvider1.SetError(txtLastName, "This Should Be Filled !!!");
            //else
            //    errorProvider1.SetError(txtLastName, "");
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            //ValidateField(txtEmail, "Please Enter Your Email");


            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !txtEmail.Text.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                errorProvider1.SetError(txtEmail, "Email Should Be Ends With : @gmail.com");
            }
            else
                errorProvider1.SetError(txtEmail, "");
        }



        //private void txtNationalNumber_Leave(object sender, EventArgs e)
        //{
        //    //errorProvider1.SetError(txtNationalNumber, ""); // نمسح أي خطأ سابق أولاً

        //    //if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
        //    //{
        //    //    errorProvider1.SetError(txtNationalNumber, "Please Enter The National Number!");
        //    //}
        //    //else if (PeopleServices.IsNationalNuExist(txtNationalNumber.Text))
        //    //{
        //    //    errorProvider1.SetError(txtNationalNumber, "This National Number Already Exists!!!");
        //    //}
        //}


        private void txtPhone_Leave(object sender, EventArgs e)
        {
            ValidateField(txtPhone, "Please Enter Your Email");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.Close();
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            ValidateField(txtAddress, "Please Enter The Address");
        }

        private void txtNationalNumber_TextChanged(object sender, EventArgs e)
        {
            //if (PeopleServices.IsNationalNuExist(txtNationalNumber.Text))
            //{
            //    ValidateField(txtNationalNumber, "This National Number Is Already Exist !!!");
            //}
        }

        private void txtNationalNumber_Leave_1(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtNationalNumber, ""); // نمسح أي خطأ سابق أولاً

            if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                errorProvider1.SetError(txtNationalNumber, "Please Enter The National Number!");
            }
            else if (PeopleServices.IsNationalNuExist(txtNationalNumber.Text))
            {
                errorProvider1.SetError(txtNationalNumber, "This National Number Already Exists!!!");
            }
        }

        private string _SavedImagePath;

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "Select an Image";
                openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // تحميل الصورة في PictureBox بدون قفل
                    using (var img = Image.FromFile(openFile.FileName))
                    {
                        pbPerson.Image = new Bitmap(img);
                    }

                    // إنشاء المجلد إذا مش موجود
                    string folderPath = @"C:\DVLD_Pictures";
                    Directory.CreateDirectory(folderPath);

                    // اسم جديد للصورة
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(openFile.FileName);
                    _SavedImagePath = Path.Combine(folderPath, newFileName);

                    // نسخ مضمون بدون قفل
                    byte[] imageBytes = File.ReadAllBytes(openFile.FileName);
                    File.WriteAllBytes(_SavedImagePath, imageBytes);

                    // حفظ المسار في Tag
                    pbPerson.Tag = _SavedImagePath;
                    llRemove.Visible = true;

                    MessageBox.Show("Saved path: " + _SavedImagePath + "\nExists? " + File.Exists(_SavedImagePath));
                }
            }

        }

        private void _AddNew()
        {
            if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                errorProvider1.SetError(txtNationalNumber, "The National Number Should Be Entered!");
                return;
            }
            else
                errorProvider1.SetError(txtNationalNumber, "");

            // تحقق من الرقم الوطني
            if (PeopleServices.IsNationalNuExist(txtNationalNumber.Text))
            {
                MessageBox.Show("The National Number Already Exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // تجهيز البيانات
            string nationalNo = txtNationalNumber.Text;
            string firstName = txtFirstName.Text;
            string secondName = txtSecondName.Text;
            string thirdName = txtThirdName.Text;
            string lastName = txtLastName.Text;
            DateTime dateOfBirth = dtpDateOfBirth.Value;
            byte gender = rdMale.Checked ? (byte)0 : (byte)1;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            int nationalityCountryID = Convert.ToInt32(cmbCountries.SelectedValue);

            // حفظ الصورة
            string imagePath = null;
            if (pbPerson.Image != null)
            {
                string destFolder = @"C:\DVLD_Pictures\";
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);

                string newFileName = Guid.NewGuid().ToString() + ".jpg";
                string destPath = Path.Combine(destFolder, newFileName);
                pbPerson.Image.Save(destPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagePath = newFileName;
            }

            // الإدخال في قاعدة البيانات
            int personId = PeopleServices.AddPerson(
                nationalNo, firstName, secondName, thirdName, lastName,
                dateOfBirth, gender, address, phone, email,
                nationalityCountryID, imagePath
            );

            // إعادة رفع الحدث للفورم الرئيسي
            OnPersonSaved?.Invoke(txtNationalNumber.Text , nationalNo);

            MessageBox.Show("Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void _Update()
        {
            
            if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                errorProvider1.SetError(txtNationalNumber, "The National Number Should Be Entered!");
                return;
            }
            else
                errorProvider1.SetError(txtNationalNumber, "");

            // تحقق من الرقم الوطني مع الأخذ بعين الاعتبار الشخص الحالي
            if (PeopleServices.IsNationalNuExist(txtNationalNumber.Text))
            {
                //PeopleServices service = new PeopleServices();
                //DataRow row = PeopleServices.GetPersonByNationalNo(txtNationalNumber.Text);
                //if (row != null && row["NationalNo"] != NationalNoBeingEdited)
                //{
                //    MessageBox.Show("The National Number Already Exists for Another Person!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
            }

            // تحويل الجنس
            byte gender = rdMale.Checked ? (byte)0 : (byte)1;

            // تجهيز الصورة
            string imagePath = null;
            if (pbPerson.Image != null)
            {
                string destFolder = @"C:\DVLD_Pictures\";
                if (!Directory.Exists(destFolder))
                    Directory.CreateDirectory(destFolder);

                string newFileName = Guid.NewGuid().ToString() + ".jpg";
                string destPath = Path.Combine(destFolder, newFileName);
                pbPerson.Image.Save(destPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                imagePath = newFileName;
            }

            // التحديث
            PeopleServices.UpdatePerson(
    PersonIDBeingEdited, // معرف الشخص اللي بتحدثه
    txtNationalNumber.Text,
    txtFirstName.Text,
    txtSecondName.Text,
    txtThirdName.Text,
    txtLastName.Text,
    dtpDateOfBirth.Value,
    gender,
    txtAddress.Text,
    txtPhone.Text,
    txtEmail.Text,
    Convert.ToInt32(cmbCountries.SelectedValue),
    imagePath
);

            MessageBox.Show("Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // إعادة رفع الحدث للفورم الرئيسي
            //OnPersonSaved?.Invoke(txtNationalNumber.Text, PersonIDBeingEdited);

        
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CurrentMode == enMode.AddNew)
            {
                _AddNew();
                return;
            }
            else if (CurrentMode==enMode.Update)
            {
                _Update();
            }
            
        }

        public void LoadPersonData(int PersonID )
        {
            
            PeopleServices service = new PeopleServices();
            DataRow row = service.GetPersonByPersonID(PersonID);

            if (row == null)
            {
                MessageBox.Show("Person not found!");
                return;
            }

            txtNationalNumber.Text = row["NationalNo"].ToString();
            txtFirstName.Text = row["FirstName"].ToString();
            txtSecondName.Text = row["SecondName"].ToString();
            txtThirdName.Text = row["ThirdName"].ToString();
            txtLastName.Text = row["LastName"].ToString();
            txtAddress.Text = row["Address"].ToString();
            txtPhone.Text = row["Phone"].ToString();
            txtEmail.Text = row["Email"].ToString();

            if (row["DateOfBirth"] != DBNull.Value)
                dtpDateOfBirth.Value = Convert.ToDateTime(row["DateOfBirth"]);

            if (row["Gendor"] != DBNull.Value)
            {
                string g = row["Gendor"].ToString().Trim();

                rdMale.Checked = (g == "0" || g.Equals("Male", StringComparison.OrdinalIgnoreCase));
                rdFemale.Checked = (g == "1" || g.Equals("Female", StringComparison.OrdinalIgnoreCase));
            }

            if (row["NationalityCountryID"] != DBNull.Value)
                cmbCountries.SelectedValue = Convert.ToInt32(row["NationalityCountryID"]);

            string imageFile = row["ImagePath"]?.ToString();
            if (!string.IsNullOrEmpty(imageFile))
            {
                string fullPath = Path.Combine(@"C:\DVLD_Pictures\", imageFile);
                if (File.Exists(fullPath))
                {
                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        pbPerson.Image = Image.FromStream(fs);
                }
                else
                    pbPerson.Image = null;
            }
            else
                pbPerson.Image = null;
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (pbPerson.Image != null)
                {
                    // خذ المسار من الـ Tag
                    string pathToDelete = pbPerson.Tag as string;

                    if (!string.IsNullOrEmpty(pathToDelete) && File.Exists(pathToDelete))
                    {
                        // حذف الصورة من الهارد
                        File.Delete(pathToDelete);
                        MessageBox.Show("Image deleted successfully ✅");
                    }
                    else
                    {
                        MessageBox.Show("Image file not found ❌");
                    }

                    // تفريغ الـ PictureBox
                    pbPerson.Image = null;
                    pbPerson.Tag = null;
                    llRemove.Visible = false;

                    // تفريغ المتغير الاحتياطي
                    _SavedImagePath = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting image: " + ex.Message);
            }

        }
    }
}


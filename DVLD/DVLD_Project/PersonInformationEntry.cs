using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class PersonInformationEntry : Form
    {
        public enum enMode { AddNew = 1 , Update =2}
        public enMode Mode = enMode.AddNew;
        public PersonInformationEntry()
        {
            InitializeComponent();
            _PersonID = -1; // يعني Add mode
            
        }

        

        public delegate void PersonSavedHandler(string nationalNo); // فقط NationalNo
        public event PersonSavedHandler OnPersonSaved;

        
        private int _PersonID;
        private readonly int PersonIDBeingEdited;

        public PersonInformationEntry(int Personid)
        {
            InitializeComponent();
            _PersonID = Personid; // يعني Edit mode
            Mode = enMode.Update;
            //lblNational.Text = nationalno;
            Mode = enMode.Update;
            personInformations1.LoadPersonData(Personid);
            
        }
        private void PersonInformationEntry_Load(object sender, EventArgs e)
        {
            personInformations1.OnPersonSaved += (nationalNo, personId) =>
            {
                lblNational.Text = nationalNo;   
            };

            personInformations1.OnPersonSaved += (nationalNo, personId) =>
            {
                // إعادة رفع الحدث للفورم
                //OnPersonSaved?.Invoke(PersonIDBeingEdited);

            };

            if (_PersonID != -1)
            {
                lblTitle.Text = "Update Person";
                
                Mode = enMode.Update;
                personInformations1.CurrentMode = enMode.Update;
                personInformations1.PersonIDBeingEdited = _PersonID; // لازم يكون موجود عندك
                personInformations1.LoadPersonData(_PersonID); // تعبي الحقول
                lblNational.Text = personInformations1.NationalNo;
            }
            else
            {
                lblTitle.Text = "Add Person";
                Mode = enMode.AddNew;
            }

            personInformations1.OnPersonSaved += (nationalNo, personId) =>
            {
                lblNational.Text = nationalNo;
            };
           
        }
        

        private void personInformations1_Load(object sender, EventArgs e)
        {

        }
    }
}

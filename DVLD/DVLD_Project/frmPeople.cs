using Business_Logic_Layer;
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
    public partial class frmPeople : Form
    {
        public frmPeople()
        {
            InitializeComponent();
        }

        private void dgvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            //{
            //    // حدد الصف اللي انكبس عليه
            //    dgvPeople.ClearSelection();
            //    dgvPeople.Rows[e.RowIndex].Selected = true;
            //    dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells[e.ColumnIndex];

            //    // اعرض المنيو عند مكان الماوس
            //    contextMenuStrip1.Show(Cursor.Position);
            //}
        }

        public void LoadPeople()
        {
            DataTable dt = PeopleServices.GetAllPeople();
            dgvPeople.DataSource = dt;

            // تحديث عدد الصفوف
            lblNumberOfRows.Text = "# Records : " + (dgvPeople.Rows.Count - 1).ToString();
            cmbFilterItems.SelectedIndex = 0; // افتراضي None
            txtFilterValue.Visible = false;
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
            
            LoadPeople();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            PersonInformationEntry frm = new PersonInformationEntry();

            frm.OnPersonSaved += (nationalNo) =>
            {
                LoadPeople(); // عمل Refresh للقريد
            };

            frm.ShowDialog();
            LoadPeople();
        }

        private void dgvPeople_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // حدد الصف اللي انكبس عليه
                dgvPeople.ClearSelection();
                dgvPeople.Rows[e.RowIndex].Selected = true;
                dgvPeople.CurrentCell = dgvPeople.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // اعرض المنيو عند مكان الماوس
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int personId = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this person?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool isDeleted = PeopleServices.DeletePerson(personId);

                    if (isDeleted)
                    {
                        MessageBox.Show("Deleted Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPeople(); // لتحديث الـ DataGridView بعد الحذف
                    }
                    else
                    {
                        MessageBox.Show("Error while deleting person , there is data link to it !");
                    }
                }
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value.ToString());

                // افتح الفورم بحالة التعديل
                PersonInformationEntry frm = new PersonInformationEntry(PersonID);
                frm.ShowDialog();

                // بعد الإغلاق، نعمل تحديث للجدول
                LoadPeople();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFilterItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterItems.SelectedItem.ToString() == "None")
            {
                txtFilterValue.Visible = false;
                txtFilterValue.Text = "";
                LoadPeople();
            }
            else
            {
                txtFilterValue.Visible = true;
                txtFilterValue.Focus();
            }
        }

        private void ApplyFilter(string filterText)
        {
            string filterColumn = cmbFilterItems.SelectedItem.ToString();
            if (filterColumn == "None") return;

            DataTable dt = PeopleServices.GetAllPeople();
            DataView dv = dt.DefaultView;

            string[] numericColumns = { "PersonID", "NationalityCountryID", "Gendor" };

            try
            {
                if (numericColumns.Contains(filterColumn))
                {
                    if (int.TryParse(filterText, out int number))
                        dv.RowFilter = $"[{filterColumn}] = {number}";
                    else if (string.IsNullOrEmpty(filterText))
                        dv.RowFilter = ""; // إذا مسح النص → اعرض كل الصفوف
                    else
                        dv.RowFilter = "1=0"; // نص غير رقمي
                }
                else
                {
                    if (string.IsNullOrEmpty(filterText))
                        dv.RowFilter = ""; // كل الصفوف
                    else
                    {
                        string safe = filterText.Replace("'", "''");
                        dv.RowFilter = $"[{filterColumn}] LIKE '%{safe}%'";
                    }
                }

                dgvPeople.DataSource = dv;

                int rowCount = dgvPeople.Rows.Count;
                if (dgvPeople.AllowUserToAddRows && rowCount > 0) rowCount--;
                lblNumberOfRows.Text = "# Records : " + rowCount.ToString();
            }
            catch
            {
                LoadPeople();
            }
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (cmbFilterItems.SelectedItem == null) return;

            string filterText = txtFilterValue.Text.Trim();

            // لو النص فارغ → لا نرجع الفلتر لـ None، بس نفترض كل البيانات
            ApplyFilter(filterText);
        }

        
        private void lblNumberOfRows_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count > 0)
            {
                int PersonID = Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["PersonID"].Value.ToString());

                
                frmPersonDetails frm = new frmPersonDetails(PersonID);
                
                frm.ShowDialog();

                
                LoadPeople();
            }
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Implementing Soon !!!");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Implementing Soon !!!");
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            // إذا كان الفلتر الحالي هو "PersonID"
            if (cmbFilterItems.SelectedItem != null && cmbFilterItems.SelectedItem.ToString() == "PersonID")
            {
                // يمنع أي إدخال غير الأرقام أو Backspace
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonInformationEntry frm = new PersonInformationEntry();

            frm.OnPersonSaved += (nationalNo) =>
            {
                LoadPeople(); // عمل Refresh للقريد
            };

            frm.ShowDialog();
            LoadPeople();
        }
    }
}

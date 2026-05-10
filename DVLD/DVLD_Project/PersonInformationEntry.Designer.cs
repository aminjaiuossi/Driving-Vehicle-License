namespace DVLD_Project
{
    partial class PersonInformationEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonInformationEntry));
            this.lbl = new System.Windows.Forms.Label();
            this.lblNational = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.personInformations1 = new DVLD_Project.PersonInformations();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(49, 75);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(144, 17);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "National Number : ";
            // 
            // lblNational
            // 
            this.lblNational.AutoSize = true;
            this.lblNational.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNational.Location = new System.Drawing.Point(227, 75);
            this.lblNational.Name = "lblNational";
            this.lblNational.Size = new System.Drawing.Size(44, 17);
            this.lblNational.TabIndex = 2;
            this.lblNational.Text = "N /A ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(188, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 36);
            this.label2.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(360, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(82, 37);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // personInformations1
            // 
            this.personInformations1.Location = new System.Drawing.Point(1, 95);
            this.personInformations1.Name = "personInformations1";
            this.personInformations1.Size = new System.Drawing.Size(731, 404);
            this.personInformations1.TabIndex = 4;
            this.personInformations1.Load += new System.EventHandler(this.personInformations1_Load);
            // 
            // PersonInformationEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 505);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.personInformations1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNational);
            this.Controls.Add(this.lbl);
            this.Name = "PersonInformationEntry";
            this.Text = "Add / Edit Person";
            this.Load += new System.EventHandler(this.PersonInformationEntry_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblNational;
        private System.Windows.Forms.Label label2;
        private PersonInformations personInformations1;
        private System.Windows.Forms.Label lblTitle;
    }
}
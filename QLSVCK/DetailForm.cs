using QLSVCK.BLL;
using QLSVCK.DB;
using QLSVCK.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSVCK
{
    public partial class DetailForm : Form
    {
        public DetailForm(string MSSV = null)
        {
            InitializeComponent();
            this.MSSV = MSSV;
            setCBBHP();
            setCBBLSH();
            createData();
        }

        public delegate void MyDel();

        public MyDel d { get; set; }

        public string MSSV { get; set; }

        public void setCBBHP()
        {
            List<CBBItem> cBBItems = QLSVBLL.Instance.GetCBBItemsHP();
            cBBItems.Remove(cBBItems.First());
            comboBoxHP.Items.AddRange(cBBItems.ToArray());
        }

        public void setCBBLSH()
        {
            comboBoxLopSH.Items.AddRange(QLSVBLL.Instance.GetCBBItemsLSH().ToArray());
        }

        public void createData()
        {
            if(this.MSSV != null)
            {
                StudentDTO student = QLSVBLL.Instance.GetStudentByMSSV(this.MSSV);
                this.textBoxMSSV.Text = student.MSSV;
                this.textBoxMSSV.Enabled = false;
                this.textBox2.Text = student.Name;
                this.comboBoxLopSH.SelectedIndex = comboBoxLopSH.FindString(student.LopSH);
                this.comboBoxHP.SelectedIndex = comboBoxHP.FindString(student.CourseName);
                this.dateTimePickerNT.Value = student.ExamDate;
                this.textBoxDBT.Text = student.DBT.ToString();
                this.textBoxDGK.Text = student.DGK.ToString();
                this.textBoxDCK.Text = student.DCK.ToString();
                radioButtonNam.Checked = student.Genre;
                radioButtonNu.Checked = !student.Genre;
            }
            else
            {
                radioButtonNam.Checked = true;
                this.comboBoxLopSH.SelectedIndex = 0;
                this.comboBoxHP.SelectedIndex = 0;
            }
        }

        private void textBoxDBT_TextChanged(object sender, EventArgs e)
        {
            setTK(textBoxDBT.Text, textBoxDGK.Text, textBoxDCK.Text);
        }

        private void textBoxDGK_TextChanged(object sender, EventArgs e)
        {
            setTK(textBoxDBT.Text, textBoxDGK.Text, textBoxDCK.Text);
        }

        private void textBoxDCK_TextChanged(object sender, EventArgs e)
        {
            setTK(textBoxDBT.Text, textBoxDGK.Text, textBoxDCK.Text);
        }

        public void setTK(string DBT, string DGK, string DCK)
        {
            try
            {
                double BT = Double.Parse(DBT);
                double GK = Double.Parse(DGK);
                double CK = Double.Parse(DCK);
                textBoxTK.Text = (BT * 0.2 + GK * 0.2 + CK * 0.3).ToString("F3");
            }
            catch(Exception)
            {
                textBoxTK.Text = null;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        { 
            try
            {
                try
                {
                    Double.Parse(textBoxDBT.Text);
                    Double.Parse(textBoxDGK.Text);
                    Double.Parse(textBoxDCK.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Mark must be number");
                }
                if (this.MSSV == null)
                {
                    QLSVBLL.Instance.AddStudent(new StudentDTO
                    {
                        MSSV = textBoxMSSV.Text,
                        Name = textBox2.Text,
                        LopSH = comboBoxLopSH.SelectedItem.ToString(),
                        Genre = radioButtonNam.Checked,
                        DBT = Double.Parse(textBoxDBT.Text),
                        DGK = Double.Parse(textBoxDGK.Text),
                        DCK = Double.Parse(textBoxDCK.Text),
                        CourseId = ((CBBItem)comboBoxHP.SelectedItem).Id,
                        ExamDate = dateTimePickerNT.Value.Date,
                    });
                }
                else
                {
                    QLSVBLL.Instance.UpdateStudent(new StudentDTO
                    {
                        MSSV = textBoxMSSV.Text,
                        Name = textBox2.Text,
                        LopSH = comboBoxLopSH.SelectedItem.ToString(),
                        Genre = radioButtonNam.Checked,
                        DBT = Double.Parse(textBoxDBT.Text),
                        DGK = Double.Parse(textBoxDGK.Text),
                        DCK = Double.Parse(textBoxDCK.Text),
                        CourseId = ((CBBItem)comboBoxHP.SelectedItem).Id,
                        ExamDate = dateTimePickerNT.Value.Date,
                    });
                }
                d();
                Dispose();
            }
            catch(DbUpdateException)
            {
                MessageBox.Show("MSSV đã tồn tại!", "Error");
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            d();
            Dispose();
        }
    }
}
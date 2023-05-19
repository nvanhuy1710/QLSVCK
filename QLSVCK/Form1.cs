using QLSVCK.BLL;
using QLSVCK.DB;
using QLSVCK.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSVCK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddCBB();
        }

        public void AddCBB()
        {
            comboBoxHP.Items.AddRange(QLSVBLL.Instance.GetCBBItemsHP().ToArray());
            comboBoxHP.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DetailForm form = new DetailForm();
            form.d += new DetailForm.MyDel(show);
            form.Show();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            show();
        }

        public void show()
        {
            List<StudentDTO> students = QLSVBLL.Instance.GetStudentsByInfor(((CBBItem)comboBoxHP.SelectedItem), textBoxSearch.Text);
            dataGridView1.DataSource = students;
            dataGridView1.Columns["MSSV"].Visible = false;
            dataGridView1.Columns["CourseId"].Visible = false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                DetailForm form = new DetailForm(dataGridView1.SelectedRows[0].Cells["MSSV"].Value.ToString());
                form.d += new DetailForm.MyDel(show);
                form.Show();
            }
            else
            {
                MessageBox.Show("Chi duoc chon 1 hang");
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow id in dataGridView1.SelectedRows)
            {
                QLSVBLL.Instance.DeleteStudent(id.Cells["MSSV"].Value.ToString());
            }
            show();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            List<StudentDTO> students = QLSVBLL.Instance.GetStudentsByInfor(((CBBItem)comboBoxHP.SelectedItem), textBoxSearch.Text, comboBoxSort.SelectedItem.ToString());
            dataGridView1.DataSource = students;
            dataGridView1.Columns["MSSV"].Visible = false;
            dataGridView1.Columns["CourseId"].Visible = false;
        }
    }
}

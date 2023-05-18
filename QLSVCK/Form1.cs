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
            List<Student> students = QLSVBLL.Instance.GetStudentsByInfor(((CBBItem)comboBoxHP.SelectedItem), textBoxSearch.Text);
            dataGridView1.DataSource = students.Select((p, index) => new {
                STT = index + 1,
                p.MSSV,
                TenSV = p.Name,
                p.LopSH,
                StudentCourseId = p.Student_Courses.FirstOrDefault().Id,
                TenHocPhan = p.Student_Courses.Single().Course.Name,
                DiemBT = p.Student_Courses.Single().DBT,
                DiemGK = p.Student_Courses.Single().DGK,
                DiemCK = p.Student_Courses.Single().DCK,
                TongKet = p.Student_Courses.Single().DBT * 0.2 + p.Student_Courses.Single().DGK * 0.2 + p.Student_Courses.Single().DCK * 0.3,
                Ngaythi = p.Student_Courses.Single().ExamDate
            }).ToList();
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns["StudentCourseId"].Visible = false;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count == 1)
            {
                DetailForm form = new DetailForm(dataGridView1.SelectedRows[0].Cells["MSSV"].Value.ToString(), Int32.Parse(dataGridView1.SelectedRows[0].Cells["StudentCourseId"].Value.ToString()));
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
            List<Student> students = QLSVBLL.Instance.GetStudentsByInfor(((CBBItem)comboBoxHP.SelectedItem), textBoxSearch.Text, comboBoxSort.SelectedItem.ToString());
            dataGridView1.DataSource = students.Select((p, index) => new {
                STT = index + 1,
                p.MSSV,
                TenSV = p.Name,
                p.LopSH,
                StudentCourseId = p.Student_Courses.FirstOrDefault().Id,
                TenHocPhan = p.Student_Courses.Single().Course.Name,
                DiemBT = p.Student_Courses.Single().DBT,
                DiemGK = p.Student_Courses.Single().DGK,
                DiemCK = p.Student_Courses.Single().DCK,
                TongKet = p.Student_Courses.Single().DBT * 0.2 + p.Student_Courses.Single().DGK * 0.2 + p.Student_Courses.Single().DCK * 0.3,
                Ngaythi = p.Student_Courses.Single().ExamDate
            }).ToList();
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns["StudentCourseId"].Visible = false;
        }
    }
}

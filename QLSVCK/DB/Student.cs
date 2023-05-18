using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DB
{
    public class Student
    {
        [Key]
        public string MSSV { get; set; }

        public string Name { get; set; }

        public string LopSH { get; set; }

        public bool Genre { get; set; }

        public virtual ICollection<Student_Course> Student_Courses { get; set; }    

        public Student()
        {
            this.Student_Courses = new HashSet<Student_Course>();
        }

    }
}

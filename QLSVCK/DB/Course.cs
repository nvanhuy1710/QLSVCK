using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DB
{
    public class Course
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Student_Course> Student_Courses { get; set; }

        public Course()
        {
            this.Student_Courses = new HashSet<Student_Course>();
        }
    }
}

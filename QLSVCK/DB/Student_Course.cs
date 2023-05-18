using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DB
{
    public class Student_Course
    {
        [Key]
        public int Id { get; set; }

        public double DBT { get; set; }

        public double DGK { get; set; }

        public double DCK { get; set; }

        public string MSSV { get; set; }

        public string CourseId { get; set; }

        public DateTime ExamDate { get; set; }

        [ForeignKey("MSSV")]
        public virtual Student Student { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}

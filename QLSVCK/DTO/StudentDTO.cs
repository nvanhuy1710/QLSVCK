using QLSVCK.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DTO
{
    internal class StudentDTO
    {
        public int STT { get; set; }

        public string MSSV { get; set; }

        public string Name { get; set; }

        public string LopSH { get; set; }

        public bool Genre { get; set; }

        public double DBT { get; set; }

        public double DGK { get; set; }

        public double DCK { get; set; }

        public double TK { get; set; }

        public string CourseId { get; set; }

        public string CourseName { get; set; }

        public DateTime ExamDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DB
{
    internal class CreateDB : CreateDatabaseIfNotExists<ModelSV>
    {
        protected override void Seed(ModelSV context)
        {
            context.Courses.AddRange(new Course[]
            {
                new Course { Id = "HP1", Name = "Giai tich"},
                new Course { Id = "HP2", Name = "XSTK"},
            });
            context.Students.AddRange(new Student[]
            {
                new Student { MSSV = "102210011", Name = "NVA", Genre = true, LopSH = "21TDT"},
                new Student { MSSV = "102210012", Name = "NVB", Genre = false, LopSH = "21TDT2"},
                new Student { MSSV = "102210014", Name = "NVC", Genre = false, LopSH = "21TCLC"},
                new Student { MSSV = "102210013", Name = "NVD", Genre = true, LopSH = "21TDT"},
                new Student { MSSV = "102210015", Name = "NVE", Genre = true, LopSH = "21TDT2"},
            });
            context.Students_Courses.AddRange(new Student_Course[]
            {
                new Student_Course { DBT = 7.5, DGK = 8.5, DCK = 8, CourseId = "HP1", MSSV = "102210011", ExamDate = new DateTime(2023, 5, 23)},
                new Student_Course { DBT = 8.5, DGK = 7, DCK = 6.5, CourseId = "HP2", MSSV = "102210012", ExamDate = new DateTime(2023, 5, 26)},
                new Student_Course { DBT = 6.5, DGK = 6, DCK = 7, CourseId = "HP2", MSSV = "102210013", ExamDate = new DateTime(2023, 4, 28)},
                new Student_Course { DBT = 9, DGK = 9.5, DCK = 6, CourseId = "HP1", MSSV = "102210014", ExamDate = new DateTime(2023, 3, 25)},
                new Student_Course { DBT = 5, DGK = 8.5, DCK = 9, CourseId = "HP2", MSSV = "102210015", ExamDate = new DateTime(2023, 7, 29)},
            });
        }
    }
}

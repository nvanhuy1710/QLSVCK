using QLSVCK.DAL;
using QLSVCK.DB;
using QLSVCK.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.BLL
{
    internal class QLSVBLL
    {
        private static QLSVBLL _Instance;

        public static QLSVBLL Instance
        {
            get
            {
                if(_Instance == null ) _Instance = new QLSVBLL();
                return _Instance;
            }
            set { _Instance = value; }
        }

        private QLSVBLL() { }

        public List<CBBItem> GetCBBItemsHP()
        {
            List<Course> Courses = QLSVDAL.Instance.GetAllCourses();
            List<CBBItem> cBBItems = new List<CBBItem>();
            CBBItem cBBItem1 = new CBBItem()
            {
                Id = "",
                Value = "All"
            };
            if(!cBBItems.Contains(cBBItem1))
            {
                cBBItems.Add(cBBItem1);
            }
            foreach(Course course in Courses)
            {
                CBBItem cBBItem = new CBBItem()
                {
                    Id = course.Id,
                        Value = course.Name,
                };
                if(!cBBItems.Contains(cBBItem)) cBBItems.Add(cBBItem);
            }
            return cBBItems;
        }

        public List<CBBItem> GetCBBItemsLSH()
        {
            List<string> lshs = QLSVDAL.Instance.GetAllLSH();
            List<CBBItem> cBBItems = new List<CBBItem>();
            foreach (string lsh in lshs)
            {
                CBBItem cBBItem = new CBBItem()
                {
                    Id = string.Empty,
                    Value = lsh,
                };
                if (!cBBItems.Contains(cBBItem)) cBBItems.Add(cBBItem);
            }
            return cBBItems;
        }

        public List<StudentDTO> GetStudentsByInfor(CBBItem MHP, string Name, string sortType = null)
        {
            List<Student> students = QLSVDAL.Instance.GetStudentsByInfor(MHP, Name);
            if(sortType != null)
            {
                switch(sortType)
                {
                    case "Name":
                        students = students.OrderBy(p => p.Name).ToList();
                        break;
                    case "LopSH":
                        students = students.OrderBy(p => p.LopSH).ToList();
                        break;
                    case "Genre":
                        students = students.OrderBy(p => p.Genre).ToList();
                        break;
                    case "DiemBT":
                        students = students.OrderBy(p => p.Student_Courses.FirstOrDefault().DBT).ToList();
                        break;
                    case "DiemGK":
                        students = students.OrderBy(p => p.Student_Courses.FirstOrDefault().DGK).ToList();
                        break;
                    case "DiemCK":
                        students = students.OrderBy(p => p.Student_Courses.FirstOrDefault().DCK).ToList();
                        break;
                    case "HocPhan":
                        students = students.OrderBy(p => p.Student_Courses.FirstOrDefault().Course.Name).ToList();
                        break;
                    case "NgayThi":
                        students = students.OrderBy(p => p.Student_Courses.FirstOrDefault().ExamDate).ToList();
                        break;
                    default:
                        break;

                }
            }
            return students.Select((p, index) =>
            {
                StudentDTO svDTO = MapToDTO(p);
                svDTO.STT = index + 1;
                return svDTO;
            }).ToList();
        }

        public StudentDTO GetStudentByMSSV(string MSSV)
        {
            return MapToDTO(QLSVDAL.Instance.GetStudentByMSSV(MSSV));
        }

        public void AddStudent(StudentDTO student)
        {
            Student student1 = new Student()
            {
                MSSV = student.MSSV,
                Name = student.Name,
                Genre = student.Genre,
                LopSH = student.LopSH,
            };
            QLSVDAL.Instance.AddStudent(student1);
            Student_Course student_Course = new Student_Course()
            {
                DBT = student.DBT,
                DGK = student.DGK,
                DCK = student.DCK,
                MSSV = student.MSSV,
                CourseId = student.CourseId,
                ExamDate = student.ExamDate,
            };
            QLSVDAL.Instance.AddStudent_Course(student_Course);
        }

        public void UpdateStudent(StudentDTO student)
        {
            Student student1 = new Student()
            {
                MSSV = student.MSSV,
                Name = student.Name,
                Genre = student.Genre,
                LopSH = student.LopSH,
            };
            QLSVDAL.Instance.UpdateStudent(student1);
            Student_Course student_Course = new Student_Course()
            {
                DBT = student.DBT,
                DGK = student.DGK,
                DCK = student.DCK,
                MSSV = student.MSSV,
                CourseId = student.CourseId,
                ExamDate = student.ExamDate,
            };
            QLSVDAL.Instance.UpdateStudentCourse(student_Course);
        }

        public void DeleteStudent(string MSSV)
        {
            QLSVDAL.Instance.DeleteStudent(MSSV);
        }

        public StudentDTO MapToDTO(Student student)
        {
            return new StudentDTO()
            {
                MSSV = student.MSSV,
                Name = student.Name,
                LopSH = student.LopSH,
                Genre = student.Genre,
                DBT = student.Student_Courses.FirstOrDefault().DBT,
                DGK = student.Student_Courses.FirstOrDefault().DGK,
                DCK = student.Student_Courses.FirstOrDefault().DCK,
                TK = Math.Round(student.Student_Courses.FirstOrDefault().DBT * 0.2 + student.Student_Courses.FirstOrDefault().DGK * 0.2
                    + student.Student_Courses.FirstOrDefault().DCK * 0.3, 3),
                CourseId = student.Student_Courses.FirstOrDefault().CourseId,
                CourseName = student.Student_Courses.FirstOrDefault().Course.Name,
                ExamDate = student.Student_Courses.FirstOrDefault().ExamDate.Date,
            };
        }
    }
}

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
            using(ModelSV db = new ModelSV())
            {
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
                foreach(Course course in db.Courses.ToList())
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
        }

        public List<CBBItem> GetCBBItemsLSH()
        {
            using (ModelSV db = new ModelSV())
            {
                List<CBBItem> cBBItems = new List<CBBItem>();
                List<string> lshs = db.Students.Select(p => p.LopSH).Distinct().ToList();
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
        }

        public List<Student> GetStudentsByInfor(CBBItem MHP, string Name, string sortType = null)
        {
            ModelSV db = new ModelSV();
            List<Student> students = new List<Student>();
            if(!MHP.Value.Equals("All")) students = db.Students.Where(p => p.Student_Courses.FirstOrDefault().CourseId == MHP.Id && (p.MSSV.Contains(Name) || p.Name.Contains(Name))).ToList();
            else students = db.Students.Where(p => (p.MSSV.Contains(Name) || p.Name.Contains(Name))).ToList();
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
            return students;
        }

        public Student GetStudentByMSSV(string MSSV)
        {
            ModelSV db = new ModelSV();
            return db.Students.Where(p => p.MSSV == MSSV).SingleOrDefault();
        }

        public void AddStudent(Student student)
        {
            ModelSV db = new ModelSV();
            db.Students.Add(student);
            db.SaveChanges();
            foreach(Student_Course student_Course in student.Student_Courses)
            {
                student_Course.MSSV = student.MSSV;
            }
            //AddStudentCourse(student.Student_Courses.ToList());
        }

        public void AddStudentCourse(List<Student_Course> studentCourses)
        {
            ModelSV db = new ModelSV();
            foreach(Student_Course studentCourse in studentCourses)
            {
                studentCourse.Student = null;
                db.Students_Courses.Add(studentCourse);
                db.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            ModelSV db = new ModelSV();
            Student student1 = db.Students.Where(p => p.MSSV == student.MSSV).Single();
            student1.Name = student.Name;
            student1.LopSH = student.LopSH;
            student1.Genre = student.Genre;
            db.SaveChanges();
            UpdateStudentCourse(student.Student_Courses.ToList());
        }

        public void UpdateStudentCourse(List<Student_Course> studentCourses)
        {
            ModelSV db = new ModelSV();
            foreach(Student_Course studentCourse in studentCourses)
            {
                Student_Course student_Course = db.Students_Courses.Where(p => p.Id == studentCourse.Id).Single();
                student_Course.DBT = studentCourse.DBT;
                student_Course.DGK = studentCourse.DGK;
                student_Course.DCK = studentCourse.DCK;
                student_Course.CourseId = studentCourse.CourseId;
            }
            db.SaveChanges();
        }

        public void DeleteStudent(string MSSV)
        {
            ModelSV db = new ModelSV();
            Student student = db.Students.Where(p => p.MSSV == MSSV).Single();
            List<Student_Course> student_Courses = student.Student_Courses.ToList();
            foreach(Student_Course student_Course in student_Courses)
            {
                db.Students_Courses.Remove(student_Course);
                
            }
            db.Students.Remove(student);
            db.SaveChanges();
        }
    }
}

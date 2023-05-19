using QLSVCK.DB;
using QLSVCK.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSVCK.DAL
{
    internal class QLSVDAL
    {
        private static QLSVDAL _Instance;

        public static QLSVDAL Instance
        {
            get
            {
                if(_Instance == null) _Instance = new QLSVDAL();
                return _Instance;
            }
            set { _Instance = value; }
        }

        private QLSVDAL() { }

        public void AddStudent(Student student)
        {
            ModelSV db = new ModelSV();
            db.Students.Add(student);
            db.SaveChanges();
        }

        public List<Course> GetAllCourses()
        {
            ModelSV db = new ModelSV();
            return db.Courses.ToList();
        }

        public List<string> GetAllLSH()
        {
            ModelSV db = new ModelSV();
            return db.Students.Select(p => p.LopSH).Distinct().ToList();
        }

        public void AddStudent_Course(Student_Course studentCourse)
        {
            ModelSV db = new ModelSV();
            studentCourse.Student = null;
            db.Students_Courses.Add(studentCourse);
            db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            ModelSV db = new ModelSV();
            Student student1 = db.Students.Where(p => p.MSSV == student.MSSV).Single();
            student1.Name = student.Name;
            student1.LopSH = student.LopSH;
            student1.Genre = student.Genre;
            db.SaveChanges();
        }

        public void UpdateStudentCourse(Student_Course studentCourse)
        {
            ModelSV db = new ModelSV();
            Student_Course student_Course = db.Students_Courses.Where(p => p.MSSV == studentCourse.MSSV).Single();
            student_Course.DBT = studentCourse.DBT;
            student_Course.DGK = studentCourse.DGK;
            student_Course.DCK = studentCourse.DCK;
            student_Course.CourseId = studentCourse.CourseId;
            db.SaveChanges();
        }

        public void DeleteStudent(string MSSV)
        {
            ModelSV db = new ModelSV();
            Student student = db.Students.Where(p => p.MSSV == MSSV).Single();
            List<Student_Course> student_Courses = student.Student_Courses.ToList();
            foreach (Student_Course student_Course in student_Courses)
            {
                db.Students_Courses.Remove(student_Course);

            }
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public Student GetStudentByMSSV(string MSSV)
        {
            ModelSV db = new ModelSV();
            return db.Students.Where(p => p.MSSV == MSSV).SingleOrDefault();
        }

        public List<Student> GetStudentsByInfor(CBBItem MHP, string Name)
        {
            ModelSV db = new ModelSV();
            if (!MHP.Value.Equals("All")) return db.Students.Where(p => p.Student_Courses.FirstOrDefault().CourseId == MHP.Id && (p.MSSV.Contains(Name) || p.Name.Contains(Name))).ToList();
            else return db.Students.Where(p => (p.MSSV.Contains(Name) || p.Name.Contains(Name))).ToList();
        }
    }
}

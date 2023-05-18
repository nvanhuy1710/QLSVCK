using QLSVCK.DB;
using System;
using System.Data.Entity;
using System.Linq;

namespace QLSVCK
{
    public class ModelSV : DbContext
    {
        // Your context has been configured to use a 'ModelSV' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'QLSVCK.ModelSV' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ModelSV' 
        // connection string in the application configuration file.
        public ModelSV()
            : base("name=ModelSV")
        {
            Database.SetInitializer<ModelSV>(new CreateDB());
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Student_Course> Students_Courses { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
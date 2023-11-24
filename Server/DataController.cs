using Server.DataBase;

namespace UdpServer
{
    class DataController
    {
        public int AddStudent(School school)
        {
            using (SchoolContext db = new SchoolContext())
            {
                db.Students.Add(school);
                db.SaveChanges();
                int id = db.Students.OrderBy(s => s.ID).First().ID;
                return id;
            }
        }

        public void DeleteStudent(int comand)
        {            
            using (SchoolContext db = new SchoolContext())
            {
                
                var students = db.Students;
                School school = db.Students.Find(comand);
                if (school == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    db.Students.Remove(school);
                    db.SaveChanges();
                }
            }
        }

        public School GetSchool(int comand)
        {
            using (SchoolContext db = new SchoolContext())
            {
                return db.Students.FirstOrDefault(p => p.ID == comand);
            }
        }

        public List<School> GetStudents()
        {
            using (SchoolContext db = new SchoolContext())
            {
                return db.Students.ToList();
            }
        }

        public void DeleteAll()
        {
            using (SchoolContext db = new SchoolContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE Schools");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Schools', RESEED, 0)");
                db.SaveChanges();
            }
        }
    }
}

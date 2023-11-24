using System.Data.Entity;
using UdpServer;

namespace Server.DataBase
{
    class SchoolContext : DbContext
    {
        public SchoolContext() : base("DbConnection")
        { }
        public DbSet<School> Students { get; set; }
    }
}

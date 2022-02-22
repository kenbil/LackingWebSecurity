using Microsoft.EntityFrameworkCore;
using LackingWebSecurityCore.Models.DataLayer;

namespace LackingWebSecurityCore.Data
{
    public class DataContext : DbContext, IContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using LackingWebSecurityCore.Models.DataLayer;

namespace LackingWebSecurityCore.Data
{
    public interface IContext
    {
        DbSet<Patient> Patients { get; }
        int SaveChanges();
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LackingWebSecurityCore.Data;
using LackingWebSecurityCore.Models.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace LackingWebSecurityCore.Services
{
    public class PatientService
    {
        private IContext context = null;

        public PatientService(IContext _context)
        {
            context = _context;
        }

        #region SqlInjection

        /// <summary>
        /// A Get method that is subjectable to injection.  There shouldn't ever be a need to use the SqlQuery method of the context because
        /// you can get everything you need using the standard linq syntax.  However, if you must use it you need to parameterize your queries.
        /// The intellisense for the method gives information on how to do it.  Circumventing it, like below, will leave a vulnerability
        /// </summary>
        /// <param name="MRN">
        ///     The service doesn't know where MRN came from, it just knows it gets it as a parameter.  All parameter used in any type of query
        ///     should be treated as if they were user input and parmaterized.  Failing to do so will create a vulnerability
        /// </param>
        /// <returns></returns>
        public IEnumerable<Patient> BadGet(string MRN)
        {
            if (context is DataContext)
            {
                var sql = "SELECT * FROM Patients WHERE MRN='" + MRN + "'";
                return context.Patients.FromSql(sql);
            }
            else
            {
                throw new InvalidOperationException("Can't test SQL Injection using a non-sql context");
            }
        }

        /// <summary>
        /// A Get method that isn't vulnerable to injection BUT still uses the SqlQuery method.  We really should just use
        /// the standard linq syntax for a couple of reasons, the biggest being that using the SqlQuery method isn't easily testable.
        /// </summary>
        /// <param name="MRN">
        ///     the MRN parmaeter is user input, so we need to parameterize it
        /// </param>
        /// <returns></returns>
        public IEnumerable<Patient> BetterGet(string MRN)
        {
            if (context is DataContext)
            {
                //when parameterizing the queries, you can use numbered parameters or named ones
                //both are demostrated below for completeness but you don't need both

                //with numbered parameters you don't need to construct the DbParameter object, you just pass it in to
                //the param collection and it will be built up for you.  this is easier but less control and if you reuse the
                //same parameter in teh query more than once can get repetative
                var sql = "SELECT * FROM Patients WHERE MRN=@p0";
                var PatientWithNumberedParameter = context.Patients.FromSql(sql, MRN);

                //with named parameters you specify the name and then construct the parameter object.
                //this is easier for more complex queries
                sql = "SELECT * FROM Patients WHERE MRN=@mrn";
                var PatientWithNamedParameter = context.Patients.FromSql(sql, new SqlParameter("@mrn", MRN));

                //it doesn't matter which one return, both are the same.  Because both user parameters they are both safe
                return PatientWithNamedParameter.ToList();
            }
            else
            {
                throw new InvalidOperationException("Can't test SQL Injection using a non-sql context");
            }
        }

        /// <summary>
        /// The BestGet method uses the standard entity framework objects and syntax.  By default, Entity Framework
        /// parameterizes the queries for you, so THEY ARE SAFE.  This way is the least code, easiest to read and one subtle
        /// difference from the other two methods, it's unit testable.  We can supply a mock context to the service
        /// and unit test this without ANY code changes to the service
        /// </summary>
        /// <param name="MRN">
        ///     The MRN is user input but EF will automaticaly parameterize if for us
        /// </param>
        /// <returns></returns>
        public IEnumerable<Patient> BestGet(string MRN)
        {
            return context.Patients.Where(p => p.MRN == MRN).ToList();
        }
        #endregion

        #region FunctionalStuff
        //these method aren't related to injection, they just do some normal crud stuff

        public IEnumerable<Patient> GetPatients()
        {
            return context.Patients.ToList();
        }

        public Patient AddPatient(Patient patient)
        {
            var newPatient = context.Patients.Add(patient);
            context.SaveChanges();

            return newPatient.Entity;
        }

        public void SeedPatients()
        {
            var numberOfPatients = context.Patients.Count();

            if (numberOfPatients > 0)
            {
                ((DataContext)context).Database.ExecuteSqlCommand("TRUNCATE TABLE [Patients]");
            }

            //add 4 patients to db
            Patient p1 = new Patient() { MRN = "123456", FirstName = "John", LastName = "Doe", DOB = new DateTime(1983, 6, 24) };
            Patient p2 = new Patient() { MRN = "7890123", FirstName = "Jane", LastName = "Doe", DOB = new DateTime(1983, 11, 9) };
            Patient p3 = new Patient() { MRN = "0123445", FirstName = "Howdy", LastName = "Duty", DOB = new DateTime(2008, 9, 25) };
            Patient p4 = new Patient() { MRN = "77123", FirstName = "Watcha", LastName = "Doing", DOB = new DateTime(2010, 4, 27) };
            Patient p5 = new Patient() { MRN = "98031", FirstName = "Donnie", LastName = "Darko", DOB = new DateTime(2012, 5, 11) };

            context.Patients.Add(p1);
            context.Patients.Add(p2);
            context.Patients.Add(p3);
            context.Patients.Add(p4);
            context.Patients.Add(p5);

            context.SaveChanges();


        }
        #endregion
    }
}

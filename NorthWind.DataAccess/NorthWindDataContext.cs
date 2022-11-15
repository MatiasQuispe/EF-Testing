using NorthWind.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace NorthWind.DataAccess
{
    public class NorthWindContext : DbContext
    {

        public NorthWindContext()
            : base("NorthwindDb")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeMap());

            modelBuilder.Configurations.Add(new EmployeeInternalMap());
            modelBuilder.Configurations.Add(new EmployeeExternalMap());

            base.OnModelCreating(modelBuilder);
        }

    }

    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            HasKey(x => x.EmployeeID);
            Property(x => x.LastName).HasMaxLength(20).IsRequired();
            Property(x => x.FirstName).HasMaxLength(10).IsRequired();

            Property(x => x.Address).HasMaxLength(60);
            Property(x => x.City).HasMaxLength(15);
            Property(x => x.Region).HasMaxLength(15);
            Property(x => x.PostalCode).HasMaxLength(10);
            Property(x => x.Country).HasMaxLength(15);

        }
    }

    public class EmployeeInternalMap : EntityTypeConfiguration<EmployeeInternal>
    {
        public EmployeeInternalMap()
        {
            ToTable("EmployeeInternal");

          
        }
    }

    public class EmployeeExternalMap : EntityTypeConfiguration<EmployeeExternal>
    {
        public EmployeeExternalMap()
        {
            ToTable("EmployeeExternal");

            Property(x => x.ConsultantName).IsRequired()
                                           .HasColumnType("varchar")
                                           .HasMaxLength(100);
        }
    }


}

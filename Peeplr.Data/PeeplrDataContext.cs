using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Peeplr.Model;

namespace Peeplr.Data
{
    public class PeeplrDataContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Number> Numbers { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new EmailConfiguration());
            modelBuilder.Configurations.Add(new NumberConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
        }
    }

    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            ToTable("Contacts");

            HasMany(x => x.Emails).WithRequired().HasForeignKey(email => email.ContactId);
            HasMany(x => x.Numbers).WithRequired().HasForeignKey(number => number.ContactId);
            HasMany(x => x.Tags).WithMany();
        }
    }

    public class EmailConfiguration : EntityTypeConfiguration<Email>
    {
        public EmailConfiguration()
        {
            ToTable("Emails");
        }
    }

    public class NumberConfiguration : EntityTypeConfiguration<Number>
    {
        public NumberConfiguration()
        {
            ToTable("Numbers");
        }
    }

    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            ToTable("Tags");
        }
    }

}

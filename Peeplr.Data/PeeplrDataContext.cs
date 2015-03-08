using System.Data.Entity;
using Peeplr.Model;

namespace Peeplr.Data
{
    public class PeeplrDataContext : DbContext
    {
        public DbSet<Contact> Contacts;
        public DbSet<Tag> Tags;
        public DbSet<Number> Numbers;
        public DbSet<Email> Emails;
    }
}

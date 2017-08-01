using Microsoft.EntityFrameworkCore;

namespace Phonebook.Models
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions options) : base(options) {}
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactTag>()
           .HasKey(t => new { t.ContactId, t.TagId });

            modelBuilder.Entity<ContactTag>()
                .HasOne(pt => pt.Contact)
                .WithMany(t => t.ContactTags)
                .HasForeignKey(pt => pt.ContactId);

            modelBuilder.Entity<ContactTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.TagContacts)
                .HasForeignKey(pt => pt.TagId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connection = @"Server=(localdb)\mssqllocaldb;Database=Phonebook;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
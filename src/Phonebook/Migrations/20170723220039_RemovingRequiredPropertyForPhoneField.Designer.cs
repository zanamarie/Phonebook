using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Phonebook.Models;

namespace Phonebook.Migrations
{
    [DbContext(typeof(PhonebookContext))]
    [Migration("20170723220039_RemovingRequiredPropertyForPhoneField")]
    partial class RemovingRequiredPropertyForPhoneField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Phonebook.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInfo");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ContactAvatar");

                    b.Property<string>("ContactName");

                    b.Property<string>("ZipCode");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Phonebook.Models.ContactTag", b =>
                {
                    b.Property<int>("ContactId");

                    b.Property<int>("TagId");

                    b.HasKey("ContactId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ContactTag");
                });

            modelBuilder.Entity("Phonebook.Models.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactId");

                    b.Property<string>("EmailAddress");

                    b.HasKey("EmailId");

                    b.HasIndex("ContactId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Phonebook.Models.Phone", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactId");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("PhoneId");

                    b.HasIndex("ContactId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Phonebook.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Phonebook.Models.ContactTag", b =>
                {
                    b.HasOne("Phonebook.Models.Contact", "Contact")
                        .WithMany("ContactTags")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Phonebook.Models.Tag", "Tag")
                        .WithMany("TagContacts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Phonebook.Models.Email", b =>
                {
                    b.HasOne("Phonebook.Models.Contact", "Contact")
                        .WithMany("Emails")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Phonebook.Models.Phone", b =>
                {
                    b.HasOne("Phonebook.Models.Contact", "Contact")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PeopleApp.Models;

namespace PeopleApp.Migrations
{
    [DbContext(typeof(PeopleContext))]
    [Migration("20161019073710_People")]
    partial class People
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PeopleApp.Models.Person", b =>
                {
                    b.Property<int>("ID");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("ImageFilename");

                    b.Property<string>("LastName");

                    b.HasKey("ID");

                    b.ToTable("People");
                });
        }
    }
}

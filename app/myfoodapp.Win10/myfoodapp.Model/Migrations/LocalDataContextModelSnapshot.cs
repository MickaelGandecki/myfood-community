using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using myfoodapp.Model;

namespace myfoodapp.Model.Migrations
{
    [DbContext(typeof(LocalDataContext))]
    partial class LocalDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("myfoodapp.Model.Measure", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("captureDate");

                    b.Property<int?>("sensorId");

                    b.Property<decimal>("value");

                    b.HasKey("Id");

                    b.HasIndex("sensorId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("myfoodapp.Model.SensorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<DateTime?>("lastCalibration");

                    b.Property<string>("name");

                    b.HasKey("Id");

                    b.ToTable("SensorTypes");
                });

            modelBuilder.Entity("myfoodapp.Model.Measure", b =>
                {
                    b.HasOne("myfoodapp.Model.SensorType", "sensor")
                        .WithMany()
                        .HasForeignKey("sensorId");
                });
        }
    }
}

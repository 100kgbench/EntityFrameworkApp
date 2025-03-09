﻿// <auto-generated />
using System;
using EntityFrameworkApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFrameworkApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("EntityFrameworkApp.DoneExercise", b =>
                {
                    b.Property<int>("DoneExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Repetitions")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sets")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainningId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("DoneExerciseId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("TrainningId");

                    b.ToTable("DoneExercises");
                });

            modelBuilder.Entity("EntityFrameworkApp.Exercise", b =>
                {
                    b.Property<int>("ExerciseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("PersonalRecord")
                        .HasColumnType("REAL");

                    b.Property<int>("TrainningPlanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExerciseId");

                    b.HasIndex("TrainningPlanId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("EntityFrameworkApp.Trainning", b =>
                {
                    b.Property<int>("TrainningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrainningPlanId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TrainningId");

                    b.HasIndex("TrainningPlanId");

                    b.ToTable("Trainnings");
                });

            modelBuilder.Entity("EntityFrameworkApp.TrainningPlan", b =>
                {
                    b.Property<int>("TrainningPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BodyPart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TrainningPlanId");

                    b.ToTable("TrainningPlans");
                });

            modelBuilder.Entity("EntityFrameworkApp.DoneExercise", b =>
                {
                    b.HasOne("EntityFrameworkApp.Exercise", "Exercise")
                        .WithMany("DoneExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFrameworkApp.Trainning", "Trainning")
                        .WithMany("DoneExercises")
                        .HasForeignKey("TrainningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Trainning");
                });

            modelBuilder.Entity("EntityFrameworkApp.Exercise", b =>
                {
                    b.HasOne("EntityFrameworkApp.TrainningPlan", "TrainningPlan")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainningPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainningPlan");
                });

            modelBuilder.Entity("EntityFrameworkApp.Trainning", b =>
                {
                    b.HasOne("EntityFrameworkApp.TrainningPlan", "TrainningPlan")
                        .WithMany("Trainnings")
                        .HasForeignKey("TrainningPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TrainningPlan");
                });

            modelBuilder.Entity("EntityFrameworkApp.Exercise", b =>
                {
                    b.Navigation("DoneExercises");
                });

            modelBuilder.Entity("EntityFrameworkApp.Trainning", b =>
                {
                    b.Navigation("DoneExercises");
                });

            modelBuilder.Entity("EntityFrameworkApp.TrainningPlan", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Trainnings");
                });
#pragma warning restore 612, 618
        }
    }
}

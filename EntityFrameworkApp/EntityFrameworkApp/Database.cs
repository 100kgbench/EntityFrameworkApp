using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkApp;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApp
{

    public class TrainningPlan
    {
        public int TrainningPlanId { get; set; }
        public required string Name { get; set; }
        public required string BodyPart { get; set; }

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public List<Trainning> Trainnings { get; set; } = new List<Trainning>();
    }
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public required string Name { get; set; }
        public required double PersonalRecord { get; set; }
        public int TrainningPlanId { get; set; }
        public TrainningPlan TrainningPlan { get; set; }
        public List<DoneExercise> DoneExercises { get; set; } = new List<DoneExercise>();
    }

    public class Trainning
    {
        public int TrainningId { get; set; }
        public DateTime Date { get; set; }
        public int TrainningPlanId { get; set; }
        public TrainningPlan TrainningPlan { get; set; }
        public List<DoneExercise> DoneExercises { get; set; } = new List<DoneExercise>();
    }
    public class DoneExercise
    {
        public int DoneExerciseId { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public int TrainningId { get; set; }
        public Trainning Trainning { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }
        public int Sets { get; set; }
    }
    public class AppDbContext : DbContext
    {
        public DbSet<TrainningPlan> TrainningPlans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Trainning> Trainnings { get; set; }
        public DbSet<DoneExercise> DoneExercises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=EntityFrameworkApp.db");
        }

    }

}



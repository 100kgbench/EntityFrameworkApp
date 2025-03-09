using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApp
{

    public class Methods
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public Methods(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public async Task<Trainning> AddTrainning(AddTrainningRequest request)
        {
            using var db = new AppDbContext(_options);

            var trainningPlan = await db.TrainningPlans
                .Include(tp => tp.Exercises)
                .FirstOrDefaultAsync(tp => tp.TrainningPlanId == request.TrainningPlanId);

            if (trainningPlan == null)
            {
                throw new Exception("Training plan not found.");
            }

            var newTrainning = new Trainning
            {
                Date = request.Date,
                TrainningPlanId = request.TrainningPlanId,
                DoneExercises = new List<DoneExercise>()
            };

            foreach (var exerciseDone in request.DoneExercises)
            {
                var exercise = await db.Exercises
                    .FirstOrDefaultAsync(e => e.ExerciseId == exerciseDone.ExerciseId);

                if (exercise == null)
                {
                    throw new Exception($"Exercise with ID {exerciseDone.ExerciseId} not found.");
                }

                var doneExercise = new DoneExercise
                {
                    ExerciseId = exercise.ExerciseId,
                    TrainningId = newTrainning.TrainningId,
                    Weight = exerciseDone.Weight,
                    Repetitions = exerciseDone.Repetitions,
                    Sets = exerciseDone.Sets
                };

                newTrainning.DoneExercises.Add(doneExercise);
            }

            db.Trainnings.Add(newTrainning);
            await db.SaveChangesAsync();

            return newTrainning;
        }

       
        public async Task<TrainningPlan> AddTrainningPlan(AddTrainningPlanRequest request)
        {
            using var db = new AppDbContext(_options);

            var trainningPlan = new TrainningPlan
            {
                Name = request.Name,
                BodyPart = request.BodyPart
            }
            ;
            db.TrainningPlans.Add(trainningPlan);
            await db.SaveChangesAsync();

            return trainningPlan;


        }
        public async Task<Exercise> AddExercise(AddExerciseRequest request)
        {
            using var db = new AppDbContext(_options);
            var exercise = new Exercise
            {
                Name = request.Name,
                PersonalRecord = request.PersonalRecord,
                TrainningPlanId = request.TrainningPlanId
            };
            db.Exercises.Add(exercise);
            await db.SaveChangesAsync();

            return exercise;



        }
        public async Task<List<object>> ShowTrainnings()
        {
            using var db = new AppDbContext(_options);
            var trainningData = await db.Trainnings
                .Include(t => t.DoneExercises)
                .ThenInclude(de => de.Exercise)
                .ToListAsync();

            var trainningResponse = trainningData.Select(trainning => (object)new
            {
                trainning.Date,
                DoneExercises = trainning.DoneExercises.Select(de => new
                {
                    de.Exercise.Name,
                    de.Weight,
                    de.Repetitions,
                    de.Sets

                }).ToList()
            }).ToList();

            return trainningResponse;



        }
        public async Task<List<object>> ShowTrainningPlans()
        {
            using var db = new AppDbContext(_options);
            var plans = await db.TrainningPlans
                .Include(p => p.Exercises)
                .Include(p => p.Trainnings)
                .ToListAsync();


            var plansResponse = plans.Select(plan => (object)new
            {

                plan.Name,
                plan.BodyPart,
                Exercises = plan.Exercises.Select(e => e.Name),
                Trainnings = plan.Trainnings.Select(t => t.Date)

            }).ToList();

            return plansResponse;

        }
        public async Task<List<object>> ShowExercises()
        {

            using var db = new AppDbContext(_options);

            var exercisesData = await db.Exercises
                .Include(e => e.TrainningPlan)
                .ToListAsync();

            var exercisesResponse = exercisesData.Select(exercise => (object)new
            {
                exercise.Name,
                exercise.PersonalRecord,
                TrainningPlanName = exercise.TrainningPlan.Name,
            }).ToList();


            return exercisesResponse;


        }
    }
}

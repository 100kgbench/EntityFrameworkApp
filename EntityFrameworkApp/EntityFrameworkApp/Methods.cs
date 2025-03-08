using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApp
{
    class Methods
    {
        public async Task AddTrainning()
        {
            using var db = new AppDbContext();

            Console.WriteLine("Enter date of the trainning (YYYY-MM-DD)");
            DateTime date = DateTime.Parse(Console.ReadLine());


            var isDateTimeFormat = DateTime.TryParse(Console.ReadLine(), out var inputDate);

            DateTime dateInput;
            if (isDateTimeFormat)
            {
                dateInput = inputDate;
            }
            else
            {
                Console.WriteLine("Invalid date format");
                return;
            }
            
            
            
            var trainingPlans = await db.TrainningPlans.ToListAsync();
            Console.WriteLine("Trainning plans:");
            foreach (var plan in trainingPlans)
            {
                Console.WriteLine($"{plan.TrainningPlanId} - {plan.Name}");
            }
            Console.WriteLine("Enter the id of the trainning plan");
            int planId = int.Parse(Console.ReadLine());
            var chosenPlan = trainingPlans[planId - 1];
            var chosenPlan1 = trainingPlans.FirstOrDefault(p => p.TrainningPlanId == planId);

            var newTrainning = new Trainning
            {
                Date = date,
                TrainningPlanId = chosenPlan.TrainningPlanId

            };

            db.Trainnings.Add(newTrainning);
            await db.SaveChangesAsync();
            
            var exercises = db.Exercises.Where(e => e.TrainningPlanId == chosenPlan.TrainningPlanId).ToList();
            Console.WriteLine("Exercises in this trainning plan");
            foreach (var exercise in exercises)
            {
                Console.WriteLine($"- {exercise.Name}");
                Console.WriteLine("Enter the weight");
                double weight = double.Parse(Console.ReadLine());
                if(weight > exercise.PersonalRecord)
                {
                    Console.WriteLine("You hit new Personal Best");
                    exercise.PersonalRecord = weight;
                    db.SaveChanges();
                    Console.WriteLine($"New Personal Best is now:{weight} ");
                }
                Console.WriteLine("Enter the repetitions");
                int repetitions = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the sets");
                int sets = int.Parse(Console.ReadLine());

                var doneExercise = new DoneExercise
                {
                    ExerciseId = exercise.ExerciseId,
                    TrainningId = newTrainning.TrainningId,
                    Weight = weight,
                    Repetitions = repetitions,
                    Sets = sets
                };
                db.DoneExercises.Add(doneExercise);
            }
            db.SaveChanges();
            Console.WriteLine("Trainning and done exercises succesfully saved");
        }
        public static void AddTrainningPlan()
        {
            using var db = new AppDbContext();

            Console.WriteLine("Enter the name of the trainning plan");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the body part");
            string bodyPart = Console.ReadLine();

            var newTrainningPlan = new TrainningPlan
            {
                Name = name,
                BodyPart = bodyPart
            };
            db.TrainningPlans.Add(newTrainningPlan);
            db.SaveChanges();
            Console.WriteLine("Trainning plan succesfully saved");
        }
        public static void AddExercise()
        {
            using var db = new AppDbContext();
            Console.WriteLine("Enter name of new exercise");
            string name = Console.ReadLine();
            Console.WriteLine("Enter personal record");
            double personalRecord = double.Parse(Console.ReadLine());
            var plans = db.TrainningPlans.ToList();
            Console.WriteLine("Enter which trainning plan it should be added to:");
            foreach (var plan in plans)
            {
                Console.WriteLine($"{plan.TrainningPlanId} - {plan.Name}");
            }
            int planId = int.Parse(Console.ReadLine());
            var chosenPlan = plans[planId - 1];
            var newExercise = new Exercise
            {
                Name = name,
                PersonalRecord = personalRecord,
                TrainningPlanId = chosenPlan.TrainningPlanId
            };
            db.Exercises.Add(newExercise);
            db.SaveChanges();
            Console.WriteLine("Exercise succesfully saved");
        }
        public static void ShowTrainnings()
        {
            using var db = new AppDbContext();
            var trainnings = db.Trainnings.Include(t => t.DoneExercises).ToList();
            foreach (var trainning in trainnings)
            {
                Console.WriteLine($"Trainning on {trainning.Date}");
                foreach (var doneExercise in trainning.DoneExercises)
                {
                    Console.WriteLine($"- {doneExercise.Exercise.Name} {doneExercise.Weight}kg {doneExercise.Repetitions}x{doneExercise.Sets}");
                }
            }
        }
        public static void ShowTrainningPlans()
        {
            using var db = new AppDbContext();
            var plans = db.TrainningPlans.Include(p => p.Exercises).Include(p => p.Trainnings).ToList();
            foreach (var plan in plans)
            {
                Console.WriteLine($"\nTrainning plan name: {plan.Name}");
                Console.WriteLine($"Body part: {plan.BodyPart}");
                Console.WriteLine("Exercises:");
                foreach (var exercise in plan.Exercises)
                {
                    Console.WriteLine($"- {exercise.Name}");
                }
                Console.WriteLine("Trainnings:");
                foreach (var trainning in plan.Trainnings)
                {
                    Console.WriteLine($"- {trainning.Date}");
                }
            }
        }
        public static void ShowExercises()
        {
            using var db = new AppDbContext();
            var exercises = db.Exercises.Include(e => e.DoneExercises).ToList();
            foreach (var exercise in exercises)
            {
                Console.WriteLine($"Exercise: {exercise.Name}");
                Console.WriteLine($"Personal record: {exercise.PersonalRecord}");
                Console.WriteLine("Done exercises:");
                foreach (var doneExercise in exercise.DoneExercises)
                {
                    Console.WriteLine($"- {doneExercise.Weight}kg {doneExercise.Repetitions}x{doneExercise.Sets}");
                }
            }
        }


    }
}

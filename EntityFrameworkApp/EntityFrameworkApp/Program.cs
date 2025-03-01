using Microsoft.EntityFrameworkCore;
using EntityFrameworkApp;

namespace EntityFrameworkApp
{
   
    
    internal class Program
    {

        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
                while (true)
                {
                    Console.WriteLine("\n1 - Add trainning plan");
                    Console.WriteLine("2 - Add trainning");
                    Console.WriteLine("3 - Add exercise");
                    Console.WriteLine("4 - Show trainnings");
                    Console.WriteLine("5 - Show training plans");
                    Console.WriteLine("6 - Show exercises");
                    Console.WriteLine("7 - Exit");
                    Console.WriteLine("Choose an option");
                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            Methods.AddTrainningPlan();
                            break;
                        case 2:
                            Methods.AddTrainning();
                            break;
                        case 3:
                            Methods.AddExercise();
                            break;
                        case 4:
                            Methods.ShowTrainnings();
                            break;
                        case 5:
                            Methods.ShowTrainningPlans();
                            break;
                        case 6:
                            Methods.ShowExercises();
                            break;
                        case 7:
                            return;
                    }
                }
            }

        }
    }
}

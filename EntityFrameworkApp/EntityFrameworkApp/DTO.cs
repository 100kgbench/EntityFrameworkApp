namespace EntityFrameworkApp;

public class AddExerciseRequest
{
    public required string Name { get; set; }
    public required double PersonalRecord { get; set; }
    public int TrainningPlanId { get; set; }
}
public class AddTrainningPlanRequest
{
    public required string Name { get; set; }
    public required string BodyPart { get; set; }
}
public class AddTrainningRequest
{
    public required DateTime Date { get; set; }
    public required int TrainningPlanId { get; set; }
    public required List<DoneExerciseRequest> DoneExercises { get; set; }
}
public class DoneExerciseRequest
{
    public required int ExerciseId { get; set; }
    public required double Weight { get; set; }
    public required int Repetitions { get; set; }
    public required int Sets { get; set; }
}
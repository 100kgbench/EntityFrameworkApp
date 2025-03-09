using Microsoft.AspNetCore.Mvc;
using Methods = EntityFrameworkApp.Methods;
using EntityFrameworkApp;


[ApiController]
[Route("api/[controller]")]
public class EntityFrameworkAppAPIController : ControllerBase
{
    private readonly Methods _methods;

    public EntityFrameworkAppAPIController(Methods methods)
    {
        _methods = methods;
    }
    [HttpGet("Show_Trainning_Plans")]
    public async Task<IActionResult> ShowTrainningPlans()
    {
        var plans = await _methods.ShowTrainningPlans();
        return Ok(plans);
    }
    [HttpGet("Show_Exercises")]
    public async Task<IActionResult> ShowExercises()
    {
        var exercisesData = await _methods.ShowExercises();
        return Ok(exercisesData);
    }
    [HttpGet("Show_Trainnings")]
    public async Task<IActionResult> ShowTrainnings()
    {
        var trainningData = await _methods.ShowTrainnings();
        return Ok(trainningData);
    }
    [HttpPost("Add_TrainningPlan")]
    public async Task<IActionResult> AddTrainningPlan([FromBody] AddTrainningPlanRequest request)
    {
        var plan = await _methods.AddTrainningPlan(request);
        return Ok(plan);
    }
    [HttpPost("Add_Exercise")]
    public async Task<IActionResult> AddExercise([FromBody] AddExerciseRequest request)
    {
        var exercise = await _methods.AddExercise(request);
        return Ok(exercise);
    }
    [HttpPost("Add_Trainning")]
    public async Task<IActionResult> AddTrainning([FromBody] AddTrainningRequest request)
    {
        var trainning = await _methods.AddTrainning(request);
        return Ok(trainning);
    }


}
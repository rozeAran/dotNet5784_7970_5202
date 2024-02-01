namespace BO;

public class Milestone
{ 
    public int Id { get; init; }
    public string? Alias { get; init; }

    public string? Description { get; init; }

    DateTime CreatedAtDate { get; init; }

    BO.Status Status { get; init; }

    List<BO.TaskInList> Dependencies { get; init; }

    public string? Remarks { get; init; }

    public double CompletionPercentage { get; init; }

    DateTime? CompleteDate { get; init; } = null;
    DateTime? DeadLineDate { get; init; }= null;
    DateTime? ForecastDate { get; init; }= null;

}

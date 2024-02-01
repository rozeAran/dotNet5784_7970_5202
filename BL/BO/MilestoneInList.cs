namespace BO;

public class MilestoneInList
{
    public int Id { get; init; }
    public string? Alias { get; set; }
    public string? Description { get; set; }
    BO.Status Status { get; set; }
    public double CompletionPercentage { get; set; }

}

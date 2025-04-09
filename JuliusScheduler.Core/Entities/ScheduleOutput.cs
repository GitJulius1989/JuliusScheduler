namespace JuliusScheduler.Core;

public class SchedulerOutput
{
    public DateTime NextExecTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
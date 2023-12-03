namespace Blazorcrud.Shared.Models;

public class Schedule
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int GetExpectedWorkHours()
    {
        var time = (EndTime - StartTime).Hours;
        if (time < 0)
            time += 24;
        return time;
    }
}
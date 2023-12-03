namespace Blazorcrud.Shared.Models;

public class Attendance
{
    public int Id { get; set; }
    public DateTime DayOfWeek { get; set; }
    public DateTime AttendanceTime { get; set; }
    public DateTime DepartureTime { get; set; }

    public int PersonId { get; set; }

    public int GetActualWorkHours()
    {
        var time = (DepartureTime - AttendanceTime).Hours;
        if (time < 0)
            time += 24;
        return time;
    }
}
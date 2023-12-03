using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public interface IAttendanceRepository
{
    public Task<Attendance> AddAttendance(Attendance attendance);
    public PagedResult<Attendance> GetAttendances(string? name, int page);
    public Task<Attendance?> GetAttendance(int attendanceId);
    public Task<Attendance?> UpdateAttendance(Attendance attendance);
    public Task<Attendance?> DeleteAttendance(int attendanceId);
}
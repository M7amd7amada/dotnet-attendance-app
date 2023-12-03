using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

#nullable enable

namespace Blazorcrud.Client.Services;

public interface IAttendanceService
{
    public Task AddAttendance(Attendance attendance);
    public Task<Attendance> GetAttendance(int id);
    public Task DeleteAttendance(int id);
    public Task UpdateAttendance(Attendance attendance);
    public Task<PagedResult<Attendance>> GetAttendances(string? name, string page);
}
using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public class AttendanceService : IAttendanceService
{
    private IHttpService _httpService;

    public AttendanceService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task AddAttendance(Attendance attendance)
    {
        await _httpService.Post($"api/attendance", attendance);
    }

    public Task DeleteAttendance(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Attendance> GetAttendance(int id)
    {
        return await _httpService.Get<Attendance>($"api/attendance/{id}");
    }

    public async Task<PagedResult<Attendance>> GetAttendances(string name, string page)
    {
        return await _httpService.Get<PagedResult<Attendance>>
            ("api/attendance" + "?page=" + page + "&name=" + name);
    }

    public Task UpdateAttendance(Attendance attendance)
    {
        throw new NotImplementedException();
    }
}
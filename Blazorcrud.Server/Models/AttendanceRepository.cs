using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _appDbContext;

    public AttendanceRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Attendance> AddAttendance(Attendance attendance)
    {
        var result = await _appDbContext.Attendances.AddAsync(attendance);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Attendance?> DeleteAttendance(int attendanceId)
    {
        var result = await _appDbContext.Attendances.FirstOrDefaultAsync(a => a.Id == attendanceId);
        if (result is not null)
        {
            _appDbContext.Attendances.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Attendance Not Fount.");
        }
        return result;
    }

    public async Task<Attendance?> GetAttendance(int attendanceId)
    {
        var result = await _appDbContext.Attendances.FirstOrDefaultAsync(a => a.Id == attendanceId);
        if (result is not null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Attendance Not Found.");
        }
    }

    public PagedResult<Attendance> GetAttendances(string? name, int page)
    {
        int pageSize = 5;

        if (name is not null)
        {
            return _appDbContext.Attendances.GetPaged(page, pageSize);
        }
        else
        {
            return _appDbContext.Attendances.GetPaged(page, pageSize);
        }
    }

    public Task<Attendance?> UpdateAttendance(Attendance attendance)
    {
        throw new NotImplementedException();
    }
}
using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazorcrud.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceRepository _attendaces;

    public AttendanceController(IAttendanceRepository attendaces)
    {
        _attendaces = attendaces;
    }

    /// <summary>
    /// Returns a list of paginated attendances with a default page 
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetAttendances([FromQuery] string? name, int page)
    {
        return Ok(_attendaces.GetAttendances(name, page));
    }

    /// <summary>
    /// Get the attendance with the specific
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAttendance(int id)
    {
        return Ok(await _attendaces.GetAttendance(id));
    }


    /// <summary>
    /// Creates an attendance.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddAttendance(Attendance attendance)
    {
        return Ok(await _attendaces.AddAttendance(attendance));
    }

    /// <summary>
    /// Update the specific attendance.
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateAttendance(Attendance attendance)
    {
        return Ok(await _attendaces.UpdateAttendance(attendance));
    }

    /// <summary>
    /// Delete the desired attendance with specific id. 
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAttendance(int id)
    {
        return Ok(await _attendaces.DeleteAttendance(id));
    }
}
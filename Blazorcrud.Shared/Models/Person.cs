using Blazorcrud.Shared.Data;

namespace Blazorcrud.Shared.Models;

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public bool IsDeleting { get; set; } = default!;
    public decimal BaseSalary { get; set; }
    public PayrollPeriod PayrollPeriod { get; set; }
    public int TotalWorkingHours => PayrollCalculations.GetWorkingHours(Attendances);
    public decimal TotalPay => PayrollCalculations.Calculate(this);

    // Relationships
    public List<Address> Addresses { get; set; } = default!;
    public List<Attendance> Attendances { get; set; } = default!;
    public Schedule Schedule { get; set; } = default!;
}

public enum PayrollPeriod
{
    Monthly = 30,
    Weekly = 7,
    BiWeekly = 14
}
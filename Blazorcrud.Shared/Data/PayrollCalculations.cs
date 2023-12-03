using Blazorcrud.Shared.Models;

namespace Blazorcrud.Shared.Data;

public static class PayrollCalculations
{

    public static decimal Calculate(Person person)
    {
        decimal hourSalary = GetHourSalary(
            person.BaseSalary,
            (int)person.PayrollPeriod,
            person.Schedule.GetExpectedWorkHours());

        int actual = GetWorkingHours(person.Attendances);
        return actual * hourSalary;
    }

    private static decimal GetHourSalary(decimal baseSalary, int payPeriod, int workingHours)
        => (baseSalary / payPeriod) / (decimal)workingHours;


    public static int GetWorkingHours(List<Attendance> attendances)
    {
        int result = 0;
        foreach (var attendace in attendances)
        {
            result += attendace.GetActualWorkHours();
        }
        return result;
    }
}
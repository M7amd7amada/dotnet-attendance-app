using static Blazorcrud.Shared.Models.PayrollPeriod;
using Blazorcrud.Shared.Models;
using Bogus;

namespace Blazorcrud.Server.Models;

public class DataGenerator
{
    public static void Initialize(AppDbContext appDbContext)
    {
        Randomizer.Seed = new Random(32321);
        appDbContext.Database.EnsureDeleted();
        appDbContext.Database.EnsureCreated();
        if (!(appDbContext.People.Any()))
        {
            var baseSalaries = new List<int>() { 1500, 2000, 3500, 4000, 5500, 6000, 7500, 8000, 9500, 11000 };
            //Create test addresses
            var testAddresses = new Faker<Address>()
                .RuleFor(a => a.Street, f => f.Address.StreetAddress())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.State, f => f.Address.State())
                .RuleFor(a => a.ZipCode, f => f.Address.ZipCode());

            //Create test addresses
            var testSchedules = new Faker<Schedule>()
                .RuleFor(a => a.StartTime, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
                .RuleFor(a => a.EndTime, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(20)));

            var testAttendances = new Faker<Attendance>()
                .RuleFor(a => a.DayOfWeek, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
                .RuleFor(a => a.AttendanceTime, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
                .RuleFor(a => a.DepartureTime, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(20)));

            // Create new people
            var testPeople = new Faker<Blazorcrud.Shared.Models.Person>()
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Addresses, f => testAddresses.Generate(2).ToList())
                .RuleFor(p => p.Attendances, f => testAttendances.Generate((int)f.PickRandom<PayrollPeriod>()).ToList());

            var people = testPeople.Generate(25);
            var schedules = testSchedules.Generate(10);
            Random random = new();

            foreach (Blazorcrud.Shared.Models.Person p in people)
            {
                if (p.Attendances.Count == 7) p.PayrollPeriod = Weekly;
                else if (p.Attendances.Count == 14) p.PayrollPeriod = BiWeekly;
                else if (p.Attendances.Count == 30) p.PayrollPeriod = Monthly;

                int randomIndex = random.Next(schedules.Count);
                p.Schedule = schedules[randomIndex];
                p.BaseSalary = baseSalaries[new Random().Next(baseSalaries.Count)];

                appDbContext.People.Add(p);
            }
            appDbContext.SaveChanges();
        }

        if (!(appDbContext.Uploads.Any()))
        {
            string jsonRecord = @"[{""FirstName"": ""Tim"",""LastName"": ""Bucktooth"",""Gender"": 1,""PhoneNumber"": ""717-211-3211"",
                    ""Addresses"": [{""Street"": ""415 McKee Place"",""City"": ""Pittsburgh"",""State"": ""Pennsylvania"",""ZipCode"": ""15140""
                    },{ ""Street"": ""315 Gunpowder Road"",""City"": ""Mechanicsburg"",""State"": ""Pennsylvania"",""ZipCode"": ""17101""  }]}]";
            var testUploads = new Faker<Upload>()
                .RuleFor(u => u.FileName, u => u.Lorem.Word() + ".json")
                .RuleFor(u => u.UploadTimestamp, u => u.Date.Past(1, DateTime.Now))
                .RuleFor(u => u.ProcessedTimestamp, u => u.Date.Future(1, DateTime.Now))
                .RuleFor(u => u.FileContent, Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonRecord)));
            var uploads = testUploads.Generate(8);

            foreach (Upload u in uploads)
            {
                appDbContext.Uploads.Add(u);
            }
            appDbContext.SaveChanges();
        }

        if (!(appDbContext.Attendances.Any()))
        {
            var testAttendances = new Faker<Attendance>()
                .RuleFor(a => a.DayOfWeek, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
                .RuleFor(a => a.AttendanceTime, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow))
                .RuleFor(a => a.DepartureTime, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddHours(20)));

            var attendances = testAttendances.Generate(20);

            foreach (Attendance a in attendances)
            {
                a.AttendanceTime = GenerateRandomDateTime();
                a.DepartureTime = GenerateRandomDateTime();
                a.DayOfWeek = GenerateRandomDateTime();
                appDbContext.Attendances.Add(a);
            }

            appDbContext.SaveChanges();
        }

        if (!(appDbContext.Users.Any()))
        {
            var testUsers = new Faker<User>()
                .RuleFor(u => u.FirstName, u => u.Name.FirstName())
                .RuleFor(u => u.LastName, u => u.Name.LastName())
                .RuleFor(u => u.Username, u => u.Internet.UserName())
                .RuleFor(u => u.Password, u => u.Internet.Password());
            var users = testUsers.Generate(4);

            User customUser = new User()
            {
                FirstName = "M7amd",
                LastName = "Hamada",
                Username = "admin",
                Password = "admin"
            };

            users.Add(customUser);

            foreach (User u in users)
            {
                u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.Password);
                u.Password = "**********";
                appDbContext.Users.Add(u);
            }
            appDbContext.SaveChanges();
        }
    }
    private static DateTime GenerateRandomDateTime()
    {
        Random random = new Random();

        // You can adjust the range for your specific requirements
        int year = random.Next(2000, 2030);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        int hour = random.Next(0, 24);
        int minute = random.Next(0, 60);
        int second = random.Next(0, 60);

        DateTime randomDateTime = new DateTime(year, month, day, hour, minute, second);

        return randomDateTime;
    }
}

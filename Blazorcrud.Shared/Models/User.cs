using System.Text.Json.Serialization;

namespace Blazorcrud.Shared.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public DateTime LoginDate { get; set; } = DateTime.Now;
        public DateTime LogoutDate { get; set; } = DateTime.Now;
        public string LoginStatus { get; set; } = "Active";
        public string OS { get; set; } = default!;
        public string Browser { get; set; } = default!;
        public string? Token { get; set; } = default!;
        public bool IsDeleting { get; set; } = default!;
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
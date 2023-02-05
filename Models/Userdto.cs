namespace library_management_system.Models
{
    public class Userdto
    {
        public string StudentId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? StudentName { get; set; }
        public string? StudentAddress { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
    }
}

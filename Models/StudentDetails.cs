namespace library_management_system.Models
{
    public class StudentDetails
    {
        public string? StudentName { get; set; }
        public string StudentId { get; set; }
        public string? StudentAddress { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
        public float PenaltyDue { get; set; } = 0.0f;
        public float RentDue { get; set; } = 0.0f;
    }

    public class StudentDetailsInput
    {
        public string? StudentName { get; set; }
        public string StudentId { get; set; }
        public string? StudentAddress { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentPhone { get; set; }
    }
}

namespace RestoRise.Application.DTOs.Staff;

public class StaffRegisterDto
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime  DateOfBirthDate { get; set; }
    public string Gender { get; set; }
    public string? TelegramNick { get; set; }
    
    public Guid RoleId { get; set; }
    
    public Guid BranchId { get; set; }
}
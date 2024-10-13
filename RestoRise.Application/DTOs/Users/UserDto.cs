namespace RestoRise.Application.DTOs;

public class UserDto
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string? MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime  DateOfBirthDate { get; set; }
    public string Gender { get; set; }
    public string? TelegramNick { get; set; }
    public string[] Roles { get; set; }
}
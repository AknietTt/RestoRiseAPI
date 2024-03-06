using RestoRise.Domain.Common;

namespace RestoRise.Domain.Entities;

public class Staff:Entity
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
    
    public IEnumerable<Role> Roles { get; set; }
    
    public IEnumerable<Branch> Branches { get; set; }
}
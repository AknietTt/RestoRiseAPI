namespace RestoRise.Application.DTOs.Branch;

public class BranchOutputDto
{
    public Guid Id { get; set; }
    public string Address { get;  set; } = string.Empty;
    public string City { get; set; }
    public string Restaurant { get; set; }
}
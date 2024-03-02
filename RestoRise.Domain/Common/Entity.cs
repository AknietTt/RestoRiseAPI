namespace RestoRise.Domain.Common;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    protected Entity(Guid id)
    {
        Id = id;
    }
    protected Entity() { }
}
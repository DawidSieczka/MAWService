namespace MAWService.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}
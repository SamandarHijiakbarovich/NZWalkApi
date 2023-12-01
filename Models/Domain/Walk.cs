namespace NzWalks.Api.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKm  { get; set; }
    public string WalkImageUrl { get; set; } = string.Empty;
    public Guid DifficultyId { get; set; }

    //Navigation properties
    public Difficult Difficulty { get; set; }
    public Region Region { get; set; }
}

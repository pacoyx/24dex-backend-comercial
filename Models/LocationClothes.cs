public class LocationClothes
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public IEnumerable<LocationWorkGuide?> LocationWorkGuides { get; set; } = new List<LocationWorkGuide?>();
    

}
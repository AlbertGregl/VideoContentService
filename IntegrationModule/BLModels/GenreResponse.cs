namespace IntegrationModule.BLModels
{
    public partial class GenreResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

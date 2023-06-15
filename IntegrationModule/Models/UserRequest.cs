namespace IntegrationModule.Models
{
    public partial class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CountryOfResidenceId { get; set; }
    }
}

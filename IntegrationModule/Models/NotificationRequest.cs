namespace IntegrationModule.Models
{
    public partial class NotificationRequest
    {
        public string ReceiverEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}

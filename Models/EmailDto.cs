namespace Finances.Models
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; internal set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
      
    }
}

using Radzen;

namespace InsideLine.Database
{
    public class User : Base
    {
        public string FirstName { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string CustomerId { get; set; }
        
    }
}

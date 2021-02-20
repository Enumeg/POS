namespace POS.Domain.Entities
{
    public class Employee : BiLanguageNameEntity
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}

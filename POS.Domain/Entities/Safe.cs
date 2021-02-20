using POS.Domain.Enums;

namespace POS.Domain.Entities
{
    public class Safe : BiLanguageNameEntity
    {
        public double CurrentBalance { get; set; }
        public SafeType Type { get; set; }
    }
}

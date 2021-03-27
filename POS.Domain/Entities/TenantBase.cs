using System.ComponentModel.DataAnnotations.Schema;
using POS.Domain.Helpers;

namespace POS.Domain.Entities
{
    public class TenantBase
    {
        [Column(Order = 1)]
        public int TenantId { get; set; }
    }
    public class EntityBase : TenantBase
    {
        [Column(Order = 2)]
        public int Id { get; set; }

    }

    public class BiLanguageNameEntity : EntityBase
    {
        [Column(Order = 3)]
        public string ArabicName { get; set; }
        [Column(Order = 4)]
        public string EnglishName { get; set; }
        [NotMapped]
        public string Name => CommonHelper.IsArabic ? ArabicName : EnglishName;
    }
}

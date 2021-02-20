using System.ComponentModel.DataAnnotations.Schema;
using POS.Domain.Helpers;

namespace POS.Domain.Entities
{
    public class TenantBase
    {
        public int TenantId { get; set; }
    }
    public class EntityBase : TenantBase
    {

        public int Id { get; set; }

    }

    public class BiLanguageNameEntity : EntityBase
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        [NotMapped]
        public string Name => CommonHelper.IsArabic ? ArabicName : EnglishName;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

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
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Domain.Infrastructure
{

   public class PosContext : DbContext
    {
        public PosContext()
            : base("Con")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);
        }
        public virtual void SetStatus(object entity,EntityState state)
        {
            Entry(entity).State = state;
        }    
    }
}

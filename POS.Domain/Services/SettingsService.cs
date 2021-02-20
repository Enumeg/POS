using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class SettingsService : ServicesBase, ISettingsService
    {
        public SettingsService(PosContext context) : base(context)
        {

        }
        Setting ISettingsService.GetSettings()
        {
            return Context.Settings.FirstOrDefault();
        }     
    }
}

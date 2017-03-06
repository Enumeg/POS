using System.Linq;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class SettingsService : ServicesBase, ISettingsService
    {
        Setting ISettingsService.GetSettings()
        {
            return Context.Settings.FirstOrDefault();
        }     
    }
}

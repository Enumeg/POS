using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ISettingsService : System.IDisposable
    {
        Setting GetSettings();

    }
}

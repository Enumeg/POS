using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface ISettingsService : IInitializer
    {
        Setting GetSettings();

    }
}

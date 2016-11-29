using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface IMachinesService : IInitializer
    {
        Machine GetMachineByName(string name);
    }
}
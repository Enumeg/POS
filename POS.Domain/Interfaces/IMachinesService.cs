using POS.Domain.Entities;
using POS.Domain.Infrastructure;

namespace POS.Domain.Interfaces
{
    public interface IMachinesService : System.IDisposable
    {
        Machine GetMachineByName(string name);
    }
}
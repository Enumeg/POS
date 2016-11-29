using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Domain.Entities;
using POS.Domain.Infrastructure;
using POS.Domain.Interfaces;

namespace POS.Domain.Services
{
    public class MachinesService : ServicesBase, IMachinesService
    {
        Machine IMachinesService.GetMachineByName(string name)
        {
            return Context.Machines.FirstOrDefault(m => m.Name == name);
        }
    }
}

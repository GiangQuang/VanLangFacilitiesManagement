using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;

namespace VLFM.Infrastructure.Repositories
{
    public class ProposeRepository : GenericRepository<ProposeDetails>, IProposeRepository
    {
        public ProposeRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}

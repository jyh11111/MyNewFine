using NFine.Data;
using NFine.Domain.Entity.LegoManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.IRepository.LegoManage
{
    public interface ILegoPartRepository : IRepositoryBase<LegoPartEntity>
    {
        decimal GetPartUnitWeight(string keyword);
    }
}

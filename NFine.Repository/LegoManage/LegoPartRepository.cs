using NFine.Data;
using NFine.Domain.IRepository.LegoManage;
using NFine.Domain.Entity.LegoManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.LegoManage
{
    public class LegoPartRepository : RepositoryBase<LegoPartEntity>, ILegoPartRepository
    {
        public decimal GetPartUnitWeight(string keyword)
        {

            var q1 = this.IQueryable(a => a.F_Id == keyword).FirstOrDefault();
            if (q1 !=null)
            { return q1.F_UnitWeight==null?0: (decimal)q1.F_UnitWeight; }
            else return 0;
            //try
            //{ return (decimal)this.IQueryable(a => a.F_Id == keyword).FirstOrDefault().F_UnitWeight; }
            //catch
            //{
            //    return 0;
            //}
        }
    }
}

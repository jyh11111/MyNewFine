using NFine.Domain.Entity.LegoManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.LegoManage
{
    class LegoPartMap:EntityTypeConfiguration<LegoPartEntity>
    {
        public LegoPartMap()
        {
            this.ToTable("U_LegoPart");
            this.HasKey(t => t.F_Id);
        }
    }
}

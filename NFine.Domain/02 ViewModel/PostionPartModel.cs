using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
   public class PostionPartModel
    {
        public string F_Id { get; set; }
        public string PositionId { get; set; }
        public string PartId { get; set; }
        public int Qty { get; set; }
        public string PartNo { get; set; }
        public string PartDesc { get; set; }
        public string PositionName { get; set; } //位置名称
        public string remark { get; set; } //物料备注
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using NFine.Domain.Entity.LegoManage;

namespace NFine.Domain.ViewModel
{
    public class ReciveTransView
    {
        public string F_id { get; set; }
        public string PartNo { get; set; }
        public string PartDesc { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string FromDept { get; set; }
        public string FromUserName { get; set; }
        public string ReceiveUserName { get; set; }
        public string PositionName { get; set; }
        public int? TransQty { get; set; }
        public string TransUnit { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string Remark { get; set; }
        public string SendTransId { get; set; }
        public Decimal? F_UnitWeight { get; set; }
        public Decimal? TotalWeight { get; set; }
        public string ReciveDept { get; set; }
    }
}
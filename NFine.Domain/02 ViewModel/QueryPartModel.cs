using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
    public class QueryPartModel //将其映射到视图QueryPartView中去 仅做显示不做修改
    {
         
        public string F_Id { get; set; }
        public string PartId { get; set; }
        public string PartNo { get; set; }
        public string PartDesc { get; set; }
        public string PartUnit { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public String DepartmentId { get; set; }
        public int Qty { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }


    }
}

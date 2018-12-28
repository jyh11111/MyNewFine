using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NFine.Domain.ViewModel;
using NFine.Data;
using NFine.Domain.Entity.LegoManage;
using System.Data.Common;
using System.Data.SqlClient;
using NFine.Application.LegoManage;
using NFine.Application.SystemManage;

namespace NFine.Application.LegoManage
{
    public class QueryPartApp
    {

        
        private PostionPartApp ppApp = new PostionPartApp();
        private PositionApp posApp = new PositionApp();
        private LegoPartApp partApp = new LegoPartApp();
        private OrganizeApp deptApp = new OrganizeApp();
        /// <summary>
        /// 计算填充部门ID对应的位置的表[U_PostionPart]
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public int FillPostionPart(string deptid)
        {
            return ppApp.FillPostionPart(deptid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">Partdesc</param>
        /// <returns></returns>
        public List<QueryPartModel> GetList(Pagination pagination, string keyword)
        {
            var op = OperatorProvider.Provider.GetCurrent();
            var deptid = op.DepartmentId;
            var expression = ExtLinq.True<PostionPartEntity>();
            string[] arr = null;
            //不显示注塑车间这个仓库
            expression = expression.And(t => t.PositionId != "acf940bf-5386-4ef8-8285-3f84ca41899a");

            if (!op.IsSystem && !string.IsNullOrWhiteSpace(deptid))
            {   
                var l1= posApp.GetList2(deptid);
                arr=new string[l1.Count];
                for (var i = 0; i < l1.Count; i++)
                {  arr[i]=l1[i].F_Id;
                }

                   expression = expression.And(t =>arr.Contains(t.PositionId));

            }
            string[] arr2 = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                var l2 = partApp.GetList(t => t.PartNo.Contains(keyword));
                 arr2 = l2.Select(t => t.F_Id).ToArray();
                expression = expression.And(t=>arr2.Contains(t.PartId));

            }
          var simpleList=  ppApp.GetList(pagination, expression);
          List<QueryPartModel> ret = new List<QueryPartModel>();
          foreach (var item in simpleList)
          {
               var  p1= partApp.GetForm(item.PartId);
               var p2 = posApp.GetForm(item.PositionId);
               var p3 = deptApp.GetForm(p2.OrganizeId);
               if (p1 != null && p2 != null && p3 != null)
               {
                   ret.Add(new QueryPartModel()
                   {   F_Id=item.F_Id,
                       PartId = item.PartId,
                       PartDesc = p1.PartDesc,
                       PartNo = p1.PartNo,
                       PartUnit = p1.PartUnit,
                       PositionId = item.PositionId,
                       PositionName = p2.PositionName,
                       DepartmentId = p2.OrganizeId,
                       Qty = item.Qty,
                       DepartmentName = p3.F_EnCode,
                       DepartmentDesc = p3.F_FullName

                   });
               };
               
          
          }
          ret.OrderBy(a => a.PartNo);
          return ret;
        }
   
    }
}

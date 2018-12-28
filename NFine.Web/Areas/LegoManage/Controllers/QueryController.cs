using NFine.Application.LegoManage;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.LegoManage.Controllers
{
    public class QueryController : ControllerBase
    {
        //
        // GET: /LegoManage/Query/
        //keyWord是PartDesc的一部分
        private QueryPartApp queryApp = new QueryPartApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyWord)
        {
            var data = new
            {
                rows = queryApp.GetList(pagination, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
      
        public ActionResult FillPostionPart()
        {
            var op = OperatorProvider.Provider.GetCurrent();
            var deptid = "";
            if (! op.IsSystem)
            { deptid = op.DepartmentId; };
            queryApp.FillPostionPart(deptid);
            return Success("计算完毕");
        
        }
    }
}

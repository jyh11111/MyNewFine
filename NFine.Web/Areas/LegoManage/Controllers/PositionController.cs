using NFine.Application.LegoManage;
 
using NFine.Code;
using NFine.Domain.Entity.LegoManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.LegoManage.Controllers
{
    public class PositionController : ControllerBase
    {
        //
        // GET: /LegoManage/Position/

        private PositionApp postionApp = new PositionApp();
        //  private PostionPartApp partApp = new PostionPartApp();
        private SendTransApp sendApp = new SendTransApp();
        private ReceiveTransApp recvApp = new ReceiveTransApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = postionApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(PositionEntity positionEntity, string keyValue)
        {
            postionApp.SubmitForm(positionEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = postionApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {


            if ((sendApp.FindEntity(t => t.FromPostionId.ToLower() == keyValue.ToLower()) != null) ||
                (recvApp.FindEntity(t => t.ReceivePostionId.ToLower() == keyValue.ToLower()) != null))
            { return Error("不可以删除此物品，此物品已经交易过!"); }

            postionApp.DeleteForm(keyValue);
            return Success("删除成功。");

        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledPos(string keyValue)
        {
            PositionEntity posEntity = new PositionEntity();
            posEntity.F_Id = keyValue;
            posEntity.F_EnabledMark = false;
            postionApp.UpdateForm(posEntity);
            return Success("位置禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledPos(string keyValue)
        {
            PositionEntity posEntity = new PositionEntity();
            posEntity.F_Id = keyValue;
            posEntity.F_EnabledMark = true;
            postionApp.UpdateForm(posEntity);
            return Success("位置启用成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson()
        {
            var deptid = "";
            //var usercode = OperatorProvider.Provider.GetCurrent().UserCode.ToLower();
            if (!OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                deptid = OperatorProvider.Provider.GetCurrent().DepartmentId;
            }
            var data = postionApp.GetList2(deptid);
            return Content(data.ToJson());

        }


    }
}

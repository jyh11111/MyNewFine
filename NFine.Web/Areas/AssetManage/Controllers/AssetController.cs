using NFine.Application.AssetManage;
using NFine.Code;
using NFine.Domain.Entity.AssetManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.AssetManage.Controllers
{
    public class AssetController : ControllerBase
    {
        //
        // GET: /AssetManage/Asset/
        private AssetApp assetApp = new AssetApp();
        private AssetStateApp assetStateApp = new AssetStateApp();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = assetApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return //Json(data);
            Content(data.ToJson());

        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(AssetEntity entity, string keyValue)
        { 

            assetApp.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = assetApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        //[HandlerAuthorize]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {

            //if ((sendApp.FindEntity(t => t.PartId.ToLower() == keyValue.ToLower()) != null) ||
            //    (recvApp.FindEntity(t => t.PartId.ToLower() == keyValue.ToLower()) != null))
            //{ return Error("不可以删除此物品，此物品已经交易过!"); }

            assetApp.DeleteFrom(keyValue);
            return Success("删除成功。");


        }

        [HttpGet]
        [HandlerAjaxOnly]
        public JsonResult GetSelectJson()
        {
            var data = assetApp.GetList();
            return Json(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public JsonResult GetNextAssetNum(string prefix)
        {

            var data = new
            {
                AssetId =prefix + assetApp.GetNextAssetNum(prefix),
            };
             
            return Json(data);

        }

        [HttpPost]
       [HandlerAjaxOnly]
        
        public JsonResult GetAssetStateJson(string AssetFId)
        {
            var data = new List<AssetState>();
            data = assetStateApp.GetList(a => a.AssetFId == AssetFId);
           if (data.Count == 0) {
               data.Add(new AssetState() { AssetFId=AssetFId});
           }
           return Json(data);          
  
        }

        [HttpPost]
       //[HandlerAuthorize]
        [HandlerAjaxOnly]
        public ActionResult DeleteAssetState(string id)
        {
            assetStateApp.DeleteFrom(id);
            return Success("删除成功。");

        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public ActionResult SubmitAssetState(AssetState entity, string id)
        {

            assetStateApp.SubmitForm(entity, id);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        public ActionResult EditGrid(string oper,int? state,string AssetFId,string remark,DateTime? Fdate, string id)
        {
            try
            {
                if (oper == "edit")
                {
                    AssetState a1 = new AssetState()
                    {
                        Id = int.Parse(id),
                        state = (ASSET_STATE)state,
                        AssetFId = AssetFId,
                        remark = remark,
                        Fdate = Fdate
                    };
                    assetStateApp.SubmitForm(a1, id.ToString());


                };
                if (oper == "add")
                {
                    AssetState a1 = new AssetState()
                    {
                        state = (ASSET_STATE)state,
                        AssetFId = AssetFId,
                        remark = remark,
                        Fdate = Fdate
                    };
                    assetStateApp.SubmitForm(a1, "");
                };
                if (oper == "del")
                {

                    if (id != null)
                        assetStateApp.DeleteFrom(id);

                };
                return Success("操作成功。");
            }
            catch { return Error("保存失败！"); }
            
        }
    public ActionResult CopyOne(string key){
        
        if(key.Trim()!="")
            assetApp.CopyOne(key);

        return Success("操作成功。");
    }



    }
}

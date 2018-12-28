using NFine.Application.LegoManage;
using NFine.Domain.ViewModel;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;

namespace NFine.Web.Areas.LegoManage.Controllers
{
    public class QueryRecvController : ControllerBase
    {
        //
        // GET: /LegoManage/QueryRecv/
        private ReceiveTransApp recvApp = new ReceiveTransApp();
        const string exeSql = @"SELECT   r.F_id,  P.PartNo, P.PartDesc, S.SendDate, R.ReceiveDate, O.F_FullName AS FromDept, S.FromUserName AS FromUserName,
        R.ReceiveUserName,(select PositionName from U_Position where F_Id=R.ReceivePostionId) as PositionName , R.TransQty, R.TransUnit,
        R.F_CreatorTime, R.Remark, R.SendTransId,P.F_UnitWeight, isnull(R.TransQty,0)*ISNULL(F_UnitWeight,0) as TotalWeight, 
        (select F_FullName From Sys_Organize where F_Id=R.ReceiveOrganizedId) as ReciveDept     
        FROM  dbo.U_ReceiveTrans AS R INNER JOIN dbo.U_SendTrans AS S ON R.SendTransId = S.F_Id INNER JOIN 
        dbo.Sys_Organize AS O ON S.FromOrganizeId = O.F_Id INNER JOIN   dbo.U_LegoPart AS P ON R.PartId = P.F_Id      
        WHERE    (R.ReceiveDate between  @date1 and @date2) ";

        [HttpGet]
      [HandlerAjaxOnly]
        //查询开始日期与结束日期
        public ActionResult GetGridJson(DateTime date1, DateTime? date2, string partno)
        {
            var curOrganizedId = NFine.Code.OperatorProvider.Provider.GetCurrent().DepartmentId;
            if (date1 == null) return Error("请输入查询日期");
            if ((date2 == null) || (date2 < date1)) { date2 = date1; };

            var sql = exeSql;
            if (!NFine.Code.OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                sql = sql + " and R.ReceiveOrganizedId =@curOrganizedId ";
            }
            if (partno != null && partno.Trim() != "")
                sql = sql + " and P.PartNo =@partno";
            var ordfield = Request.QueryString["sidx"] ;
          
            if ((ordfield != "")  ){
            sql=sql +" order by "+ordfield ;
            }

            DbParameter[] param1 = new SqlParameter[] {
                                      new SqlParameter("curOrganizedId",curOrganizedId),
                                       new SqlParameter("date1",date1),
                                       new SqlParameter("date2",date2),
                                        new SqlParameter("partno",partno==null?"":partno.Trim()),                                 
                                 };
            var data = recvApp.Getlist<ReciveTransView>(sql, param1);

            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult ExportXLS(DateTime? date1, DateTime? date2, string partno,string sidx)
        {

            //var xls = new ExcelHelper<ReciveTransView>();
            //MemoryStream fileStream;
            //List<ReciveTransView> list=new List<ReciveTransView>();
            //xls.getExcel(list, new Hashtable(), out fileStream);
           
            //return File(fileStream, "application/ms-excel", "export.xls");  
            var curOrganizedId = NFine.Code.OperatorProvider.Provider.GetCurrent().DepartmentId;
            if (date1 == null) Error("请输入查询日期");
            if ((date2 == null) || (date2 < date1)) { date2 = date1; };

            var sql = exeSql;
            if (!NFine.Code.OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                sql = sql + " and R.ReceiveOrganizedId =@curOrganizedId ";
            }
            if (partno != null && partno.Trim() != "")
                sql = sql + " and P.PartNo =@partno";
            var ordfield = sidx;

            if ((ordfield !=null)|| (ordfield !="") )
            {
                sql = sql + " order by " + ordfield;
            }

            DbParameter[] param1 = new SqlParameter[] {
                                      new SqlParameter("curOrganizedId",curOrganizedId),
                                       new SqlParameter("date1",date1),
                                       new SqlParameter("date2",date2),
                                        new SqlParameter("partno",partno==null?"":partno.Trim()),                                 
                                 };
            var data = recvApp.Getlist<ReciveTransView>(sql, param1);
            var xls = new ExcelHelper<ReciveTransView>();
            MemoryStream fileStream;
            xls.getExcel(data, new Hashtable(), out fileStream);
            return File(fileStream, "application/ms-excel", "exportxls.xls");
            //var fileName = Server.MapPath("~/Files/fileName.xls");
            //return File(fileName, "application/ms-excel", "fileName.xls");  
        
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace NFine.Web
{
    /// <summary>
    /// upload_ajax 的摘要说明
    /// </summary>
    public class upload_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
             
            string action = ContextRequest.GetQueryString("action");

            switch (action)
            {
                case "SingleFile": //单文件
                    SingleFile(context);
                    break;
                case "MultipleFile": //多文件
                    MultipleFile(context);
                    break;
                case "AttachFile": //附件
                    AttachFile(context);
                    break;
                
                case "ExcelFile":  //Excel文件
                    Excelfile(context);
                    break;

            }
        }
        #region 上传单文件处理===================================
        private void Excelfile(HttpContext context)
        {
            string _refilepath = ContextRequest.GetQueryString("ReFilePath"); //取得返回的对象名称
            string _upfilepath = ContextRequest.GetQueryString("UpFilePath"); //取得上传的对象名称
            string _delfile = ContextRequest.GetString(_refilepath);
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _isExcel = false; //默认不打水印

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isExcel);
            //删除已存在的旧文件
            Utils.DeleteUpFile(_delfile);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上传单文件处理===================================
        private void SingleFile(HttpContext context)
        {
            string _refilepath = ContextRequest.GetQueryString("ReFilePath"); //取得返回的对象名称
            string _upfilepath = ContextRequest.GetQueryString("UpFilePath"); //取得上传的对象名称
            string _delfile = ContextRequest.GetString(_refilepath);
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图
            bool _isimage = false;

            if (ContextRequest.GetQueryString("IsWater") == "1")
                _iswater = true;
            if (ContextRequest.GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;
            if (ContextRequest.GetQueryString("IsImage") == "1")
                _isimage = true;

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, _isimage);
            //删除已存在的旧文件
            Utils.DeleteUpFile(_delfile);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上传多文件处理===================================
        private void MultipleFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (ContextRequest.GetQueryString("IsWater") == "1")
                _iswater = true;
            if (ContextRequest.GetQueryString("IsThumbnail") == "1")
                _isthumbnail = true;

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 上传附件处理=====================================
        private void AttachFile(HttpContext context)
        {
            string _upfilepath = context.Request.QueryString["UpFilePath"]; //取得上传的对象名称
            HttpPostedFile _upfile = context.Request.Files[_upfilepath];
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (_upfile == null)
            {
                context.Response.Write("{\"msg\": 0, \"msgbox\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater, false, true);
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

 

       
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public class ContextRequest
    {
        public ContextRequest()
        {

        }
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";
            return HttpContext.Current.Request.QueryString[strName];
        }
        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
 
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName )
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            return HttpContext.Current.Request.Form[strName];
        }
        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
  
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName);
            else
                return GetQueryString(strName);
        }

   
    
    }
}
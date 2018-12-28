

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFine.Domain.IRepository.AssetManage;
using NFine.Domain.Entity.AssetManage;
using NFine.Repository.AssetManage;
using NFine.Code;
using System.Linq.Expressions;
namespace NFine.Application.AssetManage
{
    /// <summary>
    /// AssetApp
    /// </summary>	
    public class AssetApp
    {
        private IAssetRepository service = new AssetRepository();

        public List<AssetEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public AssetEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteFrom(string keyValue)
        {

            service.Delete(t => t.F_Id == keyValue);
        }

        public void SubmitForm(AssetEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
                entity.Create();
                service.Insert(entity);
            }
        }
        public List<AssetEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<AssetEntity>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.AssetName.Contains(keyword) || t.CustomerName.Contains(keyword) || t.AssetId.Contains(keyword) || t.Project.Contains(keyword));
              


            }

            return service.FindList(expression, pagination);
        }

        public List<AssetEntity> GetList(Expression<Func<AssetEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<AssetEntity>();
            return service.IQueryable(predicate).ToList();

        }
        public string GetNextAssetNum(string prefix)
        {
            StringBuilder strSql = new StringBuilder();
            if (prefix.Trim() != "")
            {
                strSql.Append(@"select top 1 * from EQ_asset as a where assetID=(select MAX(assetid) from EQ_Asset  where AssetId like '" + prefix.Trim() + "%'  ) ");
            }
            else {
                strSql.Append(@"select top 1 * from EQ_asset as a where assetID=(select MAX(assetid) from EQ_Asset   ) ");
            }

            List<AssetEntity> assetlist= service.FindList(strSql.ToString());
            if (assetlist.Count == 0)
                return "001";
            else
            {
                var s1 = assetlist[0].AssetId;
                string[] slist = s1.Split('-');
                int len = slist.Count();
                if (len <= 1) { return "001"; };
                int t1=0;
                if (int.TryParse(slist[len - 1], out t1))
                { t1++;
                   return t1.ToString("000");

                }
                else
                { return "001"; }

            }


        }
        public bool CopyOne(string key)
        {
            var pre = "CA-" + (DateTime.Today.Year - 2000).ToString() + "-";
            var maxno =pre + GetNextAssetNum(pre);
            var curuid = NFine.Code.OperatorProvider.Provider.GetCurrent().UserId;
            var curtime = DateTime.Now.ToString("G");
            var curDate = DateTime.Now.ToString("d");
            StringBuilder strSql = new StringBuilder();
            strSql.Append( "insert into EQ_Asset( F_id,AssetCls,AssetId,AssetType,CustomerName ,Quotation,[RequestDeptId]   ,[InvoiceNo]  ,[F_CreatorUserId]  ,[F_CreatorTime] ,E_Number  ,Customer_Content  ,mk_remark   ,MK_inputDate)" +
                                          "select newid(),[AssetCls] ,'"+maxno +"',[AssetType] ,[CustomerName] ,[Quotation]," +
      "[RequestDeptId]   ,[InvoiceNo]  ,'"+curuid +"'  ,'"+curtime +"' ,E_Number  ,Customer_Content  ,mk_remark   ,'"+ curDate +"'"+
      " from EQ_Asset       where F_Id='"+key+"'");

            service.ExecuteSql(strSql.ToString());
            return true;
        }

    }
}




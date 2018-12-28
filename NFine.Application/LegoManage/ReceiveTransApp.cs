
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 17:04:45 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFine.Domain.IRepository.SystemManage;

using NFine.Domain.IRepository.LegoManage;
using NFine.Repository.LegoManage;
using NFine.Domain.Entity.LegoManage;
using NFine.Code;
using System.Linq.Expressions;
using NFine.Domain.ViewModel;
using System.Data.Common;

namespace NFine.Application.LegoManage
{
    /// <summary>
    /// ReceiveTransApp
    /// </summary>	
    public class ReceiveTransApp
    {
        private IReceiveTransRepository service = new ReceiveTransRepository();
        private ILegoPartRepository partserive = new LegoPartRepository();
        public List<ReceiveTransEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        public List<ReceiveTransEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ReceiveTransEntity>();
            var curuser = OperatorProvider.Provider.GetCurrent().UserCode.ToLower();
            var deptid = OperatorProvider.Provider.GetCurrent().DepartmentId;
            if (!OperatorProvider.Provider.GetCurrent().IsSystem && deptid.Trim() != "")
            {
                expression = expression.And(t => t.ReceiveOrganizedId.Equals(deptid, StringComparison.OrdinalIgnoreCase));

            }
            if (!string.IsNullOrEmpty(keyword))
            {
                var partlist = partserive.IQueryable(t => t.PartNo.Contains(keyword)).Select(t => new { t.F_Id }).ToList();
                List<string> pids = new List<string>();
                foreach (var item in partlist)
                {
                    pids.Add(item.F_Id);
                }
                if (pids.Count > 0)
                {
                    expression = expression.And(t => pids.Contains(t.PartId));
                }

            }

            return service.FindList(expression, pagination);
        }


        public ReceiveTransEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteFrom(string keyValue)
        {
            service.Delete(c => c.F_Id == keyValue);
        }

        public void SubmitForm(ReceiveTransEntity entity, string keyValue)
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

        public ReceiveTransEntity FindEntity(Expression<Func<ReceiveTransEntity, bool>> predicate)
        {
            return service.FindEntity(predicate);
        }
        ///不用下面这个
        //public List<ReciveTransView> ConvertTrans(List<ReceiveTransEntity> source)
        //{
        //    List<ReciveTransView> dest = new List<ReciveTransView>();
            
            
        //    foreach (var item in source)
        //    {

        //        var w1 = partserive.GetPartUnitWeight(item.PartId);
        //        dest.Add(
        //            new ReciveTransView()
        //            {
        //                F_Id = item.F_Id,
        //                PartId = item.PartId,
        //                ReceiveDate = item.ReceiveDate,
        //                FromOrganizeId = item.FromOrganizeId,
        //                FromUserName = item.FromUserName,
        //                ReceivePostionId = item.ReceivePostionId,
        //                ReceivePostionName = item.ReceivePostionName,
        //                ReceiveOrganizedId = item.ReceiveOrganizedId,
        //                ReceiveUserName = item.ReceiveUserName,
        //                TransQty = item.TransQty,
        //                TransUnit = item.TransUnit,
        //                F_CreatorUserId = item.F_CreatorUserId,
        //                F_CreatorTime = item.F_CreatorTime,
        //                F_LastModifyTime = item.F_LastModifyTime,
        //                F_LastModifyUserId = item.F_LastModifyUserId,
        //                Remark = item.Remark,
        //                SendTransId = item.SendTransId,
        //                F_UnitWeight=w1,
        //                TotalWeight =(decimal)(w1 * item.TransQty)
        //            }
        //            );
        //    }
             
        //    return dest;
        //}
        public List<TEntity> Getlist<TEntity>(string sql) {

         return   service.FindList2<TEntity>(sql);
        }
        public List<TEntity> Getlist<TEntity>(string sql, DbParameter[] dbParameter)
        {

            return service.FindList2<TEntity>(sql,dbParameter);
        }


    }
}




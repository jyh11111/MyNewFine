
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 17:28:42 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFine.Domain.IRepository.LegoManage;
using NFine.Repository.LegoManage;
using NFine.Domain.Entity.LegoManage;
using NFine.Code;
using System.Linq.Expressions;
namespace NFine.Application.LegoManage
{
    /// <summary>
    /// SendTransApp
    /// </summary>	
    public class SendTransApp
    {
        private ISendTransRepository service = new SendTransRepository();
        private IPostionPartRepository service2 = new PostionPartRepository();
        private ILegoPartRepository partserive = new LegoPartRepository();  
        public List<SendTransEntity> GetList()
        {
            return service.IQueryable().ToList();
        }
        /// <summary>
        /// 只取当前用户当前部门的发送Part资料 admin取所有资料
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">用户名中包含</param>
        /// <returns></returns>
        public List<SendTransEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<SendTransEntity>();
            var curuser = OperatorProvider.Provider.GetCurrent().UserCode.ToLower();
            var deptid = OperatorProvider.Provider.GetCurrent().DepartmentId;
            if (!OperatorProvider.Provider.GetCurrent().IsSystem && deptid.Trim() != "")
            {
                expression = expression.And(t => t.FromOrganizeId.Equals(deptid, StringComparison.OrdinalIgnoreCase));

            }
            if (!string.IsNullOrEmpty(keyword))
            {
                var partlist = partserive.IQueryable(t => t.PartNo.Contains(keyword)).Select(t => new { t.F_Id }).ToList();
                List<string> pids=new List<string>();
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
        /// <summary>
        /// 取未发送的，并且接收门等于当前用户所在部门
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SendTransEntity> GetList2(Pagination pagination)
        {
            var expression = ExtLinq.True<SendTransEntity>();
            var op = OperatorProvider.Provider.GetCurrent();
            
            
          expression=  expression.And(t => t.Received != true);
          if (!op.IsSystem && !string.IsNullOrWhiteSpace( op.DepartmentId))
            {
                expression = expression.And(t => t.ToOrganizedId.Equals(op.DepartmentId, StringComparison.OrdinalIgnoreCase));

            }
           
            return service.FindList(expression, pagination);
            
        }
        public SendTransEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteFrom(string keyValue)
        {
            service.Delete(c => c.F_Id == keyValue);
        }

        public void SubmitForm(SendTransEntity entity, string keyValue)
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
        public void changeRecived(string keyValue, bool flag = true)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
             var entity=   service.FindEntity(keyValue);
             if (entity != null)
             {
                 entity.Received = flag;
                 service.Update(entity);
             }
            }                    
        }
        public SendTransEntity FindEntity(Expression<Func<SendTransEntity, bool>> predicate)
        {
            return service.FindEntity(predicate);
        }
        /// <summary>
        /// 在发送之前检查在某柜中的某Part数量是否为此值.若小于现存量则返回False
        /// </summary>
        /// <param name="entity">将会存进去的发送</param>       
        /// <returns></returns>
        public bool CheckSubmit(SendTransEntity entity)
        {
            //0.若是注塑车间则返回True; 或者是洪嘉注塑车间
            if (entity.FromPostionId.ToLower() == "acf940bf-5386-4ef8-8285-3f84ca41899a" || entity.FromPostionId.ToLower() == "1d50cab4-237d-4742-a508-b46736f69da4")
                return true;
            //1.先将发出部门的数据进行一下统计
            string deptid = entity.FromOrganizeId;
            string posid = entity.FromPostionId;
            service2.FillPostionPart(deptid);
           //2.从PositionPart表中查找一下
            var obj1 = service2.FindEntity(t => t.PositionId.ToLower() == posid.ToLower() && t.PartId.ToLower() == entity.PartId.ToLower() && t.Qty>=entity.TransQty);
            if (obj1 == null)
            { return false; }
            else {
                return true;
            }
        }
    }
}




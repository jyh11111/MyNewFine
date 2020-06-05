
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 14:42:51 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
 
using NFine.Domain.Entity.LegoManage;
using NFine.Domain.IRepository.LegoManage;
using NFine.Repository.LegoManage;
using NFine.Code;
using NFine.Application.LegoManage;
namespace NFine.Application.LegoManage
{
    /// <summary>
    /// PositionApp
    /// </summary>	
    public class PositionApp
    {
        private IPositionRepository service = new PositionRepository();
       

        public List<PositionEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public PositionEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
         
            
            service.Delete(c => c.F_Id == keyValue);
        }

        public void UpdateForm(PositionEntity entity)
        {
            service.Update(entity);
        }
        public void SubmitForm(PositionEntity entity, string keyValue)
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
        public List<PositionEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<PositionEntity>();
            var curuser = OperatorProvider.Provider.GetCurrent().UserCode;
            var deptid = OperatorProvider.Provider.GetCurrent().DepartmentId;
            if (!(string.IsNullOrWhiteSpace(deptid) || OperatorProvider.Provider.GetCurrent().IsSystem))
                expression = expression.And(t => t.OrganizeId == deptid);
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.PositionName.Contains(keyword));

            }

            return service.FindList(expression, pagination);
        }

        public List<PositionEntity> GetList2(string deptid)
        {
            var expression = ExtLinq.True<PositionEntity>();

            if (!string.IsNullOrWhiteSpace(deptid))
            {
                expression = expression.And(t => t.OrganizeId==deptid);

            }

            return service.IQueryable(expression).OrderBy(t => t.F_CreatorTime).ToList();

        }
    }
}




using NFine.Code;
using NFine.Domain.Entity.LegoManage;
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.LegoManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NFine.Application.LegoManage
{
    public class LegoPartApp
    {
        private LegoPartRepository service = new  LegoPartRepository();
        public List< LegoPartEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public  LegoPartEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteFrom(string keyValue)
        {

            service.Delete(c => c.F_Id == keyValue);
        }

        public void SubmitForm( LegoPartEntity entity, string keyValue)
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

        public List<LegoPartEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<LegoPartEntity>();
          
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t =>t.PartNo.Contains(keyword));

            }

            return service.FindList(expression, pagination);
        }

        public List<LegoPartEntity> GetList(Expression<Func<LegoPartEntity, bool>> predicate)
        {
            var expression = ExtLinq.True<LegoPartEntity>();
            return service.IQueryable(predicate).ToList();
        
        }
        public decimal GetPartUnitWeight(string keyword)
        {
            try
            { return (decimal)service.IQueryable(a => a.F_Id == keyword).FirstOrDefault().F_UnitWeight; }
            catch
            {
                return 0;
            }
                   
        }

    }
}

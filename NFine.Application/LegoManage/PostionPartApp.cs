
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 16:35:35 by 枫伶忆
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
	/// PostionPartApp
	/// </summary>	
	public class PostionPartApp
	{
	    private IPostionPartRepository service=new PostionPartRepository();

		public List<PostionPartEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

	    public PostionPartEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
      
        public void DeleteFrom(string keyValue)
        {        
                service.Delete(c=>c.F_Id==keyValue);            
        }
		 
		public void SubmitForm(PostionPartEntity entity, string keyValue)
        {  
              if ( !string.IsNullOrEmpty(entity.F_Id) && ( entity.F_Id!=keyValue))
            {
                  throw new ArgumentException("实体ID与KeyValue不相等");
                 return;
              }

            if (!string.IsNullOrEmpty(keyValue))
            {   //service.Update(entity)
               // entity.Modify(keyValue);
                service.Update(entity);
            }
            else
            {
              //  entity.Create();
                service.Insert(entity);
            }
        }
        public int FillPostionPart(string deptid)
        {
         return   service.FillPostionPart(deptid);
        }
        public List<PostionPartEntity> GetList(Pagination pagination, Expression<Func<PostionPartEntity, bool>> predicate)
        {
           
             return service.FindList(predicate, pagination);
           
        }

    }
}




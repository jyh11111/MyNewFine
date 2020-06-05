
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
using NFine.Domain.ViewModel;
using NFine.Data;
 

namespace NFine.Application.LegoManage
{	
	/// <summary>
	/// PostionPartApp
	/// </summary>	
	public class PostionPartApp
	{
         
        public IPostionPartRepository service=new PostionPartRepository();
        public LegoPartApp partApp = new LegoPartApp();
        public NFineDbContext dbcontext = new NFineDbContext();
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
        public List<PostionPartModel> GetList(Pagination pagination, string keyword, string postionId ) {

                
           // var curuser = OperatorProvider.Provider.GetCurrent().UserCode;
            var deptid = OperatorProvider.Provider.GetCurrent().DepartmentId;          


            var sql = @"select A.*,P.partno,P.PartDesc ,P.remark,U_Position.PositionName from U_PostionPart A inner join U_LegoPart P on A.PartId = P.F_id  inner join U_Position on U_Position.F_Id = A.PositionId where 1=1 ";
            if (!string.IsNullOrWhiteSpace(postionId))
            { sql += " and A.PositionId='" + postionId.Trim() + "'"; }
            if (!string.IsNullOrWhiteSpace(keyword)) {
                sql += " and P.partno like '%" + keyword.Trim() + "%' ";
            }
            var countsql = "select count(1) as total from (" + sql + ") tmp";
            int count = 0;
            List<CountViewModel> cv = service.FindList2<CountViewModel>(countsql);
            if (cv != null && cv.Count > 0) {
                count = cv[0].total;
                pagination.records = count;
            } 
            var pv = service.FindList2<PostionPartModel>(sql);

            return pv;

        }

    }
}




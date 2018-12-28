
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 16:36:19 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------
using NFine.Data;
using NFine.Domain.Entity.LegoManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.LegoManage;
using NFine.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Common;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.LegoManage
{
    /// <summary>
    /// U_PostionPartRepository
    /// </summary>	
    public class PostionPartRepository : RepositoryBase<PostionPartEntity>, IPostionPartRepository
    {
        
        public int FillPostionPart(string deptid)
        {

            StringBuilder strSql = new StringBuilder();
            List<PostionPartEntity> pplist = new List<PostionPartEntity>();


            if (!string.IsNullOrWhiteSpace(deptid))
            {
                strSql.Append(@"select '' as F_Id, PartId,PositionId,SUM(tmp.qty)as Qty from
                (
                select partid,ReceivePostionId as PositionId,SUM(TransQty)as qty from U_ReceiveTrans  group by PartId,ReceivePostionId
                union all
                select PartId,FromPostionId as PositionId,SUM(transqty)*-1 as qty from U_SendTrans  group by PartId,FromPostionId) as tmp
                WHERE   EXISTS( SELECT 1 FROM U_Position WHERE OrganizeId=@deptid AND TMP.PositionId=U_Position.F_Id)
                group by tmp.PartId,tmp.PositionId");
                DbParameter[] parameter = 
            {
                 new SqlParameter("@deptid",deptid)
            };
                pplist = dbcontext.Database.SqlQuery<PostionPartEntity>(strSql.ToString(), parameter).ToList<PostionPartEntity>();
            }
            else
            {
                strSql.Append(@"select '' as F_Id , PartId,PositionId,SUM(tmp.qty)as Qty from
                    ( select partid,ReceivePostionId as PositionId,SUM(TransQty)as qty from U_ReceiveTrans  group by PartId,ReceivePostionId
                        union all
                        select PartId,FromPostionId as PositionId,SUM(transqty)*-1 as qty from U_SendTrans  group by PartId,FromPostionId) as tmp
                        group by tmp.PartId,tmp.PositionId");
                pplist = dbcontext.Database.SqlQuery<PostionPartEntity>(strSql.ToString()).ToList<PostionPartEntity>();
            }
            foreach (var item in pplist)
            {
                var entity1 = FindEntity(t=>t.PartId==item.PartId && t.PositionId==item.PositionId);
                if (entity1 == null)
                {
                    if (item.Qty != 0)
                    {
                        item.F_Id = System.Guid.NewGuid().ToString();
                        Insert(item);
                    }
                }
                else
                {
                    if (item.Qty == 0)
                    {
                        Delete(entity1);
                    }
                    else
                    {
                        entity1.Qty = item.Qty;
                        Update(entity1);
                    }
                }             
            
            }

            return pplist.Count();
        }

    }
}




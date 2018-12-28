
//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 16:36:11 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------

using NFine.Domain.Entity.LegoManage;
using System.Data.Entity.ModelConfiguration;
namespace NFine.Mapping.LegomManage
{	
	/// <summary>
	/// U_PostionPartMap
	/// </summary>	
	public class  PostionPartMap:EntityTypeConfiguration<PostionPartEntity>
	{
	   public  PostionPartMap()
	   {
	      this.ToTable("U_PostionPart");
		  this.HasKey(t=>t.F_Id);
	   }
    }
}




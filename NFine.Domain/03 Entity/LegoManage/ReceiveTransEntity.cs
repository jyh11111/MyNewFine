

//------------------------------------------------------------------------------
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//     此代码由T4模板自动生成
//	   生成时间 2016-11-22 17:04:54 by 枫伶忆
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//     QQ:549387177
// <博客园-枫伶忆 http://www.cnblogs.com/fenglingyi/>
//------------------------------------------------------------------------------

using System;
namespace NFine.Domain.Entity.LegoManage
{	
	/// <summary>
	/// ReceiveTransEntity
	/// </summary>	
	public class ReceiveTransEntity:IEntity<ReceiveTransEntity>, ICreationAudited, IModificationAudited
	{
	  public string F_Id { get; set; }
	  public string PartId { get; set; }
	 
	  public DateTime? ReceiveDate { get; set; }
	  public string FromOrganizeId { get; set; }
 
	  public string FromUserName { get; set; }
	  public string ReceivePostionId { get; set; }
	  public string ReceivePostionName { get; set; }
	  public string ReceiveOrganizedId { get; set; }
	 
	  public string ReceiveUserName { get; set; }
	  public int? TransQty { get; set; }
	  public string TransUnit { get; set; }
	  public string F_CreatorUserId { get; set; }
	  public DateTime? F_CreatorTime { get; set; }
	  public DateTime? F_LastModifyTime { get; set; }
	  public string F_LastModifyUserId { get; set; }
	  public string Remark { get; set; }
      public string SendTransId { get; set; }
 
    }
}




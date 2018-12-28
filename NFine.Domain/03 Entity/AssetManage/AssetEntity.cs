

 

using System;
namespace NFine.Domain.Entity.AssetManage
{	
	/// <summary> 
	/// AssetEntity
	/// </summary>	
    public class AssetEntity : IEntity<AssetEntity>, ICreationAudited, IModificationAudited
	{
	  public string F_Id { get; set; }
	  public string AssetId { get; set; }
      public int AssetCls { get; set; }//0..代表客供设备 1..代表自制设备
	  public string AssetType { get; set; }
	  public string AssetName { get; set; }
	  public string CustomerName { get; set; }
	  public string Quotation { get; set; }
	  public string RequestDeptId { get; set; }
	  public string InvoiceNo { get; set; }
	  public string OwnerDeptId { get; set; }
	  public string Project { get; set; } //项目编号
	  public string AssetCustomerId { get; set; }
	  public DateTime? HandOverDate { get; set; }
	  public string RecvDeptId { get; set; }
	  public string AssetPhoto { get; set; }
	  public short? RecvState { get; set; }
	  public string RecvLocation { get; set; }
	  public string F_CreatorUserId { get; set; }
	  public DateTime? F_CreatorTime { get; set; } //市场录入时间
	  public bool? F_EnabledMark { get; set; }
	  public string Remark { get; set; }
	  public string F_ENGUserId { get; set; }
	  public string F_LocationUserId { get; set; }
      public string F_LastModifyUserId{ get; set; }
      public DateTime? F_LastModifyTime { get; set; }
      public string E_Number { get; set; }//報價信息傳遞單(E number)
      public string Customer_Content { get; set; }//客戶聯絡人
      public string AssetFactoryID { get; set; } //出厂编号
      public string AssetSN { get; set; } //SN号码
      public bool? AssetStandard { get; set; } //是否通用件，如果勾选通用, 項目就不用填, 否則項目一定要填.
      public DateTime? EN_inputDate { get; set; }//工程录入日期 自动生成也可更改
      public int? Asset_state { get; set; }
        ///* 绿 黄 红灯状态
        // * MK 3 發票編號, 由制表日期起計14日內綠燈, 15-21日黃, 22日+紅
        // * EN7/由制表日期起計7日內綠燈, 8-14日黃, 15日+紅
        // * AS 2/ 當EN輸入接收日期後7日內確認並有判斷ACCEPT/AOD綠燈, 8-14日未完成黃, 15日+紅. 任何時侯REJECT立即轉紅
        // * /
      public DateTime? AS_inputDate { get; set; }//使用部门录入时间

      public DateTime? MK_inputDate { get; set; } //市场部录入时间
      public string MK_Remark { get; set; } //市场部备注
      public string ProjectDesc { get; set; } //项目名称
      
    }
}




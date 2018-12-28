
 
using NFine.Domain.Entity.AssetManage;
using NFine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;
namespace NFine.Mapping.AssetManage
{	
	/// <summary>
	/// AssetMap
	/// </summary>	
	public class AssetMap:EntityTypeConfiguration<AssetEntity>
	{
	   public AssetMap()
	   {
	      this.ToTable("EQ_Asset");
		  this.HasKey(t=>t.F_Id);
	   }
    }
}




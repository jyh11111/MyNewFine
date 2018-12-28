
 
using NFine.Domain.Entity.AssetManage;
using NFine.Domain.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;
namespace NFine.Mapping.AssetManage
{	
	/// <summary>
	/// AssetMap
	/// </summary>	
	public class AssetStateMap:EntityTypeConfiguration<AssetState>
	{
        public AssetStateMap()
	   {
	      this.ToTable("EQ_AssetState");
		  this.HasKey(t=>t.Id);
	   }
    }
}




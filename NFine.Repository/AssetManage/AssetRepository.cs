
using NFine.Data;
using NFine.Domain.Entity.AssetManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.AssetManage;
using NFine.Domain.IRepository.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.AssetManage
{	
	/// <summary>
	/// AssetRepository
	/// </summary>	
	public class AssetRepository:RepositoryBase<AssetEntity>,IAssetRepository
	{

        public int Update(AssetEntity entity)
        {
            string[] arr = new string[] { "handoverdate", "en_inputdate", "as_inputdate", "mk_inputdate" };
            dbcontext.Set<AssetEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                }
                else if (arr.Contains( prop.Name.ToLower()) )
                {
                    dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbcontext.SaveChanges();
        }
    }
}




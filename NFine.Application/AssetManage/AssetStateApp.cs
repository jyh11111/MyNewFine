

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFine.Domain.IRepository.AssetManage;
using NFine.Domain.Entity.AssetManage;
using NFine.Repository.AssetManage;
using NFine.Code;
using System.Linq.Expressions;
namespace NFine.Application.AssetManage
{
    /// <summary>
    /// AssetApp
    /// </summary>	
    public class AssetStateApp
    {
        private IAssetStateRepository service = new AssetStateRepository();

        public List<AssetState> GetList()
        {
            return service.IQueryable().ToList();
        }

        public AssetState GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteFrom(string keyValue)
        {

            service.Delete(t => t.Id == int.Parse(keyValue));
        }

        public void SubmitForm(AssetState entity, string keyValue)
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
        public List<AssetState> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<AssetState>();

            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Fdate== DateTime.Parse(keyword));           
            }
            return service.FindList(expression, pagination);
        }

        public List<AssetState> GetList(Expression<Func<AssetState, bool>> predicate)
        {
            var expression = ExtLinq.True<AssetState>();
            return service.IQueryable(predicate).ToList();

        }
      

    }
}




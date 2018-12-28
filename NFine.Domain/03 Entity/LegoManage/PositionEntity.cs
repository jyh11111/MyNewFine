using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.LegoManage
{
    public class PositionEntity : IEntity<PositionEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        public string F_Id { get; set; }
        public string PositionName { get; set; }
        public string OrganizeId { get; set; }
        public bool? F_DeleteMark { get; set; }
        public bool? F_EnabledMark { get; set; }
       
        public DateTime? F_CreatorTime { get; set; }
        public string  F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string  F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string  F_DeleteUserId { get; set; }
        public string  Remark { get; set; }
    }
}

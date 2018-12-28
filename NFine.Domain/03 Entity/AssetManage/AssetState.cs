using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Entity.AssetManage
{
    public class AssetState : IEntity<AssetState>
    {
        public int Id { get; set; }
        public string AssetFId { get; set; }//对应表EQ_Asset表的FId值　
        public DateTime?  Fdate { get; set; }
        public ASSET_STATE state { get; set; }
        public string remark { get; set; }
    }

    public enum ASSET_STATE { 
            RUNNING=1, //正常运作
            REPAIRING,    //坏机维修中
            BADWAITING, //坏机等工程市场确认解决方法
            BACKEN, //退回给工程部
            SCRAPPED  //报废 
    }
}

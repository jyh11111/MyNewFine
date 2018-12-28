using System.Web.Mvc;

namespace NFine.Web.Areas.LegoManage
{
    public class LegoManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "LegoManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "LegoManage_default",
                "LegoManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

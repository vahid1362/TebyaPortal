using AutoMapper;
using Portal.core.News;
using QTasMarketing.Web.Areas.Admin.Models.Content;

namespace QTasMarketing.Web.Areas.Admin.Extentions
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            #region Group

            CreateMap<GroupViewModel, Group>();
            CreateMap<Group, GroupViewModel>();

            #endregion

            #region Content

            CreateMap<Content, ContentViewModel>();
            CreateMap<ContentViewModel, Content>();


            #endregion

        }
    }
}

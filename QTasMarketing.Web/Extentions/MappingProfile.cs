using AutoMapper;
using Portal.core.News;
using Portal.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Extentions
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

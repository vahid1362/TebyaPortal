using AutoMapper;
using Portal.core.News;
using Portal.Standard.Core.News;
using QTasMarketing.Web.Areas.Admin.Models.Content;
using QTasMarketing.Web.Areas.Admin.Models.Media;

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

            CreateMap<ContentPicture, ContentPictureViewModel>();
            CreateMap<ContentPictureViewModel, ContentPicture>();



            #endregion

        }
    }
}

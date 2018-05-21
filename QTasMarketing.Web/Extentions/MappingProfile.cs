using AutoMapper;
using Portal.Web.Framework.ViewModel.Content;
using QtasMarketing.Core.News;
using QTasMarketing.Web.Framework.ViewModel.Content;

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

        }
    }
}

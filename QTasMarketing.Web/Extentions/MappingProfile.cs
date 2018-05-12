using AutoMapper;
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

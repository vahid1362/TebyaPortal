using QtasMarketing.Core.News;
using QTasMarketing.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Framework.Profile
{
  public  class GroupProfile:AutoMapper.Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupViewModel, Group>();
            CreateMap<Group, GroupViewModel>();
        }
    }
}

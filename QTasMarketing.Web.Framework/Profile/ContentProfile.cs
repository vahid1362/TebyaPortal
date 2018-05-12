using QtasMarketing.Core.News;
using QTasMarketing.Web.Framework.ViewModel.Content;

namespace QTasMarketing.Web.Framework.Profile
{
   public class ContentProfile:AutoMapper.Profile
    {
        public ContentProfile()
        {
            CreateMap<Content, ContentViewModel>();
            CreateMap<ContentViewModel, Content>();
        }
    }
}

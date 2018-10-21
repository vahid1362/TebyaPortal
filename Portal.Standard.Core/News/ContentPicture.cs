using Portal.core;
using Portal.core.Media;
using Portal.core.News;
using Portal.Standard.Core.Media;

namespace Portal.Standard.Core.News
{
  public   class ContentPicture:BaseEntity
    {
        public long PictureId { get; set; }

        public long  ContentId { get; set; }

   
        public int DisplayOrder { get; set; }

        public Picture Picture { get; set; }

        public Content Content { get; set; }
    }
}

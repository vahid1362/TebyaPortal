using System;
using System.Collections.Generic;
using System.Text;
using Portal.core.Media;
using QtasMarketing.Core;

namespace Portal.core.News
{
  public   class ContentPicture:BaseEntity
    {
        public long PictureId { get; set; }

        public long  ContentId { get; set; }

        public Picture Picture { get; set; }

        public Content Content { get; set; }
    }
}

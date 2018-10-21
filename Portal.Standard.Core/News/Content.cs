﻿using System.Collections.Generic;
using Portal.Standard.Core.News;


namespace Portal.core.News
{
    public class Content : BaseEntity
    {
        public Content()
        {
          
            ContentPictures=new HashSet<ContentPicture>();
        }

        public string Title { get; set; }

        public string Lead { get; set; }

        public string Body { get; set; }

        public bool IsAllowedComment { get; set; }

        public bool Hidden { get; set; }

        public int Hit { get; set; }

        public decimal Rate { get; set; }

        public Group Group { get; set; }

        public long GroupId { get; set; }
        
        public ICollection<ContentPicture> ContentPictures { get; set; }
    }
}

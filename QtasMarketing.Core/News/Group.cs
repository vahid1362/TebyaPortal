namespace QtasMarketing.Core.News
{
    public class Group : BaseEntity
    {
        public string Title { get; set; }

        public int Priority { get; set; }

        public string Description { get; set; }

        public bool IsPrivate { get; set; }
    }
}

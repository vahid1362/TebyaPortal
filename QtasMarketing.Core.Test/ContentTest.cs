using QtasMarketing.Core.News;
using QtasMarketing.CrossCutting.Enum;
using Xunit;

namespace QtasMarketing.Core.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Is_New_Content_StatusSent()
        {
            var content = new Content();
            Assert.Equal(ContentStatus.Sent,content.ContentStatus);

        }
    }
}

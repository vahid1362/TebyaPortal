using System;

using Moq;
using QtasMarketing.Core.Infrastructure;
using QtasMarketing.Core.News;
using QtasMarketing.Infrastructure;
using QtasMarketing.Services.News;

using Xunit;

namespace QtasMarketing.Services.Test
{
    public class NewsServiceTest
    {
        private IRepository<Group> _groupRepository;
        private IRepository<Content> _contRepository;
      
        private INewsService _newsService;

        public NewsServiceTest()
        {
            _groupRepository =new Mock<IRepository<Group>>().Object;
            _contRepository = new Mock<IRepository<Content>>().Object;
            _newsService=new NewsService(_contRepository,_groupRepository);
          
        }

        [Fact]
        public void Get_Group_Test()
        {
  
            var groups = _newsService.GetGroups();

            Assert.NotNull(groups);
        }

        [Fact]
        public void Add_Group_Test()
        {
            var group=new Group()
            {
                Description = "تست 1",
                IsPrivate = false,
                Priority = 10,
                Title = "فن اوری اطلاعات"
            };
            _newsService.AddGroup(group);
            Assert.NotEqual(0,group.Id);
        }

        [Fact]
        public void Add_Content_Test()
        {
            var content = new Content()
            {
                Title = "تست",
                Body = "تست 2",
                Hit = 1

            };
            _newsService.AddContent(content);
            Assert.NotEqual(0, content.Id);
        }

    }
}

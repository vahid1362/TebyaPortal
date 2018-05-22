using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portal.core.News;
using Portal.Service.News;
using QtasMarketing.Core.Infrastructure;

namespace QtasMarketing.Services.News
{
    public class NewsService : INewsService
    {
        private IRepository<Content> _contentRepository;
        private IRepository<Group> _groupRepository;
        public NewsService(IRepository<Content> contentRepository,IRepository<Group> groupRepository)
        {
            _contentRepository = contentRepository;
            _groupRepository = groupRepository;
        }




        #region Content
     
        public void AddContent(Content content)
        {
            _contentRepository.Insert(content);
        }

       

        #endregion

        #region Group

        public void AddGroup(Group group)
        {

            _groupRepository.Insert(group);
        }

        public List<Group> GetGroups()
        {
            return _groupRepository.TableNoTracking.ToList();
        }

        public Group GetGroup(long id)
        {
            return _groupRepository.TableNoTracking.FirstOrDefault(x=>x.Id==id);
        }

        public void EditGroup(Group @group)
        {
            _groupRepository.Update(group);
        }

       

        public List<Content> GetContents()
        {
            return _contentRepository.TableNoTracking.ToList();
        }

       

        #endregion
    }
}

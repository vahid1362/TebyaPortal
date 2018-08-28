using System;
using System.Collections.Generic;
using System.Linq;
using Portal.core.Infrastructure;
using Portal.core.News;

namespace Portal.Service.News
{
    public class NewsService : INewsService
    {
        private readonly IRepository<Content> _contentRepository;
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<ContentPicture> _contetPictuRepository;
        public NewsService(IRepository<Content> contentRepository,IRepository<Group> groupRepository, IRepository<ContentPicture> contetPictuRepository)
        {
            _contentRepository = contentRepository;
            _groupRepository = groupRepository;
            _contetPictuRepository = contetPictuRepository;
        }
        

        #region Content
     
        public void AddContent(Content content)
        {
            _contentRepository.Insert(content);
        }

        public void AddContentPicture(ContentPicture contentPicture)
        {
            _contetPictuRepository.Insert(contentPicture);
        }

        public List<ContentPicture> GetContentPictures(long contentId)
        {
            return _contetPictuRepository.TableNoTracking.Where(x => x.ContentId == contentId).ToList();
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

        public Group GetGroupById(long id)
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


        public Content GetContentById( long id)
        {
            return _contentRepository.TableNoTracking.FirstOrDefault(x => x.Id == id);
        }

        public void EditContent(Content content)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

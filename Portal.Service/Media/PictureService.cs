using System;
using System.Collections.Generic;
using System.Text;
using Portal.core;
using Portal.core.Media;
using QtasMarketing.Core.Infrastructure;

namespace Portal.Service.Media
{
    public class PictureService : IPictureService
    {
        public IRepository<Picture> _pictureRepository;

        #region Ctor

        public PictureService(IRepository<Picture> pictureRepository) => _pictureRepository = pictureRepository;


        #endregion

        #region crud
        public Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = new Picture
            {
                PictureBinary =  pictureBinary,
                MimeType = mimeType,
                SeoFilename = seoFilename,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                IsNew = isNew,
            };
            _pictureRepository.Insert(picture);
                     

            return picture;
        }

        private byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

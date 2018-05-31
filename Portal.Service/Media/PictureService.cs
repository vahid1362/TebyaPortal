using System.IO;
using ImageResizer;
using Portal.core;
using Portal.core.Media;
using QtasMarketing.Core.Infrastructure;
using  System.Drawing;
using ImageSharp;
using ImageSharp.Processing;

namespace Portal.Service.Media
{
    public class PictureService : IPictureService
    {
        private IRepository<Picture> pictureRepository;
        private readonly MediaSettings _mediaSettings;

        public MediaSettings MediaSettings => _mediaSettings;

        public IRepository<Picture> PictureRepository
        {
            get => pictureRepository;
            set => pictureRepository = value;
        }

        #region Ctor

        public PictureService(IRepository<Picture> pictureRepository) => PictureRepository = pictureRepository;


        #endregion

        #region crud

        public Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = new Picture
            {
                PictureBinary = pictureBinary,
                MimeType = mimeType,
                SeoFilename = seoFilename,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                IsNew = isNew,
            };
            PictureRepository.Insert(picture);


            return picture;
        }

        public object GetPictureUrl(int model, int pictureSize, bool b)
        {
            throw new System.NotImplementedException();
        }

        public object GetDefaultPictureUrl(int pictureSize)
        {
            throw new System.NotImplementedException();
        }

        public object GetPictureById(int model)
        {
            throw new System.NotImplementedException();
        }

        private byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            using (var destStream = new MemoryStream())
            {
                //ImageBuilder.Current.Build(pictureBinary, destStream, new ResizeSettings
                //{
                //    MaxWidth = MediaSettings.MaximumImageSize,
                //    MaxHeight = MediaSettings.MaximumImageSize,
                //    Quality = MediaSettings.DefaultImageQuality
                //});



                return destStream.ToArray();

                }

            #endregion

        }
    }
}

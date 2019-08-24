using System;
using System.IO;
using Portal.core;
using Portal.core.Media;
using System.Linq;
using System.Threading;
using Portal.core.Data;
using Portal.core.Events;
using Portal.core.Infrastructure;
using Portal.Service.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static SixLabors.ImageSharp.Configuration;
using Image = SixLabors.ImageSharp.Image;
using Size = SixLabors.Primitives.Size;
using Portal.Standard.Core.Media;
using Portal.Standard.Core.Infrastructure;

namespace Portal.Service.Media
{
    public class PictureService : IPictureService
    {

        #region Feild

        private readonly IDataProvider _dataProvider;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPortalFileProvider _fileProvider;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly MediaSettings _mediaSettings;
        private readonly IRepository<PictureBinary> _pictureBinaryRepository;

        #endregion
     
        #region Const



        #endregion


        #region Ctor

        public PictureService(IRepository<Picture> pictureRepository, IRepository<PictureBinary> pictureBinaryRepository)
        {
            _pictureRepository = pictureRepository;
            _pictureBinaryRepository = pictureBinaryRepository;
        }

        public PictureService()
        {
        }


        #endregion

        #region Crud
        public Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, string altAttribute = null,
            string titleAttribute = null, bool isNew = true, bool validateBinary = true)
        {
            mimeType = CommonHelper.EnsureNotNull(mimeType);
            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

            if (validateBinary)
                pictureBinary = ValidatePicture(pictureBinary, mimeType);

            var picture = new Picture
            {
                MimeType = mimeType,
                SeoFilename = seoFilename,
                AltAttribute = altAttribute,
                TitleAttribute = titleAttribute,
                IsNew = isNew,
                PictureBinary = pictureBinary
            };
            _pictureRepository.Insert(picture);
          

            return picture;
        }

        /// <summary>
        /// Validates input picture dimensions
        /// </summary>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary or throws an exception</returns>
        public virtual byte[] ValidatePicture(byte[] pictureBinary, string mimeType)
        {
            using (var image = Image.Load(pictureBinary, out var imageFormat))
            {
                //resize the image in accordance with the maximum size
                if (Math.Max(image.Height, image.Width) > _mediaSettings.MaximumImageSize)
                {
                    image.Mutate(imageProcess => imageProcess.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(_mediaSettings.MaximumImageSize)
                    }));
                }

                return EncodeImage(image, imageFormat);
            }
        }

        /// <summary>
        /// Encode the image into a byte array in accordance with the specified image format
        /// </summary>
        /// <typeparam name="T">Pixel data type</typeparam>
        /// <param name="image">Image data</param>
        /// <param name="imageFormat">Image format</param>
        /// <param name="quality">Quality index that will be used to encode the image</param>
        /// <returns>Image binary data</returns>
        protected virtual byte[] EncodeImage<T>(Image<T> image, IImageFormat imageFormat, int? quality = null) where T : struct, IPixel<T>
        {
            using (var stream = new MemoryStream())
            {
                var imageEncoder = Default.ImageFormatsManager.FindEncoder(imageFormat);
                switch (imageEncoder)
                {
                    case JpegEncoder jpegEncoder:
                        jpegEncoder.IgnoreMetadata = true;
                        jpegEncoder.Quality = quality ?? _mediaSettings.DefaultImageQuality;
                        jpegEncoder.Encode(image, stream);
                        break;

                    case PngEncoder pngEncoder:
                        pngEncoder.ColorType = PngColorType.RgbWithAlpha;
                        pngEncoder.Encode(image, stream);
                        break;

                    case BmpEncoder bmpEncoder:
                        bmpEncoder.BitsPerPixel = BmpBitsPerPixel.Pixel32;
                        bmpEncoder.Encode(image, stream);
                        break;

                    case GifEncoder gifEncoder:
                        gifEncoder.IgnoreMetadata = true;
                        gifEncoder.Encode(image, stream);
                        break;

                    default:
                        imageEncoder.Encode(image, stream);
                        break;
                }

                return stream.ToArray();
            }
        }
        public Picture GetPictureById(long model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Getting picture local path/URL methods

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <returns>Picture binary</returns>
        public virtual byte[] LoadPictureBinary(Picture picture)
        {
            return null;
        }



        /// <summary>
        /// Gets the default picture URL
        /// </summary>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Picture URL</returns>
        public virtual string GetDefaultPictureUrl(int targetSize = 0,
            PictureType defaultPictureType = PictureType.Entity,
            string storeLocation = null)
        {
            string defaultImageFileName;
            switch (defaultPictureType)
            {
                case PictureType.Avatar:
                    defaultImageFileName = _settingService.GetSettingByKey("Media.Customer.DefaultAvatarImageName", PortalMediaDefaults.DefaultAvatarFileName);
                    break;
                case PictureType.Entity:
                default:
                    defaultImageFileName = _settingService.GetSettingByKey("Media.DefaultImageName", PortalMediaDefaults.DefaultImageFileName);
                    break;
            }

            var filePath = GetPictureLocalPath(defaultImageFileName);
            if (!_fileProvider.FileExists(filePath))
            {
                return string.Empty;
            }

            if (targetSize == 0)
            {
                var url = (!string.IsNullOrEmpty(storeLocation)
                                 ? storeLocation
                                 : _webHelper.GetStoreLocation())
                                 + "images/" + defaultImageFileName;
                return url;
            }
            else
            {
                var fileExtension = _fileProvider.GetFileExtension(filePath);
                var thumbFileName = $"{_fileProvider.GetFileNameWithoutExtension(filePath)}_{targetSize}{fileExtension}";
                var thumbFilePath = GetThumbLocalPath(thumbFileName);
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    using (var image = Image.Load(filePath, out var imageFormat))
                    {
                        image.Mutate(imageProcess => imageProcess.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = CalculateDimensions(image.Size(), targetSize)
                        }));
                        var pictureBinary = EncodeImage(image, imageFormat);
                        SaveThumb(thumbFilePath, thumbFileName, imageFormat.DefaultMimeType, pictureBinary);
                    }
                }

                var url = GetThumbUrl(thumbFileName, storeLocation);
                return url;
            }
        }

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        public virtual string GetPictureUrl(int pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity)
        {
            var picture = GetPictureById(pictureId);
            return GetPictureUrl(picture, targetSize, showDefaultPicture, storeLocation, defaultPictureType);
        }

        /// <summary>
        /// Get a picture URL
        /// </summary>
        /// <param name="picture">Picture instance</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <param name="defaultPictureType">Default picture type</param>
        /// <returns>Picture URL</returns>
        public virtual string GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string storeLocation = null,
            PictureType defaultPictureType = PictureType.Entity)
        {
            var url = string.Empty;
            byte[] pictureBinary = null;
            if (picture != null)
                pictureBinary = LoadPictureBinary(picture);
            if (picture == null || pictureBinary == null || pictureBinary.Length == 0)
            {
                if (showDefaultPicture)
                {
                    url = GetDefaultPictureUrl(targetSize, defaultPictureType, storeLocation);
                }

                return url;
            }

            if (picture.IsNew)
            {
                DeletePictureThumbs(picture);

                ////we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
                //picture = UpdatePicture(picture.Id,
                //    pictureBinary,
                //    picture.MimeType,
                //    picture.SeoFilename,
                //    picture.AltAttribute,
                //    picture.TitleAttribute,
                //    false,
                //    false);
            }

            var seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string thumbFileName;
            if (targetSize == 0)
            {
                thumbFileName = !string.IsNullOrEmpty(seoFileName)
                    ? $"{picture.Id:0000000}_{seoFileName}.{lastPart}"
                    : $"{picture.Id:0000000}.{lastPart}";
            }
            else
            {
                thumbFileName = !string.IsNullOrEmpty(seoFileName)
                    ? $"{picture.Id:0000000}_{seoFileName}_{targetSize}.{lastPart}"
                    : $"{picture.Id:0000000}_{targetSize}.{lastPart}";
            }

            var thumbFilePath = GetThumbLocalPath(thumbFileName);

            //the named mutex helps to avoid creating the same files in different threads,
            //and does not decrease performance significantly, because the code is blocked only for the specific file.
            using (var mutex = new Mutex(false, thumbFileName))
            {
                if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                {
                    mutex.WaitOne();

                    //check, if the file was created, while we were waiting for the release of the mutex.
                    if (!GeneratedThumbExists(thumbFilePath, thumbFileName))
                    {
                        byte[] pictureBinaryResized;
                        if (targetSize != 0)
                        {
                            //resizing required
                            using (var image = Image.Load(pictureBinary, out var imageFormat))
                            {
                                image.Mutate(imageProcess => imageProcess.Resize(new ResizeOptions
                                {
                                    Mode = ResizeMode.Max,
                                    Size = CalculateDimensions(image.Size(), targetSize)
                                }));

                                pictureBinaryResized = EncodeImage(image, imageFormat);
                            }
                        }
                        else
                        {
                            //create a copy of pictureBinary
                            pictureBinaryResized = pictureBinary.ToArray();
                        }

                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized);
                    }

                    mutex.ReleaseMutex();
                }
            }

            url = GetThumbUrl(thumbFileName, storeLocation);
            return url;
        }
        



        #endregion

        #region Utilities

        /// <summary>
        /// Calculates picture dimensions whilst maintaining aspect
        /// </summary>
        /// <param name="originalSize">The original picture size</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="resizeType">Resize type</param>
        /// <param name="ensureSizePositive">A value indicating whether we should ensure that size values are positive</param>
        /// <returns></returns>
        protected virtual Size CalculateDimensions(Size originalSize, int targetSize,
            ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            float width, height;

            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait
                        width = originalSize.Width * (targetSize / (float)originalSize.Height);
                        height = targetSize;
                    }
                    else
                    {
                        // landscape or square
                        width = targetSize;
                        height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    }

                    break;
                case ResizeType.Width:
                    width = targetSize;
                    height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    break;
                case ResizeType.Height:
                    width = originalSize.Width * (targetSize / (float)originalSize.Height);
                    height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (!ensureSizePositive)
                return new Size((int)Math.Round(width), (int)Math.Round(height));

            if (width < 1)
                width = 1;
            if (height < 1)
                height = 1;

            //we invoke Math.Round to ensure that no white background is rendered - https://www.nopcommerce.com/boards/t/40616/image-resizing-bug.aspx
            return new Size((int)Math.Round(width), (int)Math.Round(height));
        }

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //TODO use FileExtensionContentTypeProvider to get file extension

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }

            return lastPart;
        }

        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var fileName = $"{pictureId:0000000}_0.{lastPart}";
            var filePath = GetPictureLocalPath(fileName);

            return _fileProvider.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        protected virtual void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var fileName = $"{pictureId:0000000}_0.{lastPart}";
            _fileProvider.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }

        /// <summary>
        /// Delete a picture on file system
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureOnFileSystem(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            var lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            var fileName = $"{picture.Id:0000000}_0.{lastPart}";
            var filePath = GetPictureLocalPath(fileName);
            _fileProvider.DeleteFile(filePath);
        }

        /// <summary>
        /// Delete picture thumbs
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureThumbs(Picture picture)
        {
            var filter = $"{picture.Id:0000000}*.*";
            var currentFiles = _fileProvider.GetFiles(_fileProvider.GetAbsolutePath(PortalMediaDefaults.ImageThumbsPath), filter, false);
            foreach (var currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName);
                _fileProvider.DeleteFile(thumbFilePath);
            }
        }

        /// <summary>
        /// Get picture (thumb) local path
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(PortalMediaDefaults.ImageThumbsPath);

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > PortalMediaDefaults.MultipleThumbDirectoriesLength)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, PortalMediaDefaults.MultipleThumbDirectoriesLength);
                    thumbsDirectoryPath = _fileProvider.GetAbsolutePath(PortalMediaDefaults.ImageThumbsPath, subDirectoryName);
                    _fileProvider.CreateDirectory(thumbsDirectoryPath);
                }
            }

            var thumbFilePath = _fileProvider.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            storeLocation = !string.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : _webHelper.GetStoreLocation();
            var url = storeLocation + "images/thumbs/";

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > PortalMediaDefaults.MultipleThumbDirectoriesLength)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, PortalMediaDefaults.MultipleThumbDirectoriesLength);
                    url = url + subDirectoryName + "/";
                }
            }

            url = url + thumbFileName;
            return url;
        }

        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database)
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Local picture path</returns>
        protected virtual string GetPictureLocalPath(string fileName)
        {
            return _fileProvider.GetAbsolutePath("images", fileName);
        }

 

        /// <summary>
        /// Get a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <returns>Result</returns>
        protected virtual bool GeneratedThumbExists(string thumbFilePath, string thumbFileName)
        {
            return _fileProvider.FileExists(thumbFilePath);
        }

        /// <summary>
        /// Save a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <param name="mimeType">MIME type</param>
        /// <param name="binary">Picture binary</param>
        protected virtual void SaveThumb(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary)
        {
            //ensure \thumb directory exists
            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(PortalMediaDefaults.ImageThumbsPath);
            _fileProvider.CreateDirectory(thumbsDirectoryPath);

            //save
            _fileProvider.WriteAllBytes(thumbFilePath, binary);
        }



        /// <summary> Updates the picture binary data </summary>
        /// <param name="picture">The picture object</param>
        /// <param name="binaryData">The picture binary data</param>
        /// <returns>Picture binary</returns>
        protected virtual PictureBinary UpdatePictureBinary(Picture picture, byte[] binaryData)
        {
            //if (picture == null)
            //    throw new ArgumentNullException(nameof(picture));

            //var pictureBinary = picture.PictureBinary;

            //var isNew = pictureBinary == null;

            //if (isNew)
            //    pictureBinary = new PictureBinary
            //    {
            //        PictureId = picture.Id
            //    };

            //pictureBinary.BinaryData = binaryData;

            //if (isNew)
            //    _pictureBinaryRepository.Insert(pictureBinary);
            //else
            //    _pictureBinaryRepository.Update(pictureBinary);

            //return pictureBinary;
            return null;
        }

        Picture IPictureService.InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, string altAttribute, string titleAttribute, bool isNew, bool validateBinary)
        {
            throw new NotImplementedException();
        }

        Picture IPictureService.GetPictureById(long model)
        {
            throw new NotImplementedException();
        }

        public string GetPictureUrl(Picture picture)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}


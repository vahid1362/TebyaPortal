using Portal.core.Media;

namespace Portal.Service.Media
{
   public interface IPictureService
    {
        #region crud
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            string altAttribute = null, string titleAttribute = null,
            bool isNew = true, bool validateBinary = true);
        #endregion

        object GetPictureUrl(int model, int pictureSize, bool b);
        object GetDefaultPictureUrl(int pictureSize);
        Picture GetPictureById(long model);
    }
}

using System.Collections.Generic;
using Portal.core.Media;
using Portal.Standard.Core.Media;

namespace Portal.Service.Media
{
   public interface IPictureService
    {
        #region crud
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            string altAttribute = null, string titleAttribute = null,
            bool isNew = true, bool validateBinary = true);
        #endregion


        /// <summary>
        /// Validates input picture dimensions
        /// </summary>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary or throws an exception</returns>
        byte[] ValidatePicture(byte[] pictureBinary, string mimeType);


        Picture GetPictureById(long model);
        string GetPictureUrl(Picture picture);
    }
}

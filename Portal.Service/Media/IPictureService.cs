using Portal.core.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Service
{
   public interface IPictureService
    {
        #region crud
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename,
            string altAttribute = null, string titleAttribute = null,
            bool isNew = true, bool validateBinary = true);
        #endregion
    }
}

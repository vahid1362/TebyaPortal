using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Portal.core;
using Portal.Service.Media;

namespace QTasMarketing.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MediaController : Controller
    {

        private readonly IPictureService _pictureService;

        public MediaController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Save(IEnumerable<IFormFile> files, long? pictureId)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (!string.IsNullOrEmpty(fileExtension))
                        fileExtension = fileExtension.ToLowerInvariant();
                    var httpPostedFile = Request.Form.Files.FirstOrDefault();
                    //contentType is not always available 
                    //that's why we manually update it here
                    //http://www.sfsu.edu/training/mimetype.htm

                    var contentType = file.ContentType;
                    if (string.IsNullOrEmpty(contentType))
                    {
                        switch (fileExtension)
                        {
                            case ".bmp":
                                contentType = MimeTypes.ImageBmp;
                                break;
                            case ".gif":
                                contentType = MimeTypes.ImageGif;
                                break;
                            case ".jpeg":
                            case ".jpg":
                            case ".jpe":
                            case ".jfif":
                            case ".pjpeg":
                            case ".pjp":
                                contentType = MimeTypes.ImageJpeg;
                                break;
                            case ".png":
                                contentType = MimeTypes.ImagePng;
                                break;
                            case ".tiff":
                            case ".tif":
                                contentType = MimeTypes.ImageTiff;
                                break;
                            default:
                                break;
                        }
                    }
                    var binaryFormatFile = file.GetDownloadBits();
                    var picture = _pictureService.InsertPicture(binaryFormatFile, contentType, null);
                    pictureId = picture.Id;



                }
            }



            return Json(new
            {
                success = true,
                pictureId,
                // imageUrl = _pictureService.GetPictureUrl(picture, 100)
            }, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = { new StringEnumConverter() }
            });
        }


    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace wdn.Web.Controllers
{
    public class UploadController : System.Web.Mvc.Controller
    {
        #region 文件上传wdn
        public string UpLoadWdn(bool createThumb = true,int isImg=1)
        {
            int addhost = 1;
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["picAddHost"] != "1") addhost = 0;


            ReturnResult result = null;

            var filebox = Request.Files[0];

            string[] extendArr = null;
            extendArr = (".bmp,.jpg,.png,.tiff,.gif,.pcx,.tga,.exif,.fpx,.svg,.psd,.cdr,.pcd,.dxf,.ufo,.eps,.ai,.raw,.WMF,.xlsx,.xls").Split(',');

            if (isImg==1&&filebox.ContentLength > 1024 * 1024)
            {
                result = new ReturnResult { code = 1, msg = "图片规格不能大于1M" };
            }

            var now = DateTime.Now;
            var _f = "/UpLoad/" + now.ToString("yyyyMMdd") + "/";
            var folders = Server.MapPath("~/" + _f);
            if (!System.IO.Directory.Exists(folders)) System.IO.Directory.CreateDirectory(folders);

            var nfileName = Path.GetFileName(filebox.FileName);
            var extend = Path.GetExtension(nfileName).ToLower();

            if (!extendArr.Contains(extend))
            {
                result.code = 1;
                result.msg = "系统暂不支持" + extend + "格式";
                return Hotch.ITCradle.Common.JSONHelper.Seriallize(result);
            }

            string _name = Guid.NewGuid().ToString() + extend;

            var fullname = folders + _name;

            filebox.SaveAs(fullname);
            var host = httpHelper.GetDomainName();
            var src = (addhost == 1 ? host : string.Empty) + _f + _name;

            //生成小图
            //string folder = FileController.Sati_GetCurrentFolder();
            //string newFileName = GUIDHelper.CreateGUIDWithNoSplit();
            //string smallFileFullPath = Path.Combine(Server.MapPath("~" + folder), newFileName + "_small" + extend);
            //ThumbnailHelper.Generate(fullname, smallFileFullPath, 80, 80);

            result = new ReturnResult
            {
                code = 0,
                msg = "it摇篮",
                data = new
                {
                    src = src,
                    title = "it摇篮"
                }
            };

            return JSONHelper.Seriallize(result);
        }


        ReturnResult _UpLoadWdn(HttpPostedFileBase filebox, int addhost = 1, bool createThumb = true)
        {
            HttpFileCollectionBase a = Request.Files;

            ReturnResult result = null;
            string[] extendArr = null;
            extendArr = (".bmp,.jpg,.png,.tiff,.gif,.pcx,.tga,.exif,.fpx,.svg,.psd,.cdr,.pcd,.dxf,.ufo,.eps,.ai,.raw,.WMF").Split(',');
            if (filebox.ContentLength > 1024 * 1024)
            {
                result = new ReturnResult { code = 1, msg = "图片规格不能大于1M" };
            }

            var now = DateTime.Now;
            var _f = "/UpLoad/" + now.ToString("yyyyMMdd") + "/";
            var folders = Server.MapPath("~/" + _f);
            if (!System.IO.Directory.Exists(folders)) System.IO.Directory.CreateDirectory(folders);

            var nfileName = Path.GetFileName(filebox.FileName);
            var extend = Path.GetExtension(nfileName).ToLower();

            if (!extendArr.Contains(extend))
            {
                result.code = 1;
                result.msg = "系统暂不支持" + extend + "格式";
                return (result);
            }

            string _name = now.ToString("HHmmsss") + extend;
            var fullname = folders + _name;

            filebox.SaveAs(fullname);
            var host = httpHelper.GetDomainName();
            var src = (addhost == 1 ? host : string.Empty) + _f + _name;

            //生成小图
            string folder = FileController.Sati_GetCurrentFolder();
            string newFileName = GUIDHelper.CreateGUIDWithNoSplit();
            string smallFileFullPath = Path.Combine(Server.MapPath("~" + folder), newFileName + "_small" + extend);
            ThumbnailHelper.Generate(fullname, smallFileFullPath, 80, 80);

            result = new ReturnResult
            {
                code = 0,
                msg = src,
                data = new
                {
                    src = src,
                    title = "it摇篮"
                }
            };

            return (result);
        }

        #endregion

    }
}

using System;
using System.Data;
using System.Web;
using System.IO;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace TL.Web.UploadServices
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class Upload : System.Web.Services.WebService
    {
        private string WebServicePassword;
        private string UploadFilesPath;
        public Upload()
        {
            //获取调用WebService密码
            WebServicePassword = Config.Get("WebServicePassword");
            //获取上传文件根目录路径
            UploadFilesPath = Config.Get("UploadFilesPath");
        }

        /// <summary>
        /// 创建指定路径中的所有目录
        /// </summary>
        /// <param name="Path">路径</param>
        private void CreateDirectory(string Path)
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        /// <summary>
        /// 上传店铺照片
        /// </summary>
        /// <param name="fs">照片字节</param>
        /// <param name="FileExt">照片扩展名</param>
        /// <param name="UserId">店铺用户编号</param>
        /// <param name="CallPassword">调用服务密码</param>
        /// <returns>操作结果</returns>
        [WebMethod]
        public string UploadShopPhoto(byte[] fs, string FileExt, int UserId, string CallPassword)
        {
            if (CallPassword == WebServicePassword)
            {
                if (ImageTools.CheckExt(FileExt, new string[] { ".gif", ".jpeg", ".jpg", ".png", ".bmp" }))
                {
                    try
                    {
                        string UrlPath = Config.Get("ShopPhotoFolder") + "/" + UserId + "/" + System.DateTime.Now.ToString("yyyyMMddHHmmssffff") + "/";
                        string FullPath = UploadFilesPath + UrlPath;
                        CreateDirectory(FullPath);

                        string FileName = "originally" + FileExt;
                        string FilePath = FullPath + FileName;

                        //保存店铺照片
                        MemoryStream m = new MemoryStream(fs);//定义并实例化一个内存流，以存放提交上来的字节数组。
                        FileStream f = new FileStream(FilePath, FileMode.Create);//定义实际文件对象，保存上载的文件。
                        m.WriteTo(f);//把内内存里的数据写入物理文件
                        m.Close();
                        m.Dispose();
                        f.Close();
                        f.Dispose();

                        //为店铺照片生成缩略图
                        string[] ShopPhotoThumbnailSize = Config.Get("ShopPhotoThumbnailSize").Split(',');
                        for (int i = 0; i < ShopPhotoThumbnailSize.Length; i++)
                        {
                            string[] CurrentSize = ShopPhotoThumbnailSize[i].Split('|');
                            int Width = Convert.ToInt32(CurrentSize[0].Replace("[", ""));
                            int Height = Convert.ToInt32(CurrentSize[1]);

                            string ThumbnailFileName = Width + "x" + Height + FileExt;
                            string ThumbnailFilePath = FullPath + ThumbnailFileName;

                            if (CurrentSize[2].Replace("]", "") == "True")
                            {
                                ImageTools.WaterImageDirection Direction = ImageTools.ConvertToWaterImageDirection(Config.Get("ShopPhotoWaterImageDiretion"));
                                ImageTools.MakeThumbnailImageAndWaterImage(FilePath, ThumbnailFilePath, Width, Height, Config.Get("WaterImagePath"), Direction, Convert.ToInt32(Config.Get("ShopPhotoWaterImageOffsetX")), Convert.ToInt32(Config.Get("ShopPhotoWaterImageOffsetY")));
                            }
                            else
                            {
                                ImageTools.MakeThumbnailImage(FilePath, ThumbnailFilePath, Width, Height);
                            }
                        }
                        return UrlPath;
                    }
                    catch
                    {
                        return "UploadError";
                    }
                }
                else
                {
                    return "ExtError";
                }
            }
            else
            {
                return "PasswordError";
            }
        }

        /// <summary>
        /// 删除店铺照片
        /// </summary>
        /// <param name="PhotoUrls">照片Url</param>
        /// <param name="CallPassword">调用服务密码</param>
        /// <returns>操作结果</returns>
        [WebMethod]
        public string DelShopPhoto(string PhotoUrls, string CallPassword)
        {
            if (CallPassword == WebServicePassword)
            {
                bool flag = true;
                string[] PhotoUrlsArr = PhotoUrls.Split(',');
                for (int i = 0; i < PhotoUrlsArr.Length; i++)
                {
                    string PhotoUrl = UploadFilesPath + PhotoUrlsArr[i];
                    if (Directory.Exists(PhotoUrl))
                    {
                        try
                        {
                            Directory.Delete(PhotoUrl, true);
                        }
                        catch
                        {
                            flag = false;
                        }
                    }
                }
                return flag ? "" : "DelError";
            }
            else
            {
                return "PasswordError";
            }
        }
    }
}

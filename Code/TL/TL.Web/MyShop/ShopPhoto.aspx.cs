using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TL.Common;
using TL.Model.City.Shop;

namespace TL.Web.MyShop
{
    public partial class ShopPhoto : TL.Web.UI.ShopUserPage
    {
        public int CurrentTotalPhoto;//当前店铺照片张数
        public int TotalPhoto = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("TotalPhoto"));//系统允许上传店铺照片的张数
        public IList<PhotoInfo> ShopPhotoList;//店铺照片对象列表
        private BLL.City.Shop.Photo BllShopPhoto = new TL.BLL.City.Shop.Photo();
        public int UploadShopPhotoMinSize = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("UploadShopPhotoMinSize"));//照片最小容量
        public int UploadShopPhotoMaxSize = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("UploadShopPhotoMaxSize"));//照片最大容量

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentTotalPhoto = BllShopPhoto.GetTotalPhoto(Shop_User.UserId);

            string action = Tools.GetQueryString("action").ToLower();
            switch (action)
            {
                case "upload":
                    Upload();
                    break;
                case "del":
                    Del();
                    break;
                case "delall":
                    DelAll();
                    break;
                case "save":
                    ShopPhotoList = BllShopPhoto.GetList(Shop_User.UserId); ;
                    Save();
                    break;
                default:
                    ShopPhotoList = BllShopPhoto.GetList(Shop_User.UserId); ;
                    break;
            }
        }

        /// <summary>
        /// 检查扩展名
        /// </summary>
        /// <param name="ExtName"></param>
        /// <returns></returns>
        private bool CheckExt(string ExtName)
        {
            string[] ExtNames = TL.Config.SysConfig.GetConfigValue("UploadShopPhotoExt").Split(',');
            for (int i = 0; i < ExtNames.Length; i++)
                if (ExtName.ToLower() == ExtNames[i].ToLower())
                    return true;
            return false;
        }

        /// <summary>
        /// 上传
        /// </summary>
        private void Upload()
        {
            string Msg = "";
            int FilesCount = Request.Files.Count;
            if (CurrentTotalPhoto + FilesCount <= TotalPhoto)
            {
                if (FilesCount > 0)
                {
                    if (FilesCount <= Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("UploadShopPhotoBatchSize")))
                    {
                        bool flag = false;
                        for (int i = 0; i < FilesCount; i++)
                        {
                            HttpPostedFile CurrentFile = Request.Files[i];
                            if (CurrentFile.ContentLength == 0)
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"您还未选择第 " + (i + 1) + " 张店铺照片\\\"}";
                                flag = true;
                                break;
                            }
                            if (!CheckExt(Path.GetExtension(CurrentFile.FileName).ToLower()))
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"第 " + (i + 1) + " 张店铺照片文件格式不正确\\\"}";
                                flag = true;
                                break;
                            }
                            if (CurrentFile.ContentLength < UploadShopPhotoMinSize || CurrentFile.ContentLength > UploadShopPhotoMaxSize)
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"第 " + (i + 1) + " 张店铺照片文件大小不正确，单张店铺照片容量只能在  " + UploadShopPhotoMinSize / 1024 + "kb - " + UploadShopPhotoMaxSize / 1024 + "kb 之间\\\"}";
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)//如果符合要求则上载店铺照片
                        {
                            try
                            {
                                WSDL wsdl = new WSDL(CurrentCity.UploadServicesUrl, CurrentCity.UploadServicesNamespace);//创建一个远程WebService对象
                                for (int i = 0; i < FilesCount; i++)
                                {
                                    //WebService参数
                                    HttpPostedFile CurrentFile = Request.Files[i];
                                    byte[] PhotoByte = new byte[CurrentFile.ContentLength];
                                    System.IO.Stream FileStream = (System.IO.Stream)CurrentFile.InputStream;
                                    FileStream.Read(PhotoByte, 0, CurrentFile.ContentLength);

                                    object[] args = new object[4];
                                    args[0] = PhotoByte;
                                    args[1] = Path.GetExtension(CurrentFile.FileName).ToLower();
                                    args[2] = Shop_User.UserId;
                                    args[3] = CurrentCity.UploadServicesPassword;

                                    //调用远程WebServices中的UploadShopPhoto方法进行上载店铺照片，并返回店铺照片的Url地址
                                    string WebServiceResult = wsdl.Invoke("UploadShopPhoto", args).ToString();
                                    FileStream.Close();

                                    //在上传过程中，只有上传成功才会被写入数据库，如果是在批量上传中出现错误，则会停止后续文件的上传和写入数据库
                                    if (WebServiceResult == "PasswordError" || WebServiceResult == "ExtError" || WebServiceResult == "UploadError")
                                    {
                                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"上传过程中出错\\\"}";
                                        break;
                                    }
                                    else
                                    {
                                        //建立店铺照片对象
                                        PhotoInfo ShopPhoto = new PhotoInfo();
                                        string FileName = Path.GetFileNameWithoutExtension(CurrentFile.FileName);
                                        ShopPhoto.Description = FileName.Length > 50 ? FileName.Substring(0, 50) : FileName;
                                        ShopPhoto.Url = WebServiceResult;
                                        ShopPhoto.Ext = Path.GetExtension(CurrentFile.FileName).ToLower();
                                        ShopPhoto.UserId = Shop_User.UserId;

                                        //写入数据库
                                        if (BllShopPhoto.Add(ShopPhoto) != 0)
                                        {
                                            Msg = "{\\\"type\\\":true,\\\"msg\\\":\\\"上传成功\\\"}";
                                        }
                                        else
                                        {
                                            Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"保存出错\\\"}";
                                            break;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"上传过程中出错\\\"}";
                            }
                        }
                    }
                    else
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"最多只能同时上传 " + TL.Config.SysConfig.GetConfigValue("UploadShopPhotoBatchSize") + " 张店铺照片\\\"}";
                    }
                }
                else
                {
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"没有可上传的店铺照片\\\"}";
                }
            }
            else
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"当前您的店铺照片为 " + CurrentTotalPhoto.ToString() + " 张，总共只能上传 " + TotalPhoto.ToString() + " 张，因此还能上传 " + (TotalPhoto - CurrentTotalPhoto).ToString() + " 张。\\\"}";
            }

            Response.Write("<script type=\"text/javascript\">" +
                "\r\tparent.uploadEnd(\"" + Msg + "\")" +
                "\r</script>");
            Response.End();
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {
            if (CurrentTotalPhoto > 0)
            {
                string Id = Tools.GetQueryString("id");
                if (Id != string.Empty)
                {
                    string PhotoUrls;
                    if (BllShopPhoto.Del(Id, Shop_User.UserId, out PhotoUrls) != 0)
                    {
                        WSDL wsdl = new WSDL(CurrentCity.UploadServicesUrl, CurrentCity.UploadServicesNamespace);

                        object[] args = new object[2];
                        args[0] = PhotoUrls;
                        args[1] = CurrentCity.UploadServicesPassword;

                        string WebServiceResult = wsdl.Invoke("DelShopPhoto", args).ToString();
                        if (WebServiceResult == "PasswordError" || WebServiceResult == "DelError")
                        {
                            ShowWindow(4, "系统提示", "删除店铺照片失败", null, true);
                        }
                        else
                        {
                            ShowWindow(3, "系统提示", "删除店铺照片成功,点击 \\\"确定\\\" 换钮返回", "shopphoto.aspx", false);
                        }
                    }
                    else
                    {
                        ShowWindow(4, "系统提示", "删除店铺照片失败", null, true);
                    }
                }
                else
                {
                    ShowWindow(1, "系统提示", "请选择要删除的店铺照片", null, true);
                }
            }
            else
            {
                ShowWindow(1, "系统提示", "您还没有上传店铺照片", null, true);
            }
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        private void DelAll()
        {
            if (CurrentTotalPhoto > 0)
            {
                string PhotoUrls;
                if (BllShopPhoto.DelAll(Shop_User.UserId, out PhotoUrls) != 0)
                {
                    WSDL wsdl = new WSDL(CurrentCity.UploadServicesUrl, CurrentCity.UploadServicesNamespace);

                    object[] args = new object[2];
                    args[0] = PhotoUrls;
                    args[1] = CurrentCity.UploadServicesPassword;

                    string WebServiceResult = wsdl.Invoke("DelShopPhoto", args).ToString();
                    if (WebServiceResult == "PasswordError" || WebServiceResult == "DelError")
                    {
                        ShowWindow(4, "系统提示", "删除全部店铺照片失败", null, true);
                    }
                    else
                    {
                        ShowWindow(3, "系统提示", "删除全部店铺照片成功,点击 \\\"确定\\\" 换钮返回", "shopphoto.aspx", false);
                    }
                }
                else
                {
                    ShowWindow(4, "系统提示", "删除全部店铺照片失败", null, true);
                }
            }
            else
            {
                ShowWindow(1, "系统提示", "您还没有上传店铺照片", null, true);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            for (int i = 0; i < ShopPhotoList.Count; i++)
            {
                PhotoInfo ShopPhoto = ShopPhotoList[i];
                string Description = Tools.GetForm("Description" + ShopPhoto.Id);
                int OrderNum;
                try
                {
                    OrderNum = Convert.ToInt32(Tools.GetForm("OrderNum" + ShopPhoto.Id));
                }
                catch
                {
                    continue;
                }

                if (ShopPhoto.Description != Description || ShopPhoto.OrderNum != OrderNum)
                {
                    ShopPhoto.Description = Description.Length > 50 ? Description.Substring(0, 50) : Description;
                    ShopPhoto.OrderNum = OrderNum;
                    BllShopPhoto.Save(ShopPhoto);
                }
            }
            Response.Redirect("shopphoto.aspx");
        }
    }
}

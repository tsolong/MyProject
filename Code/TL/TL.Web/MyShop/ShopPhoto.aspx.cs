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
        public int CurrentTotalPhoto;//��ǰ������Ƭ����
        public int TotalPhoto = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("TotalPhoto"));//ϵͳ�����ϴ�������Ƭ������
        public IList<PhotoInfo> ShopPhotoList;//������Ƭ�����б�
        private BLL.City.Shop.Photo BllShopPhoto = new TL.BLL.City.Shop.Photo();
        public int UploadShopPhotoMinSize = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("UploadShopPhotoMinSize"));//��Ƭ��С����
        public int UploadShopPhotoMaxSize = Convert.ToInt32(TL.Config.SysConfig.GetConfigValue("UploadShopPhotoMaxSize"));//��Ƭ�������

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
        /// �����չ��
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
        /// �ϴ�
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
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"����δѡ��� " + (i + 1) + " �ŵ�����Ƭ\\\"}";
                                flag = true;
                                break;
                            }
                            if (!CheckExt(Path.GetExtension(CurrentFile.FileName).ToLower()))
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�� " + (i + 1) + " �ŵ�����Ƭ�ļ���ʽ����ȷ\\\"}";
                                flag = true;
                                break;
                            }
                            if (CurrentFile.ContentLength < UploadShopPhotoMinSize || CurrentFile.ContentLength > UploadShopPhotoMaxSize)
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�� " + (i + 1) + " �ŵ�����Ƭ�ļ���С����ȷ�����ŵ�����Ƭ����ֻ����  " + UploadShopPhotoMinSize / 1024 + "kb - " + UploadShopPhotoMaxSize / 1024 + "kb ֮��\\\"}";
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)//�������Ҫ�������ص�����Ƭ
                        {
                            try
                            {
                                WSDL wsdl = new WSDL(CurrentCity.UploadServicesUrl, CurrentCity.UploadServicesNamespace);//����һ��Զ��WebService����
                                for (int i = 0; i < FilesCount; i++)
                                {
                                    //WebService����
                                    HttpPostedFile CurrentFile = Request.Files[i];
                                    byte[] PhotoByte = new byte[CurrentFile.ContentLength];
                                    System.IO.Stream FileStream = (System.IO.Stream)CurrentFile.InputStream;
                                    FileStream.Read(PhotoByte, 0, CurrentFile.ContentLength);

                                    object[] args = new object[4];
                                    args[0] = PhotoByte;
                                    args[1] = Path.GetExtension(CurrentFile.FileName).ToLower();
                                    args[2] = Shop_User.UserId;
                                    args[3] = CurrentCity.UploadServicesPassword;

                                    //����Զ��WebServices�е�UploadShopPhoto�����������ص�����Ƭ�������ص�����Ƭ��Url��ַ
                                    string WebServiceResult = wsdl.Invoke("UploadShopPhoto", args).ToString();
                                    FileStream.Close();

                                    //���ϴ������У�ֻ���ϴ��ɹ��Żᱻд�����ݿ⣬������������ϴ��г��ִ������ֹͣ�����ļ����ϴ���д�����ݿ�
                                    if (WebServiceResult == "PasswordError" || WebServiceResult == "ExtError" || WebServiceResult == "UploadError")
                                    {
                                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ϴ������г���\\\"}";
                                        break;
                                    }
                                    else
                                    {
                                        //����������Ƭ����
                                        PhotoInfo ShopPhoto = new PhotoInfo();
                                        string FileName = Path.GetFileNameWithoutExtension(CurrentFile.FileName);
                                        ShopPhoto.Description = FileName.Length > 50 ? FileName.Substring(0, 50) : FileName;
                                        ShopPhoto.Url = WebServiceResult;
                                        ShopPhoto.Ext = Path.GetExtension(CurrentFile.FileName).ToLower();
                                        ShopPhoto.UserId = Shop_User.UserId;

                                        //д�����ݿ�
                                        if (BllShopPhoto.Add(ShopPhoto) != 0)
                                        {
                                            Msg = "{\\\"type\\\":true,\\\"msg\\\":\\\"�ϴ��ɹ�\\\"}";
                                        }
                                        else
                                        {
                                            Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�������\\\"}";
                                            break;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"�ϴ������г���\\\"}";
                            }
                        }
                    }
                    else
                    {
                        Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"���ֻ��ͬʱ�ϴ� " + TL.Config.SysConfig.GetConfigValue("UploadShopPhotoBatchSize") + " �ŵ�����Ƭ\\\"}";
                    }
                }
                else
                {
                    Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"û�п��ϴ��ĵ�����Ƭ\\\"}";
                }
            }
            else
            {
                Msg = "{\\\"type\\\":false,\\\"msg\\\":\\\"��ǰ���ĵ�����ƬΪ " + CurrentTotalPhoto.ToString() + " �ţ��ܹ�ֻ���ϴ� " + TotalPhoto.ToString() + " �ţ���˻����ϴ� " + (TotalPhoto - CurrentTotalPhoto).ToString() + " �š�\\\"}";
            }

            Response.Write("<script type=\"text/javascript\">" +
                "\r\tparent.uploadEnd(\"" + Msg + "\")" +
                "\r</script>");
            Response.End();
        }

        /// <summary>
        /// ɾ��
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
                            ShowWindow(4, "ϵͳ��ʾ", "ɾ��������Ƭʧ��", null, true);
                        }
                        else
                        {
                            ShowWindow(3, "ϵͳ��ʾ", "ɾ��������Ƭ�ɹ�,��� \\\"ȷ��\\\" ��ť����", "shopphoto.aspx", false);
                        }
                    }
                    else
                    {
                        ShowWindow(4, "ϵͳ��ʾ", "ɾ��������Ƭʧ��", null, true);
                    }
                }
                else
                {
                    ShowWindow(1, "ϵͳ��ʾ", "��ѡ��Ҫɾ���ĵ�����Ƭ", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "����û���ϴ�������Ƭ", null, true);
            }
        }

        /// <summary>
        /// ɾ��ȫ��
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
                        ShowWindow(4, "ϵͳ��ʾ", "ɾ��ȫ��������Ƭʧ��", null, true);
                    }
                    else
                    {
                        ShowWindow(3, "ϵͳ��ʾ", "ɾ��ȫ��������Ƭ�ɹ�,��� \\\"ȷ��\\\" ��ť����", "shopphoto.aspx", false);
                    }
                }
                else
                {
                    ShowWindow(4, "ϵͳ��ʾ", "ɾ��ȫ��������Ƭʧ��", null, true);
                }
            }
            else
            {
                ShowWindow(1, "ϵͳ��ʾ", "����û���ϴ�������Ƭ", null, true);
            }
        }

        /// <summary>
        /// ����
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

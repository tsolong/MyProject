using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;


    /// <summary>
    /// UploadFile 的摘要说明
    /// </summary>
public class UploadFile
{
    //网站的根目录
    private static readonly string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
    //上传的文件路径
    private static readonly string filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"].ToString();
    //上传的图片路径
    private static readonly string imagePath = System.Configuration.ConfigurationManager.AppSettings["imagePath"].ToString();
    //水印图片的路径
    private static readonly string waterImagePath = System.Configuration.ConfigurationManager.AppSettings["waterImagePath"].ToString();

    //这里需要注意的是保存文件的路径有两个,一个是文件,一个是图片!
    #region "上传文件类型的方法"
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="upfile">使用的上传文件的对象</param>
    /// <param name="fileType">上传文件的类型</param>
    /// <param name="fileSize">上传文件的大小,是以(M)为单位</param>
    /// <returns>返回上传的文件路径</returns>
    public static string UploadFiles(FileUpload upfile,string[] fileType,int fileSize)
    {
        return UpFile(upfile,fileType,fileSize*1024000,filePath,serverPath);
    }
    #endregion

    #region "上传原图,不加水印和缩略"
    /// <summary>
    /// 上传原图,不加水印和缩略
    /// </summary>
    /// <param name="upFile">使用的上传图片的对象</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadImages(FileUpload upFile,int fileSize)
    {
        return UploadImage(upFile,fileSize*1024000,imagePath,serverPath);
    }
    #endregion

    #region "上传经过加水印的图片"
    /// <summary>
    /// 上传经过加水印的图片
    /// </summary>
    /// <param name="upFile">上传图片使用的对象</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadAddWaterImages(FileUpload upFile, int fileSize)
    {
        return UploadAddWaterImage(upFile,fileSize*1024000,imagePath,serverPath);
    }
    #endregion

    #region "上传缩略图图片"
    /// <summary>
    /// 上传缩略图图片
    /// </summary>
    /// <param name="upFile">上传图片使用的对象</param>
    /// <param name="picWidth">缩略图需要的宽度</param>
    /// <param name="picHeight">缩略图需要的高度</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadAddSmallImages(FileUpload upFile,int picWidth,int picHeight, int fileSize)
    {
        return UploadAddSmallImage(upFile,picWidth,picHeight,fileSize*1024000,imagePath,serverPath);
    }
    #endregion

    #region "保存原图,同时生成缩略图"
    /// <summary>
    /// 保存原图,同时生成缩略图
    /// </summary>
    /// <param name="upFile">上传图片的对象</param>
    /// <param name="picWidth">缩略图需要的宽度</param>
    /// <param name="picHeigth">缩略图需要的高度</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadAddSmallImageAndImages(FileUpload upFile,int picWidth,int picHeigth,int fileSize)
    {
        return UploadAddSmallImageAndImage(upFile,picWidth,picHeigth,fileSize*1024000,imagePath,serverPath);
    }
    #endregion

    #region "对原图进行加水印但是不对缩略图进行加水印"
    /// <summary>
    /// 对原图进行加水印但是不对缩略图进行加水印
    /// </summary>
    /// <param name="upFile">上传文件的对象</param>
    /// <param name="picWidth">上传图片的宽度</param>
    /// <param name="picHeigth">上传图片的高度</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadAddWaterImageAndSmallImages(FileUpload upFile,int picWidth,int picHeigth,int fileSize)
    {
        return UpAddWaterImageAndSmallImage(upFile, picWidth, picHeigth, fileSize*1024000,imagePath, serverPath);
    }
    #endregion

    #region "上传加水印图片,同时生成缩略图"
    /// <summary>
    /// 加缩略图,同时加水印
    /// </summary>
    /// <param name="upFile">上传图片使用的对象</param>
    /// <param name="picWidth">缩略图需要的宽度</param>
    /// <param name="picHeight">缩略图需要的高度</param>
    /// <param name="fileSize">上传图片的大小</param>
    /// <returns>返回上传图片的路径</returns>
    public static string UploadAddWaterAndAddSmallImages(FileUpload upFile, int picWidth, int picHeight, int fileSize)
    {
        return UpPhoto(upFile, picWidth, picHeight, fileSize * 1024000,imagePath, serverPath);
    }
    #endregion

    #region "添加文字水印"
    public static string UploadAddTextWaters(FileUpload upFile,int FileSize)
    {
        return AddTextWater(upFile,FileSize*1024000,imagePath,serverPath);
    }
    #endregion

    #region "添加文字水印同时加缩略图"
    public static string UploadAddTextWaterImageAndSmallImages(FileUpload upFile,int picWidth,int picHeight,int fileSize)
    {
        return UpAddTextWaterImageAndSmallImage(upFile, picWidth, picHeight, fileSize*1024000,imagePath, serverPath);
    }
    #endregion





    #region "Base Operation"

    #region "上传文件"
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="fileType"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePath"></param>
    /// <param name="ServerPath"></param>
    /// <returns></returns>
    private static string UpFile(FileUpload upFile, string[] fileType, int fileSize, string filePath, string ServerPath)
    {
        //获取文件名称
        string fullName = upFile.PostedFile.FileName;
        int j = fullName.LastIndexOf("\\") + 1;
        //得到原来文件名称
        string fileName = fullName.Substring(j);

        int ni = fileName.LastIndexOf(".") + 1;
        string _fileType = (fileName.Substring(ni)).ToUpper();

        //重新生成一个文件名称,防治文件同名
        string name = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + _fileType.ToLower();
        string _filePath = "";
        for (int i = 0; i < fileType.Length; i++)
        {
            if (_fileType == fileType[i].ToUpper())
            {
                int _fileSize = upFile.PostedFile.ContentLength;
                if (_fileSize > fileSize)
                {
                    break;
                }
                else
                {
                    upFile.PostedFile.SaveAs(ServerPath + "/" + filePath + name);
                    _filePath = filePath + fileName;
                }
            }
        }
        return _filePath;
    }
    #endregion

    #region "上传原图,不加水印和缩略"
    /// <summary>
    /// 上传不加水印和缩略图的图片
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePath"></param>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    private static string UploadImage(FileUpload upFile, int fileSize, string filePath, string ServerPath)
    {
        string fullName = upFile.PostedFile.FileName;
        int j = fullName.LastIndexOf("\\") + 1;
        string fileName = fullName.Substring(j);

        int ni = fileName.LastIndexOf(".") + 1;
        string typeName = (fileName.Substring(ni)).ToUpper();
        string name = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + typeName.ToLower();
        string _filePath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            int _fileSize = upFile.PostedFile.ContentLength;
            if (_fileSize < fileSize)
            {
                upFile.PostedFile.SaveAs(ServerPath + "/" + filePath +name);
                _filePath = filePath + fileName;
            }
        }
        return _filePath;
    }
    #endregion

    #region "上传经过加水印的图片"
    /// <summary>
    /// 上传加水印图片
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePaht"></param>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    private static string UploadAddWaterImage(FileUpload upFile, int fileSize, string filePath, string serverPath)
    {
        string strFullName = upFile.PostedFile.FileName;
        string strWaterFullName = serverPath + waterImagePath;
        int size = upFile.PostedFile.ContentLength;
        int j = strFullName.LastIndexOf("\\") + 1;
        string fileName = strFullName.Substring(j);
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        //让系统的时间作为图片的名称
        string name = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
        //保存的文件路径和名称
        string picPath = serverPath + "/" + filePath + name + ".jpg";
        string newPath = "";
        string picRelativePath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (fileSize > size)
            {
                //先把文件提交到服务器
                upFile.SaveAs(picPath);

                //加文字水印，注意，这里的代码和以下加图片水印的代码不能共存
                //System.Drawing.Image image = System.Drawing.Image.FromFile(path);
                //Graphics g = Graphics.FromImage(image);
                //g.DrawImage(image, 0, 0, image.Width, image.Height);
                //Font f = new Font("Verdana", 32);
                //Brush b = new SolidBrush(Color.White);
                //string addText = AddText.Value.Trim();
                //g.DrawString(addText, f, b, 10, 10);
                //g.Dispose();

                //首先给图片添加水印

                System.Drawing.Image.GetThumbnailImageAbort myCallback =
                new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

                //创建一个图像对象
                //Bitmap bmp = new Bitmap(picPath);

                System.Drawing.Image image = System.Drawing.Image.FromFile(picPath);

                //水印图片
                System.Drawing.Image waterImage = System.Drawing.Image.FromFile(strWaterFullName);

                //Create   a   new   FrameDimension   object   from   this   image 

                FrameDimension ImgFrmDim = new FrameDimension(image.FrameDimensionsList[0]);

                int nFrameCount = image.GetFrameCount(ImgFrmDim);

                //   Save   every   frame   into   jpeg   format   

                for (int i = 0; i < nFrameCount; i++)
                {
                    image.SelectActiveFrame(ImgFrmDim, i);

                    image.Save(string.Format(serverPath + "/" + filePath + "Frame{0}.jpg ", i), ImageFormat.Jpeg);
                }
                image.Dispose();

                for (int i = 0; i < nFrameCount; i++)
                {
                    string pa = serverPath + "/" + filePath + "Frame" + i + ".jpg ";
                    System.Drawing.Image Image = System.Drawing.Image.FromFile(pa);
                    Graphics g = Graphics.FromImage(Image);
                    g.DrawImage(waterImage, new Rectangle(Image.Width - waterImage.Width, Image.Height - waterImage.Height, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, GraphicsUnit.Pixel);
                    g.Dispose();

                    newPath = serverPath + "/" + filePath + name + i.ToString() + "." + typeName.ToLower();
                    Image.Save(newPath);
                    picRelativePath = filePath + name + i.ToString() + "." + typeName.ToLower();
                    Image.Dispose();

                    if (File.Exists(pa))
                    {
                        File.Delete(pa);
                    }
                }


                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }
            }
        }
        return picRelativePath;
    }
    #endregion

    #region "上传经过缩略的图片"
    /// <summary>
    /// 上传缩略图图片
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="_picWidth"></param>
    /// <param name="_picHeight"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePath"></param>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    private static string UploadAddSmallImage(FileUpload upFile, int _picWidth, int _picHeight, int fileSize, string filePath, string serverPath)
    {
        int size = upFile.PostedFile.ContentLength;
        string fileName = upFile.PostedFile.FileName;
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        string name = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + typeName.ToLower();
        string picPath = "";
        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (size > fileSize)
            {
                return picPath;
            }
            else
            {
                double picWidth = Convert.ToDouble(_picWidth);
                double picHeight = Convert.ToDouble(_picHeight);

                System.Drawing.Image im = System.Drawing.Image.FromStream(upFile.PostedFile.InputStream);
                double height = Convert.ToDouble(im.Height);
                double width = Convert.ToDouble(im.Width);
                int suoluetuHeight = 0;
                int suoluetuWidth = 0;
                double beishuHeight = 1;
                double beishuWidth = 1;
                if (height > picHeight)
                {
                    beishuHeight = height / picHeight;
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height);
                }

                if (width > picWidth)
                {
                    beishuWidth = width / picWidth;
                }
                else
                {
                    suoluetuWidth = Convert.ToInt16(width);
                }

                if (height > width)
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuHeight);
                    suoluetuWidth = Convert.ToInt16(width / beishuHeight);
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuWidth);
                    suoluetuWidth = Convert.ToInt16(width / beishuWidth);
                }
                System.Drawing.Image.GetThumbnailImageAbort myCallback =
            new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(upFile.PostedFile.InputStream);
                //图片制作缩略图
                System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(suoluetuWidth, suoluetuHeight, myCallback, IntPtr.Zero);
                //将图像保存到页面输出流中,并制定输出图像的格式
                string path = serverPath + "/" + filePath + name;
                picPath = filePath + name;
                myThumbnail.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

                myBitmap.Dispose();

                return picPath;
            }
        }
        else
        {
            return picPath;
        }
    }
    #endregion

    #region "保存原图和对原图进行缩略的图片"
    /// <summary>
    /// 上传大的图片，保存进行缩略加工的图片
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="_picWidth"></param>
    /// <param name="_picHeight"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePath"></param>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    private static string UploadAddSmallImageAndImage(FileUpload upFile, int _picWidth, int _picHeight, int fileSize, string filePath, string serverPath)
    {
        int size = upFile.PostedFile.ContentLength;
        string fileName = upFile.PostedFile.FileName;
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        string name = DateTime.Now.ToString("yyyyMMddhhmmss") + "." + typeName.ToLower();
        string path = serverPath + "/" + filePath + name;
        string picPath = "";
        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (size > fileSize)
            {
                return picPath;
            }
            else
            {
                upFile.PostedFile.SaveAs(path);
                double picWidth = Convert.ToDouble(_picWidth);
                double picHeight = Convert.ToDouble(_picHeight);

                System.Drawing.Image im = System.Drawing.Image.FromStream(upFile.PostedFile.InputStream);
                double height = Convert.ToDouble(im.Height);
                double width = Convert.ToDouble(im.Width);
                int suoluetuHeight = 0;
                int suoluetuWidth = 0;
                double beishuHeight = 1;
                double beishuWidth = 1;
                if (height > picHeight)
                {
                    beishuHeight = height / picHeight;
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height);
                }

                if (width > picWidth)
                {
                    beishuWidth = width / picWidth;
                }
                else
                {
                    suoluetuWidth = Convert.ToInt16(width);
                }

                if (height > width)
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuHeight);
                    suoluetuWidth = Convert.ToInt16(width / beishuHeight);
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuWidth);
                    suoluetuWidth = Convert.ToInt16(width / beishuWidth);
                }
                System.Drawing.Image.GetThumbnailImageAbort myCallback =new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(path);
                //图片制作缩略图
                System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(suoluetuWidth, suoluetuHeight, myCallback, IntPtr.Zero);
                //将图像保存到页面输出流中,并制定输出图像的格式
                string smallPath = serverPath + "/" + filePath + "smallphoto/" + name;
                picPath = filePath + name;
                myThumbnail.Save(smallPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                myBitmap.Dispose();

                return picPath;
            }
        }
        else
        {
            return picPath;
        }
    }
    #endregion

    #region "对原图进行加水印但是不对缩略图进行加水印"
    private static string UpAddWaterImageAndSmallImage(FileUpload upFile, int _picWidth, int _picHeight, int fileSize, string filePath, string serverPath)
    {
        string strFullName = upFile.PostedFile.FileName;
        string strWaterFullName = serverPath + waterImagePath;
        int size = upFile.PostedFile.ContentLength;
        int j = strFullName.LastIndexOf("\\") + 1;
        string fileName = strFullName.Substring(j);
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        //让系统的时间作为图片的名称
        string name = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
        //保存的文件路径和名称
        string picPath = serverPath + "/" + filePath + name + ".jpg";
        string newPath = "";
        string smallPicPath = "";
        string picRelativePath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (fileSize > size)
            {
                //先把文件提交到服务器
                upFile.SaveAs(picPath);
                smallPicPath = serverPath + "/" + filePath + "smallphoto/" + name + ".jpg";

                //对图片进行缩略
                double picWidth = Convert.ToDouble(_picWidth);
                double picHeight = Convert.ToDouble(_picHeight);

                System.Drawing.Image im = System.Drawing.Image.FromStream(upFile.PostedFile.InputStream);
                double height = Convert.ToDouble(im.Height);
                double width = Convert.ToDouble(im.Width);
                int suoluetuHeight = 0;
                int suoluetuWidth = 0;
                double beishuHeight = 1;
                double beishuWidth = 1;
                if (height > picHeight)
                {
                    beishuHeight = height / picHeight;
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height);
                }

                if (width > picWidth)
                {
                    beishuWidth = width / picWidth;
                }
                else
                {
                    suoluetuWidth = Convert.ToInt16(width);
                }

                if (height > width)
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuHeight);
                    suoluetuWidth = Convert.ToInt16(width / beishuHeight);
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuWidth);
                    suoluetuWidth = Convert.ToInt16(width / beishuWidth);
                }
                System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(picPath);

                System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(suoluetuWidth, suoluetuHeight, myCallback, IntPtr.Zero);
                //将图像保存到页面输出流中,并制定输出图像的格式

                myThumbnail.Save(smallPicPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                myBitmap.Dispose();



            

                //给大图图片添加水印------------------------------------------------------------------------------

                //创建一个图像对象
                System.Drawing.Image image = System.Drawing.Image.FromFile(picPath);
                //水印图片
                System.Drawing.Image waterImage = System.Drawing.Image.FromFile(strWaterFullName);

                //Create   a   new   FrameDimension   object   from   this   image 

                FrameDimension ImgFrmDim = new FrameDimension(image.FrameDimensionsList[0]);

                int nFrameCount = image.GetFrameCount(ImgFrmDim);

                //   Save   every   frame   into   jpeg   format   

                for (int i = 0; i < nFrameCount; i++)
                {
                    image.SelectActiveFrame(ImgFrmDim, i);

                    image.Save(string.Format(serverPath + "/" + filePath + "Frame{0}.jpg ", i), ImageFormat.Jpeg);
                }
                image.Dispose();

                for (int i = 0; i < nFrameCount; i++)
                {
                    string pa = serverPath + "/" + filePath + "Frame" + i + ".jpg ";
                    System.Drawing.Image Image = System.Drawing.Image.FromFile(pa);
                    Graphics g = Graphics.FromImage(Image);
                    g.DrawImage(waterImage, new Rectangle(Image.Width - waterImage.Width, Image.Height - waterImage.Height, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, GraphicsUnit.Pixel);

                    g.Dispose();

                    newPath = serverPath + "/" + filePath + name + i.ToString() + "." + "jpg ";
                    picRelativePath = filePath + name + i.ToString() + "." + "jpg ";
                    Image.Save(newPath);
                    Image.Dispose();

                    if (File.Exists(pa))
                    {
                        File.Delete(pa);
                    }
                }
                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }
            }
        }
        return picRelativePath;
    }

    #endregion

    #region "对原图进行加水印的图片同时加缩略的图片"
    /// <summary>
    /// 加缩略图同时加水印
    /// </summary>
    /// <param name="upFile"></param>
    /// <param name="_picWidth"></param>
    /// <param name="_picHeight"></param>
    /// <param name="fileSize"></param>
    /// <param name="filePath"></param>
    /// <param name="serverPath"></param>
    /// <returns></returns>
    private static string UpPhoto(FileUpload upFile, int _picWidth, int _picHeight, int fileSize, string filePath, string serverPath)
    {
        string strFullName = upFile.PostedFile.FileName;
        string strWaterFullName = serverPath + waterImagePath;
        int size = upFile.PostedFile.ContentLength;
        int j = strFullName.LastIndexOf("\\") + 1;
        string fileName = strFullName.Substring(j);
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        //让系统的时间作为图片的名称
        string name = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
        //保存的文件路径和名称
        string picPath = serverPath + "/" + filePath + name + ".jpg";
        string newPath = "";
        string smallPicPath = "";
        string picRelativePath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (fileSize > size)
            {
                //先把文件提交到服务器
                upFile.SaveAs(picPath);
                smallPicPath = serverPath + "/" + filePath + "smallphoto/" + name + ".jpg";

                //对图片进行缩略
                double picWidth = Convert.ToDouble(_picWidth);
                double picHeight = Convert.ToDouble(_picHeight);

                System.Drawing.Image im = System.Drawing.Image.FromStream(upFile.PostedFile.InputStream);
                double height = Convert.ToDouble(im.Height);
                double width = Convert.ToDouble(im.Width);
                int suoluetuHeight = 0;
                int suoluetuWidth = 0;
                double beishuHeight = 1;
                double beishuWidth = 1;
                if (height > picHeight)
                {
                    beishuHeight = height / picHeight;
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height);
                }

                if (width > picWidth)
                {
                    beishuWidth = width / picWidth;
                }
                else
                {
                    suoluetuWidth = Convert.ToInt16(width);
                }

                if (height > width)
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuHeight);
                    suoluetuWidth = Convert.ToInt16(width / beishuHeight);
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuWidth);
                    suoluetuWidth = Convert.ToInt16(width / beishuWidth);
                }
                System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(picPath);

                System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(suoluetuWidth, suoluetuHeight, myCallback, IntPtr.Zero);
                //将图像保存到页面输出流中,并制定输出图像的格式

                myThumbnail.Save(smallPicPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                myBitmap.Dispose();

                //首先给大图图片添加水印------------------------------------------------------------------------------

                //创建一个图像对象
                System.Drawing.Image image = System.Drawing.Image.FromFile(picPath);
                //水印图片
                System.Drawing.Image waterImage = System.Drawing.Image.FromFile(strWaterFullName);

                //Create   a   new   FrameDimension   object   from   this   image 

                FrameDimension ImgFrmDim = new FrameDimension(image.FrameDimensionsList[0]);

                int nFrameCount = image.GetFrameCount(ImgFrmDim);

                //   Save   every   frame   into   jpeg   format   

                for (int i = 0; i < nFrameCount; i++)
                {
                    image.SelectActiveFrame(ImgFrmDim, i);

                    image.Save(string.Format(serverPath + "/" + filePath + "Frame{0}.jpg ", i), ImageFormat.Jpeg);
                }
                image.Dispose();

                for (int i = 0; i < nFrameCount; i++)
                {
                    string pa = serverPath + "/" + filePath + "Frame" + i + ".jpg ";
                    System.Drawing.Image Image = System.Drawing.Image.FromFile(pa);
                    Graphics g = Graphics.FromImage(Image);
                    g.DrawImage(waterImage, new Rectangle(Image.Width - waterImage.Width, Image.Height - waterImage.Height, waterImage.Width, waterImage.Height), 0, 0, waterImage.Width, waterImage.Height, GraphicsUnit.Pixel);

                    g.Dispose();

                    newPath = serverPath + "/" + filePath + name + i.ToString() + "." + "jpg ";
                    picRelativePath = filePath + name + i.ToString() + "." + "jpg ";
                    Image.Save(newPath);
                    Image.Dispose();

                    if (File.Exists(pa))
                    {
                        File.Delete(pa);
                    }
                }
                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }

                //然后给小图加水印----------------------------------------------------------------------------------
                strWaterFullName = serverPath + waterImagePath;
                System.Drawing.Image smallimage = System.Drawing.Image.FromFile(smallPicPath);

                //水印图片
                System.Drawing.Image smallwaterImage = System.Drawing.Image.FromFile(strWaterFullName);

                //Create   a   new   FrameDimension   object   from   this   image 

                FrameDimension ImgFrmDimSmall = new FrameDimension(smallimage.FrameDimensionsList[0]);

                nFrameCount = smallimage.GetFrameCount(ImgFrmDimSmall);

                //   Save   every   frame   into   jpeg   format   

                for (int i = 0; i < nFrameCount; i++)
                {
                    smallimage.SelectActiveFrame(ImgFrmDimSmall, i);

                    smallimage.Save(string.Format(serverPath + "/" + filePath + "smallphoto/" + "Frame{0}.jpg ", i), ImageFormat.Jpeg);
                }
                smallimage.Dispose();

                for (int i = 0; i < nFrameCount; i++)
                {
                    string pa = serverPath + "/" + filePath + "smallphoto/" + "Frame" + i + ".jpg ";
                    System.Drawing.Image Image = System.Drawing.Image.FromFile(pa);
                    Graphics g = Graphics.FromImage(Image);
                    g.DrawImage(smallwaterImage, new Rectangle(Image.Width - smallwaterImage.Width, Image.Height - smallwaterImage.Height, smallwaterImage.Width, smallwaterImage.Height), 0, 0, smallwaterImage.Width, smallwaterImage.Height, GraphicsUnit.Pixel);

                    g.Dispose();

                    newPath = serverPath + "/" + filePath + "smallphoto/" + name + i.ToString() + "." + "jpg ";
                    Image.Save(newPath);
                    Image.Dispose();

                    if (File.Exists(pa))
                    {
                        File.Delete(pa);
                    }
                }

                if (File.Exists(smallPicPath))
                {
                    File.Delete(smallPicPath);
                }
            }
        }
        return picRelativePath;
    }

    #endregion

    #region "添加文字水印"
    private static string AddTextWater(FileUpload upFile, int fileSize, string filePath, string serverPath)
    {
        string strFullName = upFile.PostedFile.FileName;
        string strWaterFullName = serverPath + waterImagePath;
        int size = upFile.PostedFile.ContentLength;
        int j = strFullName.LastIndexOf("\\") + 1;
        string fileName = strFullName.Substring(j);
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        //让系统的时间作为图片的名称
        string name = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
        //保存的文件路径和名称
        string picPath = serverPath + "/" + filePath + name + ".jpg";
        string newPath = "";
        string picRelativePath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (fileSize > size)
            {
                //先把文件提交到服务器
                upFile.SaveAs(picPath);

                //加文字水印
                System.Drawing.Image image = System.Drawing.Image.FromFile(picPath);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(image, 20, 20,image.Width,image.Height);
                //在这里设置字体的样式
                Font f = new Font("宋体",60,FontStyle.Italic);
                Brush b = new SolidBrush(Color.Black);
                string addText = "Tsolong";
                //在这里设置字体的位置
                g.DrawString(addText, f, b,image.Width-300,image.Height-100);               
                g.Dispose();
                //保存加水印的图片然后删除原来的图片
                newPath = serverPath + "/" + filePath + name + "3.jpg";
                image.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                image.Dispose();
                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }

                picRelativePath =filePath + name + "3.jpg";
            }
        }
        return picRelativePath;
    }
    #endregion

    #region "添加文字水印同时加缩略图"
    private static string UpAddTextWaterImageAndSmallImage(FileUpload upFile,int _picWidth,int _picHeight, int fileSize, string filePath, string serverPath)
    {
        string strFullName = upFile.PostedFile.FileName;
        string strWaterFullName = serverPath + waterImagePath;
        int size = upFile.PostedFile.ContentLength;
        int j = strFullName.LastIndexOf("\\") + 1;
        string fileName = strFullName.Substring(j);
        int a = fileName.LastIndexOf(".") + 1;
        string typeName = fileName.Substring(a).ToUpper();
        //让系统的时间作为图片的名称
        string name = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Day.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString();
        //保存的文件路径和名称
        string picPath = serverPath + "/" + filePath + name + ".jpg";
        string newPath = serverPath + "/" + filePath + name + "new.jpg";
        string picRelativePath = "";
        string smallPicPath = "";

        if (typeName == "GIF" || typeName == "JPG" || typeName == "PNG" || typeName == "BMP")
        {
            if (fileSize > size)
            {
                //先把文件提交到服务器
                upFile.SaveAs(picPath);
                smallPicPath = serverPath + "/" + filePath + "smallphoto/" + name + "new.jpg";

                //对图片进行缩略
                double picWidth = Convert.ToDouble(_picWidth);
                double picHeight = Convert.ToDouble(_picHeight);

                System.Drawing.Image im = System.Drawing.Image.FromStream(upFile.PostedFile.InputStream);
                double height = Convert.ToDouble(im.Height);
                double width = Convert.ToDouble(im.Width);
                int suoluetuHeight = 0;
                int suoluetuWidth = 0;
                double beishuHeight = 1;
                double beishuWidth = 1;
                if (height > picHeight)
                {
                    beishuHeight = height / picHeight;
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height);
                }

                if (width > picWidth)
                {
                    beishuWidth = width / picWidth;
                }
                else
                {
                    suoluetuWidth = Convert.ToInt16(width);
                }

                if (height > width)
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuHeight);
                    suoluetuWidth = Convert.ToInt16(width / beishuHeight);
                }
                else
                {
                    suoluetuHeight = Convert.ToInt16(height / beishuWidth);
                    suoluetuWidth = Convert.ToInt16(width / beishuWidth);
                }
                System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(picPath);

                System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(suoluetuWidth, suoluetuHeight, myCallback, IntPtr.Zero);
                //将图像保存到页面输出流中,并制定输出图像的格式

                myThumbnail.Save(smallPicPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                myBitmap.Dispose();



                //加文字水印
                System.Drawing.Image image = System.Drawing.Image.FromFile(picPath);
                Graphics g = Graphics.FromImage(image);
                g.DrawImage(image, 20, 20, image.Width, image.Height);
                //在这里设置字体的样式
                Font f = new Font("宋体", 60, FontStyle.Italic);
                Brush b = new SolidBrush(Color.Black);
                string addText = "Tsolong";
                //在这里设置字体的位置
                g.DrawString(addText, f, b, image.Width - 300, image.Height - 100);
                g.Dispose();
                //保存加水印的图片然后删除原来的图片
                image.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                image.Dispose();
                if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }

                picRelativePath = filePath + name + "new.jpg";
            }
        }
        return picRelativePath;
    }
    #endregion 

    //必须创建此委托,在GDI+ 1.0版本中已不调用
    public static bool ThumbnailCallback()
    {
        return false;
    }
    #endregion
}

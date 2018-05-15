using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TL.Web.SystemManage
{
    /*public enum LoginStatef:int
    {
        /// <summary>
        /// 用户不存在
        /// </summary>
        Err_NotUser,
        /// <summary>
        /// 密码错误
        /// </summary>
        Err_UserPassword,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        Err_UserNameOrUserPassword,
        /// <summary>
        /// 用户被锁定
        /// </summary>
        Err_Locked,
        /// <summary>
        /// 登录成功
        /// </summary>
        Succeed
    }

    public class tso
    {
        private bool _aa;
        public bool aa
        {
            get { return _aa; }
            set { _aa = value; }
        }

    }*/

    /*public class aa 
    {
        public int bb;
    }

    public class ff
    {
        public void change(int i)
        {
            i = 1000;
        }

        public void change1(aa obj)
        {
            //obj.bb = 1000;
            obj = newobj();
            obj.bb = 1000;
        }
        public aa newobj()
        {
            return new aa();
        }
    }*/

    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (string.Empty == "")
            {
                Response.Write(bool.TrueString);
            }
            if (string.Empty == null)
            {
                Response.Write(bool.TrueString);
            }
            else
            {
                Response.Write(bool.FalseString);
            }




            aa a1 = new aa();
            a1.bb = 10;
            Response.Write(a1.bb.ToString() + "<br>");

            aa b1 = new aa();
            b1.bb = 100;
            Response.Write(b1.bb.ToString() + "<br>");

            ff f1 = new ff();
            f1.change(a1.bb);
            Response.Write(a1.bb.ToString() + "<br>");
            f1.change1(a1);
            Response.Write(a1.bb.ToString() + "<br>");


            aa fff = a1;
            fff.bb = 2000;
            Response.Write(a1.bb.ToString() + "<br>");

            b1.bb = 70000;

            Response.Write(a1.bb.ToString() + "<br>");
            Response.Write(b1.bb.ToString() + "<br>");

            if ("s" == "S")
            {
                Response.Write("True");
            }
            else
            {
                Response.Write(bool.FalseString);
            }





            //if()
            //Response.Write(Sys.SiteName);
            string str="";
            DataSet ds=TL.DALProfile.XmlHelper.GetDataSet(HttpContext.Current.Server.MapPath("/Config/System.config"),TL.DALProfile.XmlHelper.XmlType.File);
            Response.Write(ds.Tables.Count.ToString());

            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                    {
                        str += ds.Tables[i].Rows[j][k].ToString() + ",";
                    }
                    str += "<br>";
                }
                str += "<br>------------------<br>";
            }

            Response.Write(str);*/



            /*int fen = new int();
            Response.Write(fen);
            fen = 10;
            Response.Write(fen);

            bool aa=new bool();
            Response.Write(aa);

            Response.Write((LoginStatef.Succeed).ToString());

            //int val = null;
            Nullable<int> val=null;
            Response.Write(val.ToString());
            if (val == null)
            {
                Response.Write("是的");
            }


            string str = null;
            Response.Write(str);
            if (str == null)
            {
                Response.Write("是的");
            }

            if (str == string.Empty)
                Response.Write("tsolong");
            else
                Response.Write("notsolong");


            str = "123";
            Response.Write(str);

            tso ts = new tso();
            Response.Write(ts.aa.ToString());

            if (ts.aa == false)
            {
                Response.Write("fffffff");
            }


            int a = 1;
            Response.Write(Convert.ToBoolean(a));*/


            double aa = Convert.ToDouble(19 / 5);
            Response.Write(aa.ToString());
            Response.Write(Convert.ToDouble(19) / 5+"<br>");
            Response.Write(Math.Ceiling(Convert.ToDouble(10)) + "<br>");

            int n = 10;
            double x = 10.012646;
            Response.Write(n + "<br>");
            Response.Write(x + "<br>");
            Response.Write(null + "--<br>");

            string str = Request.QueryString["test"];
            if (str == null)
            {
                Response.Write("null<br>");
            }
            if (str == "")
            {
                Response.Write("空<br>");
            }
            if (str == string.Empty)
            {
                Response.Write("string.empty<br>");
            }

            Response.Write(str + "aa<br>");

            


            //Response.Write(int.Parse(str)+"<br>");
            Response.Write(Convert.ToInt32(str)+"<br>");

            

            /*if (q==0)
            {
                Response.Write("aa");
            }*/

            te t = new te();

            Response.Write(t.q+"<br>");
            Response.Write(t.s + "<br>");
            Response.Write(t.f + "<br>");
            Response.Write(t.dd + "<br>");
            Response.Write(t.ff + "<br>");
            Response.Write(t.da + "<br>");

            if (t.q == 0)
                Response.Write("yes1<br>");

            if (t.s == null)
                Response.Write("yes2<br>");

            if (!t.f)
                Response.Write("yes3<br>");

            if (t.dd == 0.0)
                Response.Write("yes4<br>");

            if (t.ff == 0.0)
                Response.Write("yes5<br>");

            if (t.da.ToString() == "0001-1-1 0:00:00")
                Response.Write("yes6<br>");

        }
    }

    public class te {
        public int q;
        public string s;
        public bool f;
        public double dd;
        public float ff;
        public DateTime da;
    }
}

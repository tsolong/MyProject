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

namespace TL.Web
{
    public partial class ValidateImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TL.Common.ValidateImage i = new TL.Common.ValidateImage(1,4,55,25);
        }
    }
}

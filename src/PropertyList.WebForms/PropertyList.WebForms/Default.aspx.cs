using System;
using System.Web.UI.WebControls;

namespace PropertyList.WebForms
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var service = new PropertyDataService();
            PropertyGrid.DataSource = service.GetAll();
            PropertyGrid.DataBind();
        }

        protected void PropertyGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(PropertyGrid.DataKeys[rowIndex].Value);
                Response.Redirect($"PropertyDetail.aspx?id={id}");

            }
        }
    }
}
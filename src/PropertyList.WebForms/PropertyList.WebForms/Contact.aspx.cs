using System;
using System.Linq;

namespace PropertyList.WebForms
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPropertyRef.DataSource = PropertyData.GetAll();
                ddlPropertyRef.DataTextField = "Address";
                ddlPropertyRef.DataValueField = "Id";
                ddlPropertyRef.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                pnlForm.Visible = false;
                lblSuccess.Text = $"Thanks {txtName.Text}, we received your message about property {ddlPropertyRef.SelectedValue}.";
                lblSuccess.CssClass = "alert alert-success";
                lblSuccess.Visible = true;
            }
        }
    }
}
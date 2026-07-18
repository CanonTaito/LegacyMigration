using System;

namespace PropertyList.WebForms
{
    public partial class PropertyDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (int.TryParse(idStr, out int id))
                {
                    var property = PropertyData.GetById(id);
                    if (property != null)
                    {
                        lblAddress.Text = property.Address;
                        lblPrice.Text = property.Price.ToString("C");
                        lblBedrooms.Text = property.Bedrooms.ToString();
                        lblType.Text = property.PropertyType;
                        lblDescription.Text = property.Description;
                        imgProperty.Src = property.ImageUrl;
                        return;
                    }
                }
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}
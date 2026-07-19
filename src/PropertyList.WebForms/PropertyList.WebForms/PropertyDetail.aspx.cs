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
                    if (id <= 0)
                    {
                        ShowErrorMessage("Invalid property ID. Please select a valid property from the list.");
                        Response.Redirect("~/Default.aspx");
                        return;
                    }
                    
                    var property = PropertyData.GetById(id);
                    if (property != null)
                    {
                        DisplayPropertyDetails(property);
                    }
                    else
                    {
                        ShowErrorMessage($"Property with ID {id} was not found. It may have been removed or you may not have permission to view it.");
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    ShowErrorMessage("Please select a valid property from the home page.");
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        
        private void ShowErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
        
        private void DisplayPropertyDetails(Property property)
        {
            if (property == null) return;
            
            lblAddress.Text = property.Address;
            lblPrice.Text = property.Price.ToString("C");
            lblBedrooms.Text = property.Bedrooms.ToString();
            lblType.Text = property.PropertyType;
            lblDescription.Text = property.Description;
            imgProperty.Src = property.ImageUrl;
        }
        
        private void NavigateToDefault()
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
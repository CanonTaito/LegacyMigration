using System;

namespace PropertyList.WebForms
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();
            string propertyType = ddlType.SelectedValue;

            var results = PropertyData.Search(keyword, propertyType);
            SearchResults.DataSource = results;
            SearchResults.DataBind();
        }
    }
}
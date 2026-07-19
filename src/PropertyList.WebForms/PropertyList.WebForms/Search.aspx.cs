using System;
using System.Collections.Generic;

namespace PropertyList.WebForms
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Enhanced server-side validation
            string validationErrors = ValidateSearchInput();
            
            if (!string.IsNullOrEmpty(validationErrors))
            {
                DisplayValidationMessage(validationErrors);
                return;
            }
            
            // Sanitized input processing
            string keyword = SanitizeSearchInput(txtKeyword.Text);
            string propertyType = ddlType.SelectedValue ?? "";
            
            // Execute search with error handling
            try
            {
                var service = new PropertyDataService();
                var results = service.Search(keyword, propertyType);
                DisplaySearchResults(results);
                DisplaySearchTips();
            }
            catch (Exception ex)
            {
                LogSearchError(ex);
                DisplayErrorMessage("An error occurred during search. Please try again.");
            }
        }
        
        // Enhanced validation method
        private string ValidateSearchInput()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                errors.Add("Please enter a search keyword.");
            }
            else if (txtKeyword.Text.Length > 100)
            {
                errors.Add("Search keyword too long (max 100 characters).");
            }
            else if (!IsValidSearchTerm(txtKeyword.Text))
            {
                errors.Add("Invalid search term. Please use letters, numbers, and spaces only.");
            }
            
            return string.Join("<br/>", errors);
        }
        
        private string SanitizeSearchInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            
            // Basic sanitization
            return input.Trim();
        }
        
        private bool IsValidSearchTerm(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return false;
            
            // Allow letters, numbers, spaces, and basic punctuation
            foreach (char c in term)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) && 
                    c != ',' && c != '.' && c != '-' && c != '!' && c != '?')
                {
                    return false;
                }
            }
            
            return true;
        }
        
        private void DisplayValidationMessage(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
        
        private void DisplaySearchResults(List<Property> results)
        {
            SearchResults.DataSource = results;
            SearchResults.DataBind();
        }
        
        private void DisplaySearchTips()
        {
                if (SearchResults.Rows.Count > 0)
            {
                lblTips.Text = "Search tips: Use keywords from property descriptions (e.g., 'pool', 'garden', 'city views')";
                lblTips.Visible = true;
            }
        }
        
        private void DisplayErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
        
        private void LogSearchError(Exception ex)
        {
            // Log search errors for debugging
            System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropertyList.WebForms
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPropertyReferenceData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!PerformBusinessValidation())
            {
                return;
            }
            
            if (!Page.IsValid)
            {
                return;
            }
            
            try
            {
                ProcessContactSubmission();
                DisplaySuccessMessage();
                ClearForm();
            }
            catch (Exception ex)
            {
                HandleContactProcessingError(ex);
            }
        }
        
        private bool PerformBusinessValidation()
        {
            var errors = new List<string>();
            
            if (!ValidateContactName(txtName.Text))
            {
                errors.Add("Name must be at least 2 characters and contain only letters and spaces.");
            }
            
            if (!ValidateContactEmail(txtEmail.Text))
            {
                errors.Add("Please provide a valid email address.");
            }
            
            if (!ValidatePropertySelection(ddlPropertyRef.SelectedValue))
            {
                errors.Add("Please select a property you're interested in.");
            }
            
            if (!ValidateContactMessage(txtMessage.Text))
            {
                errors.Add("Message must be at least 10 characters describing your inquiry.");
            }
            
            if (errors.Count > 0)
            {
                DisplayBusinessValidationErrors(errors);
                return false;
            }
            
            return true;
        }
        
        private bool ValidateContactName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && 
                   name.Length >= 2 && 
                   name.Length <= 100 && 
                   name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
        
        private bool ValidateContactEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && 
                   email.Contains("@") && 
                   email.Contains(".") &&
                   email.Length <= 320;
        }
        
        private bool ValidatePropertySelection(string selectedValue)
        {
            return !string.IsNullOrEmpty(selectedValue) && 
                   int.TryParse(selectedValue, out int id) && 
                   id > 0;
        }
        
        private bool ValidateContactMessage(string message)
        {
            return !string.IsNullOrWhiteSpace(message) && 
                   message.Length >= 10 && 
                   message.Length <= 2000;
        }
        
        private void LoadPropertyReferenceData()
        {
            try
            {
                var propertyDataService = new PropertyDataService();
                ddlPropertyRef.DataSource = propertyDataService.GetAll();
                ddlPropertyRef.DataTextField = "Address";
                ddlPropertyRef.DataValueField = "Id";
                ddlPropertyRef.DataBind();
            }
            catch (Exception ex)
            {
                LogError(ex);
                ShowErrorMessage("Unable to load property reference data. Please try again later.");
            }
        }
        
        private void ProcessContactSubmission()
        {
            try
            {
                pnlForm.Visible = false;
                
                lblSuccess.Text = $"Thank you for your interest! We've received your message about property {ddlPropertyRef.SelectedItem.Text}.";
                lblSuccess.CssClass = "alert alert-success";
                lblSuccess.Visible = true;
                
                LogContactSubmission(txtName.Text, txtEmail.Text, ddlPropertyRef.SelectedValue);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
        
        private void DisplaySuccessMessage()
        {
            pnlForm.Visible = false;
            lblSuccess.Text = $"Thank you for your interest! We've received your message about property {ddlPropertyRef.SelectedItem.Text}.";
            lblSuccess.CssClass = "alert alert-success";
            lblSuccess.Visible = true;
        }
        
        private void ClearForm()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMessage.Text = string.Empty;
            ddlPropertyRef.SelectedIndex = 0;
            
            ClearValidationMessages();
        }
        
        private void DisplayBusinessValidationErrors(List<string> errors)
        {
            lblError.Text = String.Join("<br/>", errors);
            lblError.Visible = true;
        }
        
        private void ShowErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }
        
        private void ClearValidationMessages()
        {
            lblError.Visible = false;
        }
        
        private void HandleContactProcessingError(Exception ex)
        {
            LogError(ex);
            ShowErrorMessage("An unexpected error occurred while processing your request. Please try again later or contact support.");
            pnlForm.Visible = true;
        }
        
        private void LogError(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Contact form error: {ex.Message}\n{ex.StackTrace}");
        }
        
        private void LogContactSubmission(string name, string email, string propertyId)
        {
            System.Diagnostics.Debug.WriteLine($"Contact submitted: {name}, {email}, Property {propertyId}");
        }
    }
}
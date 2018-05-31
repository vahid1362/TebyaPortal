namespace QTasMarketing.Web.Areas.Admin.Models.Memebership
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsPresistant { get; set; }
    }
}

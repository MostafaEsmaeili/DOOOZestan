namespace Doozestan.UserManagement
{
    public static class AuthenticationManager
    {
        public static AuthenticationProvider AuthenticationProvider { get; set; }

        static AuthenticationManager()
        {
            AuthenticationProvider = new AuthenticationProvider();
        }

    
    }
}

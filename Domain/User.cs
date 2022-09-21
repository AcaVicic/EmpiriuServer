namespace Domain
{
    /// <summary>
    /// Class <c>User</c> represents registered user.
    /// </summary>
    public class User
    {

        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the user's primary key.
        /// </value>
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// <value>
        /// Property <c>Email</c> represents the user's login email.
        /// </value>
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// <value>
        /// Property <c>Username</c> represents the user's empiriu username.
        /// </value>
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// <value>
        /// Property <c>Password</c> represents the user's login password.
        /// </value>
        /// </summary>
        public string? Password { get; set; }

        public override string ToString()
        {
            return $"{Id}={Email}={Username}={Password}";
        }
    }

}
using E_Commerce.Domain.Common.Persistent;
using E_Commerce.Domain.Common.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Model.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        
        

        //private User() { } // Required for deserialization (e.g., ORM)
        private User():base(UserId.CreateUnique())
        {
            
        }

        private User(UserId id,string firstName,string lastName,string username, string email):base(id)
        {
            FirstName = firstName;
            SecondName = lastName;
            Username = username;
            Email = email;
        }

        public static User Create(UserId userId,string firstName,string lastName,string username,string email)
        {
            return new(userId,firstName,lastName,username,email);
        }

        public void ChangeUsername(string newUsername)
        {
            Username = newUsername;
        }

        public void ChangeEmail(string newEmail)
        {
            Email = newEmail;
        }

        

    }

    

    // Similar event classes for other user changes (EmailChanged, PasswordChanged, RoleAdded, RoleRemoved)

   

    

}

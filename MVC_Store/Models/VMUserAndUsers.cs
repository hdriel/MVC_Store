using MVC_Store.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Store.Models
{
    public class VMUserAndUsers
    {
        public User user { get; set; }
        public List<User> users { get; set; }

        public VMUserAndUsers()
        {
            UserDal dal = new UserDal();
            List<User> listUsers = (from x in dal.Users
                                    select x).ToList<User>();
            this.user = new User();
            this.users = listUsers;
        }

        public VMUserAndUsers(User u)
        {
            UserDal dal = new UserDal();
            List<User> listUsers = (from x in dal.Users
                                    select x).ToList<User>();
            this.user = u;
            this.users = listUsers;
        }
    }    
}
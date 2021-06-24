using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Helper class for performing database calls related to the user table
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// We retrieve a user by userName
        /// </summary>
        /// <param name="dc">DataContext</param>
        /// <param name="userName">userName of the desired user</param>
        /// <returns>A specific user</returns>
        public static User GetUserByUserName(DataContext dc, string userName)
        {
            return dc.User.FirstOrDefault(x => x.UserName == userName);
        }

        /// <summary>
        /// We retrieve all the users from the database
        /// </summary>
        /// <param name="dc">DataContext</param>
        /// <returns>All the users</returns>
        public static List<User> GetUsers(DataContext dc)
        {
            return dc.User.ToList<User>();
        }
    }
}

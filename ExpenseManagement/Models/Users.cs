using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum Role
    {
        Employee,Manager
    }
    public class Users
    {
        public int userId { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public Role role { get; set; }
        /// <summary>
        /// Constructor of Users class used for Logging in
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public Users(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
        public Users() { }
        /// <summary>
        /// Constructor of USers class used for Registering a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        public Users(string username, string password, int role)
        {
            this.username = username;
            this.password = password;
            this.role = (Role) role;
        }
        /// <summary>
        /// Constructor of Users clas used for collecting information from database in GetAllUsers methods
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        public Users(int userId, string username, string password, Role role)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.role = role;
        }
        /// <summary>
        /// Converts a casted string from the database into an integer for reference to the Role enum
        /// </summary>
        /// <param name="s"></param>
        /// <returns>integer of related role</returns>
        public int RoleToNum(string s)
        {
           if(s == "Manager")
            {
                return 1;
            }
            else { return 0; }
        }
        /// <summary>
        /// For putting a role into a string for the purposes of updating a user or creating a user 
        /// </summary>
        /// <param name="role"></param>
        /// <returns>string representing the role of the user</returns>
        public string RoleToString(Role role)
        {
            if(role == Role.Employee)
            {
                return "Employee";
            }
            else { return "Manager"; }
        }
        public override string ToString()
        {
            return $"Id: {this.userId}, Name: {this.username}, Role: {this.role}";
        }
    }
}

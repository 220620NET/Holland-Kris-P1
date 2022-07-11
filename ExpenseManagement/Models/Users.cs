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
        public string username { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
        //For Logging in
        public Users(string username,string password)
        {
            this.username = username;
            this.password = password;
        }
        public Users() { }
        // For Registering account need to ask if employee or manager
        public Users(string username, string password, int role)
        {
            this.username = username;
            this.password = password;
            this.role = (Role) role;
        }
        // For Getting all users
        public Users(int userId, string username, string password, Role role)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.role = role;
        }
        public int RoleToNum(string s)
        {
           if(s == "Manager")
            {
                return 1;
            }
            else { return 0; }
        }
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

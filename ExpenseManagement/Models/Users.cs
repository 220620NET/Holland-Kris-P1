﻿using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum Role
    {
        //[EnumMember(Value = "Employee")]
        Employee,
        //[EnumMember(Value = "Manager")]
        Manager
    }
    public class Users
    {
        [Key]
        public int userId { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }

        public Role role { get; set; }
        /// <summary>
        /// Constructor of Users class used for Logging in
        /// </summary>
        /// <param name="username">A valid username</param>
        /// <param name="password">A valid password</param>
        public Users(string? username,string? password):this()
        {
            this.username = username;
            this.password = password;
        }
        public Users() {
            userId = 0;
            username = "";
            password = "";
            role = Role.Employee;
        }
        /// <summary>
        /// Constructor of Users class used for Registering a new user
        /// </summary>
        /// <param name="username">A valid username</param>
        /// <param name="password">A valid password</param>
        /// <param name="role">{employee = 0, Manager=1}</param>
        public Users(string? username, string? password, int role):this()
        {
            this.username = username;
            this.password = password;
            this.role = (Role) role;
        }
        /// <summary>
        /// Constructor of Users clas used for collecting information from database in GetAllUsers methods
        /// </summary>
        /// <param name="userId">Unique id from database</param>
        /// <param name="username">A valid username</param>
        /// <param name="password">A valid password</param>
        /// <param name="role">{employee = 0, Manager=1}</param>
        public Users(int userId, string? username, string? password, int role):this()
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.role = (Role)role;
        }
        /// <summary>
        /// Converts a casted string from the database into an integer for reference to the Role enum
        /// </summary>
        /// <param name="s">string from database</param>
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
        /// <param name="role">The role as an enumerator</param>
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
            return $"Id: {this.userId}, Name: {this.username},Password: {this.password}, Role: {RoleToString(this.role)}";
        }
    }
}

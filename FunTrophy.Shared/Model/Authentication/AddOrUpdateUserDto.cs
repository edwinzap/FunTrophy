﻿namespace FunTrophy.Shared.Model.Authentication
{
    public class AddOrUpdateUserDto
    {
        public string UserName { get; set; }
        public UserRoles[] Roles { get; set; }
    }
}
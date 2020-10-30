using System;
using System.Collections.Generic;
using System.Text;

namespace BackendSolution.Entity
{
    public class User
    {
        public long ID { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
    }
}

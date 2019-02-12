using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class User:SuperEntity
    {
        public string IdentityGuid { get; private set; }
        public User()
        {

        }
        public User(string userIdentity):base()
        {
            IdentityGuid = userIdentity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonDB.Model
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserLevel { get; set; }
        public int UserID { get; set; }
    }
}

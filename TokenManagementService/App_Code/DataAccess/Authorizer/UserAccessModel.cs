
using DataAccess.SQL.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.CustomAuthorizer{
    
    public static class UserAccessModel{
        private static readonly UserEntities UserEF = new UserEntities();

        private static readonly String _QUERY_SELECT = "SELECT * FROM User WHERE username = @username AND password = @password ";

        public static bool UserExists(String username, String password){
            return UserEF.User.Any(obj => obj.username.Equals(username) && obj.password.Equals(password));
        }

    }
}

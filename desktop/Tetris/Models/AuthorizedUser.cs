using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models
{
    internal class AuthorizedUser
    {
        public string id;
        public string role;
        private string _userName;
        private string _token;
        private DateTime _expirationDate;

        public AuthorizedUser(string id, string role, string userName, string token, DateTime expirationDate)
        {
            _token = token;
            this.id = id;
            _userName = userName;
            this.role = role;
            _expirationDate = expirationDate;
        }
    }
}

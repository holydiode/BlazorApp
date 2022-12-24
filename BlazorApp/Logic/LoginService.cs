using BlazorApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.Logic
{
    public class LoginService
    {
        protected BaseContext _context;
        public LoginService()
        {
            _context = new BaseContext();
        }
        public LoginService(BaseContext context)
        {
            _context = context;
        }

        private string EncryptString(string str)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(hash);
        }

        public void CreatePlayer(string login, string password, string name)
        {
            var count_player_with_same_name = _context.Players.Where(x => x.Nikname == name || x.Nikname == login).Count();
            var count_player_with_same_login = _context.Players.Where(x => x.Login == login || x.Login == name).Count();
            if (count_player_with_same_name == 0 && count_player_with_same_login == 0)
            {
                var newPlayer = new Player()
                {
                    Nikname = name,
                    Login = login,
                    Balance = 0,
                    PasswordHash = EncryptString(password)
                };
                _context.Players.Add(newPlayer);
                _context.SaveChanges();
            }
        }

        public Player GetRegistratedAccaunt(string login, string password)
        {
            var login_by_nikname = _context.Players.Where(x => x.Nikname == login && x.PasswordHash == EncryptString(password)).FirstOrDefault();
            if (login_by_nikname is null)
            {
                var login_by_login = _context.Players.Where(x => x.Login == login && x.PasswordHash == EncryptString(password)).FirstOrDefault();
                return login_by_login;
            }
            return login_by_nikname;
        }

        public Player GetAccauntByID(int id)
        {
            var login_by_nikname = _context.Players.Find(id);
            return login_by_nikname;
        }

        public void ForceRemovePlayer(int id)
        {
            var accaunt = GetAccauntByID(id);
            if (accaunt is not null)
            {
                _context.Players.Remove(accaunt);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Player> GetListOfPlayer()
        {
            return _context.Players;
        }

        public void ChangePassword(int id, string new_password) {
            
            var accaunt = GetAccauntByID(id);
            if (accaunt is not null)
            {
                accaunt.PasswordHash = EncryptString(new_password);
                _context.SaveChanges();
            }
        }
    }
}

using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Logic
{
    public class PermissionService
    {
        protected BaseContext _context;
        public PermissionService()
        {
            _context = new BaseContext();
        }
        public PermissionService(BaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Permission> GetPlayerPermission(int player_id)
        {

            var permission = new List<Permission>();
            var player = _context.Players.Where(player => player.PlayerId == player_id)
                .Include(player => player.Permissions)
                .Include(player => player.ValidRoles
                    .Where(role => role.ExpirationDate < DateTime.Now)
                    )
                .ThenInclude(role => role.Role)
                .ThenInclude(role => role.Inherited)
                .Include(role => role.Permissions).FirstOrDefault();

            if (player is null)
            {
                return permission;
            }

            permission.AddRange(player.Permissions);
            foreach(var role in player.ValidRoles)
            {
                var current_role = role.Role;
                while (current_role != null)
                {
                    permission.AddRange(current_role.Permissions);
                    current_role = current_role.Inherited;
                }
            }

            return permission;
        }

        public IEnumerable<Permission> GetRolePermission(int role_id)
        {
            var permission = new List<Permission>();
            var role = _context.Roles.Where(role => role.RoleId == role_id)
                .Include(role => role.Inherited)
                .Include(role => role.Permissions).FirstOrDefault();

            if (role is null)
            {
                return permission;
            }

            var current_role = role;
            while (current_role != null)
            {
                permission.AddRange(current_role.Permissions);
                current_role = current_role.Inherited;
            }
            return permission;
        }

        public void PlayerAppendPermission(int player_id, Permission permission)
        {
            var player = _context.Players.Where(player => player.PlayerId == player_id).Include(player => player.Permissions).FirstOrDefault();
            if (player is not null)
            {
                player.Permissions.Add(permission);
                _context.SaveChanges();
            }
        }

        public void RoleAppendPermission(int role_id, Permission permission)
        {
            var role = _context.Roles.Where(role => role.RoleId == role_id).Include(role => role.Permissions).FirstOrDefault();
            if (role is not null)
            {
                role.Permissions.Add(permission);
                _context.SaveChanges();
            }
        }

        public void PlayerRemovePermission(int player_id, Permission permission)
        {
            var player = _context.Players.Where(player => player.PlayerId == player_id).Include(player => player.Permissions).FirstOrDefault();
            if (player is not null)
            {
                player.Permissions.Remove(permission);
                _context.SaveChanges();
            }
        }

        public void RoleRemovePermission(int role_id, Permission permission)
        {
            var role = _context.Roles.Where(role => role.RoleId == role_id).Include(role => role.Permissions).FirstOrDefault();
            if (role is not null)
            {
                role.Permissions.Remove(permission);
                _context.SaveChanges();
            }
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public Role GetRoleByID(int id)
        {
            return _context.Roles.Find(id);
        }

        public void DeleteRoleByID(int id)
        {
            var role = GetRoleByID(id);
            if (role is not null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Role> GetRoleList()
        {
            return _context.Roles.ToList();
        }


        public Role GetRoleByName(string name)
        {
            return _context.Roles.Where(role => role.Name == name).FirstOrDefault();
        }

        public void AddRelation(Role main, Role child)
        {
            main = GetRoleByID(main.RoleId);
            child = GetRoleByID(child.RoleId);
            if(main is not null && child is not null)
            {
                child.Inherited = main;
                _context.SaveChanges();
            }
        }

        public void TryMakePurchase(Player player, Role role)
        {
            player = _context.Players.Where(player => player.PlayerId == player.PlayerId)
                .Include(player => player.ValidRoles).FirstOrDefault();
            role = GetRoleByID(role.RoleId);

            if (player is not null && role is not null)
            {
                if(player.Balance > role.Price)
                {
                    player.Balance -= role.Price;
                    player.ValidRoles.Add(new ValidRole()
                    {
                        Role = role,
                        ReceivingDate = DateTime.Now,
                        ExpirationDate = DateTime.Now.AddDays(30)
                    }) ;
                }
            }
        }
    }
}

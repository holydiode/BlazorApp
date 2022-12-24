using Xunit;
using BlazorApp.Logic;
using BlazorApp.Data;
using System.Linq;
using EntityFrameworkCore.Testing.Moq;

namespace TestLaba
{
    public class PermissionTest
    {
        [Fact]
        public void GettingPermissionFromPlayer()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var login_service = new LoginService(mock);
            login_service.CreatePlayer("a@mail.com", "qwerty123", "player");
            var player = login_service.GetRegistratedAccaunt("a@mail.com", "qwerty123");
            var service = new PermissionService(mock);
            var permission = new Permission { PermissionName = "root" };
            service.PlayerAppendPermission(player.PlayerId, permission);
            var list = service.GetPlayerPermission(player.PlayerId);
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Same(list.First(), permission);
        }

        [Fact]
        public void GettingPermissionFromRole()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var service = new PermissionService(mock);
            var role = new Role { Name = "admin"};
            service.AddRole(role);
            role = service.GetRoleByName(role.Name);
            var permission = new Permission { PermissionName = "root" };
            service.RoleAppendPermission(role.RoleId, permission);
            var list = service.GetRolePermission(role.RoleId);
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Same(list.First(), permission);
        }

        [Fact]
        public void GettingPLayerPermissionFromRole()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var login_service = new LoginService(mock);
            login_service.CreatePlayer("a@mail.com", "qwerty123", "player");
            var player = login_service.GetRegistratedAccaunt("a@mail.com", "qwerty123");
            var service = new PermissionService(mock);
            var role = new Role { Name = "admin", Price  = -10};
            service.AddRole(role);
            role = service.GetRoleByName(role.Name);
            var permission = new Permission { PermissionName = "root" };
            service.RoleAppendPermission(role.RoleId, permission);
            service.TryMakePurchase(player, role);
            var list = service.GetPlayerPermission(player.PlayerId);
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Same(list.First(), permission);
        }

        [Fact]
        public void GettingPLayerPermissionFromRoleTree()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var login_service = new LoginService(mock);
            login_service.CreatePlayer("a@mail.com", "qwerty123", "player");
            var player = login_service.GetRegistratedAccaunt("a@mail.com", "qwerty123");
            var service = new PermissionService(mock);
            var role = new Role { Name = "admin", Price = -10 };
            var master_role = new Role { Name = "master"};
            service.AddRole(role);
            service.AddRole(master_role);
            service.AddRelation(master_role, role);

            var permission = new Permission { PermissionName = "root" };
            service.RoleAppendPermission(master_role.RoleId, permission);
            service.TryMakePurchase(player, role);
            var list = service.GetPlayerPermission(player.PlayerId);
            Assert.NotNull(list);
            Assert.Single(list);
            Assert.Same(list.First(), permission);
        }

        [Fact]
        public void RejectingPurchase()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var login_service = new LoginService(mock);
            login_service.CreatePlayer("a@mail.com", "qwerty123", "player");
            var player = login_service.GetRegistratedAccaunt("a@mail.com", "qwerty123");
            var service = new PermissionService(mock);
            var role = new Role { Name = "admin", Price = 10 };
            service.AddRole(role);
            role = service.GetRoleByName(role.Name);
            var permission = new Permission { PermissionName = "root" };
            service.RoleAppendPermission(role.RoleId, permission);
            service.TryMakePurchase(player, role);
            var list = service.GetPlayerPermission(player.PlayerId);
            Assert.Empty(list);
        }

        [Fact]
        public void RemovePermissionFromPlayer()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var login_service = new LoginService(mock);
            login_service.CreatePlayer("a@mail.com", "qwerty123", "player");
            var player = login_service.GetRegistratedAccaunt("a@mail.com", "qwerty123");
            var service = new PermissionService(mock);
            var permission = new Permission { PermissionName = "root" };
            service.PlayerAppendPermission(player.PlayerId, permission);
            service.PlayerRemovePermission(player.PlayerId, permission);
            var list = service.GetPlayerPermission(player.PlayerId);
            Assert.Empty(list);
        }
        [Fact]
        public void RemovePermissionFromRole()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var service = new PermissionService(mock);
            var role = new Role { Name = "admin" };
            service.AddRole(role);
            var permission = new Permission { PermissionName = "root" };
            service.RoleAppendPermission(role.RoleId, permission);
            service.RoleRemovePermission(role.RoleId, permission);
            var list = service.GetRolePermission(role.RoleId);
            Assert.Empty(list);
        }
    }
}

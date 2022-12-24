using System.Linq;
using BlazorApp.Data;
using Xunit;

namespace TestLaba.DataTest
{
    public class PermissionDataTest
    {
        [Fact]
        public void TestAddPermission()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Permissions.Add(new Permission() { PermissionName = "root" });
            context.SaveChanges();
            transaction.Rollback();
        }

        [Fact]
        public void TestSelectPermission()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Permissions.Add(new Permission() { PermissionName = "root" });
            context.SaveChanges();
            var value = context.Permissions.Where(x => x.PermissionName == "root").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

        [Fact]
        public void TestDeletePermission()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var Permission = new Permission() { PermissionName = "root" };
            context.Permissions.Add(Permission);
            context.SaveChanges();
            context.Permissions.Remove(Permission);
            context.SaveChanges();
            var value = context.Permissions.Where(x => x.PermissionName == "root").FirstOrDefault();
            transaction.Rollback();
            Assert.Null(value);
        }

        [Fact]
        public void TestChangePermission()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var Permission = new Permission() { PermissionName = "root" };
            context.Permissions.Add(Permission);
            context.SaveChanges();
            Permission.PermissionName = "support";
            context.SaveChanges();
            var value = context.Permissions.Where(x => x.PermissionName == "support").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }
    }
}

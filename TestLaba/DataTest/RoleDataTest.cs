using System.Linq;
using BlazorApp.Data;
using Xunit;

namespace TestLaba.DataTest
{
    public class RoleDataTest
    {
        [Fact]
        public void TestAddRole()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Roles.Add(new Role() { Name = "root" });
            context.SaveChanges();
            transaction.Rollback();
        }

        [Fact]
        public void TestSelectRole()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Roles.Add(new Role() { Name = "root" });
            context.SaveChanges();
            var value = context.Roles.Where(x => x.Name == "root").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

        [Fact]
        public void TestDeleteRole()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var Role = new Role() { Name = "root" };
            context.Roles.Add(Role);
            context.SaveChanges();
            context.Roles.Remove(Role);
            context.SaveChanges();
            var value = context.Roles.Where(x => x.Name == "root").FirstOrDefault();
            transaction.Rollback();
            Assert.Null(value);
        }

        [Fact]
        public void TestChangeRole()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var Role = new Role() { Name = "root" };
            context.Roles.Add(Role);
            context.SaveChanges();
            Role.Name = "support";
            context.SaveChanges();
            var value = context.Roles.Where(x => x.Name == "support").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }
    }
}

using System.Linq;
using BlazorApp.Data;
using Xunit;

namespace TestLaba.DataTest
{
    public class ServerDataTest
    {
        [Fact]
        public void TestAddServer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.GameServers.Add(new GameServer() { Port = 1, Core = new Core()});
            context.SaveChanges();
            transaction.Rollback();
        }

        [Fact]
        public void TestSelectServer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.GameServers.Add(new GameServer() { Port = 1, Core = new Core() });
            context.SaveChanges();
            var value = context.GameServers.Where(x => x.Port == 1 ).FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

        [Fact]
        public void TestDeleteServer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var server = new GameServer() { Port = 1, Core = new Core() };
            context.GameServers.Add(server);
            context.SaveChanges();
            context.GameServers.Remove(server);
            context.SaveChanges();
            var value = context.GameServers.Where(x => x.Port == 1).FirstOrDefault();
            transaction.Rollback();
            Assert.Null(value);
        }


        [Fact]
        public void TestChangeCore()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var server = new GameServer() { Port = 1, Core = new Core() };
            context.GameServers.Add(server);
            context.SaveChanges();
            server.Port = 2;
            context.SaveChanges();
            var value = context.GameServers.Where(x => x.Port == 2).FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

    }
}

using System.Linq;
using BlazorApp.Data;
using Xunit;

namespace TestLaba.DataTest
{
    public class PlayerDataTest
    {
        [Fact]
        public void TestAddPlayer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Players.Add(new Player() { Nikname = "admin" });
            context.SaveChanges();
            transaction.Rollback();
        }

        [Fact]
        public void TestSelectPlayer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Players.Add(new Player() { Nikname = "admin" });
            context.SaveChanges();
            var value = context.Players.Where(x => x.Nikname == "admin").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

        [Fact]
        public void TestDeletePlayer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var player = new Player() { Nikname = "admin" };
            context.Players.Add(player);
            context.SaveChanges();
            context.Players.Remove(player);
            context.SaveChanges();
            var value = context.Players.Where(x => x.Nikname == "admin").FirstOrDefault();
            transaction.Rollback();
            Assert.Null(value);
        }

        [Fact]
        public void TestChangePlayer()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var player = new Player() { Nikname = "admin" };
            context.Players.Add(player);
            context.SaveChanges();
            player.Nikname = "root";
            context.SaveChanges();
            var value = context.Players.Where(x => x.Nikname == "root").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

    }
}

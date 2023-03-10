using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Data;
using Xunit;

namespace TestLaba.DataTest
{

    public class CoreDataTest
    {
        [Fact]
        public void TestAddCore()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Cores.Add(new Core() {Name = "Pomelo"});
            context.SaveChanges();
            transaction.Rollback();
        }

        [Fact]
        public void TestSelectCore()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            context.Cores.Add(new Core() { Name = "Pomelo" });
            context.SaveChanges();
            var value = context.Cores.Where(x => x.Name == "Pomelo").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

        [Fact]
        public void TestDeleteCore()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var core = new Core() { Name = "Pomelo" };
            context.Cores.Add(core);
            context.SaveChanges();
            context.Cores.Remove(core);
            context.SaveChanges();
            var value = context.Cores.Where(x => x.Name == "Pomelo").FirstOrDefault();
            transaction.Rollback();
            Assert.Null(value);
        }


        [Fact]
        public void TestChangeCore()
        {
            var context = new BaseContext();
            var transaction = context.Database.BeginTransaction();
            var core = new Core() { Name = "Pomelo" };
            context.Cores.Add(core);
            context.SaveChanges();
            core.Name = "Lime";
            context.SaveChanges();
            var value = context.Cores.Where(x => x.Name == "Lime").FirstOrDefault();
            transaction.Rollback();
            Assert.NotNull(value);
        }

    }
}

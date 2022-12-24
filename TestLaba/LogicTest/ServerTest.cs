using Xunit;
using BlazorApp.Logic;
using BlazorApp.Data;
using EntityFrameworkCore.Testing.Moq;

namespace TestLaba
{
    public class ServerTest
    {
        [Fact]
        public void CreationServerAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var server = new GameServer() {Name = "server"};
            service.AddServer(server);
            var returned_server = service.GetServerByID(server.ServerId);
            Assert.Same(returned_server, server);
        }

        [Fact]
        public void CreationCoreAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var core = new Core() { Name = "Pomello" };
            service.AddCore(core);
            var returned_core = service.GetCoreByID(core.CoreId);
            Assert.Same(returned_core, core);
        }


        [Fact]
        public void removeServerAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var server = new GameServer() { Name = "server" };
            service.AddServer(server);
            service.RemoveServerByID(server.ServerId);
            server = service.GetServerByID(server.ServerId);
            Assert.Null(server);
        }

        [Fact]
        public void RemoveCoreAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var core = new Core() { Name = "Pomello" };
            service.AddCore(core);
            service.RemoveCoreByID(core.CoreId);
            core = service.GetCoreByID(core.CoreId);
            Assert.Null(core);
        }

        [Fact]
        public void ChangeCoreAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var core = new Core() { Name = "Pomello" };
            service.AddCore(core);
            var other_core = new Core() { Name = "Lime" };
            service.ChangeCoreByID(core.CoreId, other_core);
            
            Assert.Equal(other_core.Name, core.Name);
        }

        [Fact]
        public void ChangeServerAviable()
        {
            var mock = Create.MockedDbContextFor<BaseContext>();
            var context = mock;
            var service = new ServerService(context);
            var server = new GameServer() { Name = "server" };
            service.AddServer(server);
            var other_server = new GameServer() { Name = "other server" };
            service.ChangeSererByID(server.ServerId, other_server);
            Assert.Equal(other_server.Name, server.Name);
        }
    }
}

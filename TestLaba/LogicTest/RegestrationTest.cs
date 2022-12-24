using System;
using Xunit;
using BlazorApp.Logic;
using BlazorApp.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using EntityFrameworkCore.Testing.Moq;

namespace TestLaba
{


    public class RegestrationTest
    {
        [Fact]
        public void RegestrationIsAviable()
        {
           var mock = Create.MockedDbContextFor<BaseContext>();
           var context = mock; 
           var service = new LoginService(context);
           service.CreatePlayer("a@mail.com", "qwerty123", "player");
        }

        [Fact]
        public void RegestrationIsCommited()
        {
            var login = "a@mail.com";
            var password = "qwerty123";
            var name = "player";
            
            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            var player_by_login = service.GetRegistratedAccaunt(login, password);
            var player_by_name = service.GetRegistratedAccaunt(name, password);
            Assert.NotNull(player_by_login);
            Assert.NotNull(player_by_name);
            Assert.Same(player_by_login, player_by_name);
        }

        [Fact]
        public void DoubleRegestrationIsRejected()
        {
            var login = "a@mail.com";
            var other_login = "b@mail.com";
            var password = "qwerty123";
            var name = "player";
            var other_name = "admin";
            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            service.CreatePlayer(login, password, name);
            service.CreatePlayer(login, password, other_name);
            service.CreatePlayer(other_login, password, name);
            Assert.Equal(1, context.Players.Count());
        }

        [Fact]
        public void GetingPlayerByID()
        {
            var login = "a@mail.com";
            var password = "qwerty123";
            var name = "player";

            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            var player = service.GetRegistratedAccaunt(login, password);
            var same_player = service.GetAccauntByID(player.PlayerId);
            Assert.Same(player, same_player);
        }

        [Fact]
        public void RemovingPlayerByID()
        {
            var login = "a@mail.com";
            var password = "qwerty123";
            var name = "player";

            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            var player = service.GetRegistratedAccaunt(login, password);
            service.ForceRemovePlayer(player.PlayerId);
            player = service.GetRegistratedAccaunt(login, password);
            Assert.Null(player);
        }

        [Fact]
        public void GetingListOFPlayer()
        {
            var login = "a@mail.com";
            var other_login = "b@mail.com";
            var password = "qwerty123";
            var name = "player";
            var other_name = "admin";
            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            service.CreatePlayer(other_login, password, other_name);
            var list_of_player = service.GetListOfPlayer();
            Assert.Equal(2, list_of_player.Count());
        }

        [Fact]
        public void ChangingPassword()
        {
            var login = "a@mail.com";
            var password = "qwerty123";
            var new_password = "123qwerty";
            var name = "player";

            var context = Create.MockedDbContextFor<BaseContext>();
            var service = new LoginService(context);
            service.CreatePlayer(login, password, name);
            var player = service.GetRegistratedAccaunt(login, password);
            service.ChangePassword(player.PlayerId, new_password);
            player = service.GetRegistratedAccaunt(login, password);
            Assert.Null(player);
            player = service.GetRegistratedAccaunt(login, new_password);
            Assert.NotNull(player);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BlazorApp.Logic;
using BlazorApp.Data;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly LoginService _logining_service;

        private readonly PermissionService _permission_service;

        public PlayerController(LoginService service)
        {
            this._logining_service = service;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_logining_service.GetListOfPlayer());
        }

        [HttpGet("{id}")]
        public Player Get(int id)
        {
            return _logining_service.GetAccauntByID(id);
        }

        [Route("auth")]
        [HttpGet]
        public bool Auth(string login, string password)
        {
            _logining_service.GetRegistratedAccaunt(login, password);
            return _logining_service.GetRegistratedAccaunt(login, password) is not null;
        }

        [Route("register")]
        [HttpGet]
        public bool Register(string login, string password, string name)
        {
            _logining_service.CreatePlayer(login, password, name);
            return _logining_service.GetRegistratedAccaunt(login, password) is not null;

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logining_service.ForceRemovePlayer(id);
        }


        [Route("permission")]
        [HttpGet]
        public IEnumerable<Permission> PermissionList(int id)
        {
            return  _permission_service.GetPlayerPermission(id);
        }

    }














}

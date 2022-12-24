using Microsoft.AspNetCore.Mvc;
using BlazorApp.Logic;
using BlazorApp.Data;
using System.Collections.Generic;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ServerService _service;

        public ServerController(ServerService service)
        {
            this._service = service;
        }

        [HttpGet]
        public IEnumerable<GameServer> Get()
        {
            return _service.GetServersList();
        }

        [HttpGet("{id}")]
        public GameServer Get(int id)
        {
            return _service.GetServerByID(id);
        }

        [Route("create")]
        [HttpGet]
        public void Create(GameServer server)
        {
            _service.AddServer(server);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.RemoveServerByID(id);
        }
    }
}

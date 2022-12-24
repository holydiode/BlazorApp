using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace BlazorApp.Logic
{
    public class ServerService
    {
        protected BaseContext _context;
        public ServerService()
        {
            _context = new BaseContext();
        }
        public ServerService(BaseContext context)
        {
            _context = context;
        }

        public void AddSerer(GameServer server)
        {
            _context.GameServers.Add(server);
            _context.SaveChanges();
        }

        public void AddCore(Core core)
        {
            _context.Cores.Add(core);
            _context.SaveChanges();
        }

        public GameServer GetSererByID(int id)
        {
            return _context.GameServers.Find(id);
        }

        public Core GetCoreByID(int id)
        {
            return _context.Cores.Find(id);
        }

        public void RemoveSererByID(int id)
        {
            var server = GetSererByID(id);
            if (server is not null)
            {
                _context.GameServers.Remove(server);
                _context.SaveChanges();
            }
        }

        public void RemoveCoreByID(int id)
        {
            var core = GetCoreByID(id);
            if (core is not null)
            {
                _context.Cores.Remove(core);
                _context.SaveChanges();
            }
        }

        public void ChangeSererByID(int id, GameServer new_server)
        {
            var server = GetSererByID(id);
            if (server is not null)
            {
                server.Discrib = new_server.Discrib;
                server.Name = new_server.Name;
                server.Ip = new_server.Ip;
                server.Port = new_server.Port;
                _context.SaveChanges();
            }
        }

        public void ChangeCoreByID(int id, Core new_core)
        {
            var core = GetCoreByID(id);
            if (core is not null)
            {
                core.Name = new_core.Name;
                core.GameServers = new_core.GameServers;
                core.Version = new_core.Version;
                _context.SaveChanges();
            }
        }

        public IEnumerable<GameServer> GetServersList(){
            return _context.GameServers.Include(a => a.Core);
        }
    }
}

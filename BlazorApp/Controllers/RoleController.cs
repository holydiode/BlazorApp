﻿using Microsoft.AspNetCore.Mvc;
using BlazorApp.Logic;
using BlazorApp.Data;
using System.Collections.Generic;

namespace BlazorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly PermissionService _service;
        public RoleController(PermissionService service)
        {
            this._service = service;
        }
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            var roles = _service.GetRoleList();
            return roles;
        }

        [HttpGet("{id}")]
        public Role  Get(int id)
        {
            return _service.GetRoleByID(id);
        }

        [Route("append-permission")]
        [HttpGet]
        public void AppendPermission(int id,Permission permission)
        {
            _service.RoleAppendPermission(id, permission);
        }

        [Route("create")]
        [HttpGet]
        public void Create(Role role)
        {
            _service.AddRole(role);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteRoleByID(id);
        }


        [Route("permission")]
        [HttpGet]
        public IEnumerable<Permission> PermissionList(int id)
        {
            return _service.GetRolePermission(id);
        }

    }








}

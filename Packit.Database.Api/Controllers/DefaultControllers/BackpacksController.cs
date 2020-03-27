﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Authentication;
using Packit.Database.Api.Controllers.Abstractions;
using Packit.Model;

namespace Packit.Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackpacksController : PackitApiController
    {
        public IRelationMapper RelationMapper { get; set; }

        public BackpacksController(PackitContext context, IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor, IRelationMapper relationMapper)
            :base(context, authenticationService, httpContextAccessor)
        {
            RelationMapper = relationMapper;
        }

        // GET: api/Backpacks
        [HttpGet]
        public IEnumerable<Backpack> GetBackpacks()
        {
            return Context.Backpacks;
        }

        // GET: api/Backpacks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await Context.Backpacks.FindAsync(id).ConfigureAwait(false);

            if (backpack == null)
            {
                return NotFound();
            }

            return Ok(backpack);
        }

        // PUT: api/Backpacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBackpack([FromRoute] int id, [FromBody] Backpack backpack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != backpack?.BackpackId)
            {
                return BadRequest();
            }

            Context.Entry(backpack).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BackpackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Backpacks
        [HttpPost]
        public async Task<IActionResult> PostBackpack([FromBody] Backpack backpack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Context.Backpacks.Add(backpack);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetBackpack", new { id = backpack?.BackpackId }, backpack);
        }

        // DELETE: api/Backpacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBackpack([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var backpack = await Context.Backpacks.FindAsync(id).ConfigureAwait(false);
            if (backpack == null)
            {
                return NotFound();
            }

            Context.Backpacks.Remove(backpack);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return Ok(backpack);
        }

        // PUT: api/backpacks/3/items6
        [HttpPut]
        [Route("{backpackId}/items/{itemId}")]
        public async Task<IActionResult> PutItemToBackpack([FromRoute] int backpackId, [FromRoute] int itemId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (RelationMapper.ObjRelationExists(itemId, backpackId, Context.ItemBackpack))
                return BadRequest();

            var itemBackpack = (ItemBackpack)RelationMapper.CreateManyToMany<ItemBackpack>(itemId, backpackId);

            await Context.ItemBackpack.AddAsync(itemBackpack).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetItemBackpack", new { itemId, backpackId }, itemBackpack);
        }

        // GET: api/backpacks/5/items
        [HttpGet] //TODO: Fix route
        [Route("{backpackId}/items")]
        public  IEnumerable<Item> GetItemsInBackpack([FromRoute] int backpackId)
        {
            var items = Context.Items.Include(i => i).Where(i => i.ItemId == backpackId);

            return items;
        }

        private bool BackpackExists(int id)
        {
            return Context.Backpacks.Any(e => e.BackpackId == id);
        }
    }
}
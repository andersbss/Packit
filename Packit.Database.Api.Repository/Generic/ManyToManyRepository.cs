﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packit.DataAccess;
using Packit.Database.Api.Repository.Interfaces;
using Packit.Model;
using Packit.Model.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Packit.Database.Api.Repository.Generic
{
    public class ManyToManyRepository<T1, T2, T3> : GenericRepository<T1>, IManyToManyRepository<T1> where T1 : class, IDatabase 
                                                                                              where T2 : class, IDatabase 
                                                                                              where T3 : class, IManyToMany
    {
        public ManyToManyRepository(PackitContext context)
            :base(context)
        {
        }

        public async Task<IActionResult> CreateManyToManyAsync(string message, int leftId, int rightId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await EntityExistsAsync<T2>(leftId) || !await EntityExistsAsync<T1>(rightId))
                return NotFound();

            if (EntityRelationExists(leftId, rightId))
                return NoContent();

            var entity = (T3)Activator.CreateInstance(typeof(T3));
            entity.SetLeftId(leftId);
            entity.SetRightId(rightId);

            await Context.Set<T3>().AddAsync(entity);

            await SaveChangesAsync();

            return CreatedAtAction(message, new { leftId, rightId }, entity);
        }

        public async Task<IActionResult> DeleteManyToManyAsync(int leftId, int rightId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await EntityExistsAsync<T2>(leftId) || !await EntityExistsAsync<T1>(rightId))
                return NotFound();

            if (!EntityRelationExists(leftId, rightId))
                return NoContent();

            var entity = await Context.Set<T3>().FirstOrDefaultAsync(e => e.GetLeftId() == leftId && e.GetRightId() == rightId);
            Context.Set<T3>().Remove(entity);

            await SaveChangesAsync();

            return Ok(entity);
        }

        public async Task<IActionResult> GetManyToManyAsync(int id)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var entity = await Context.Set<T1>().FindAsync(id);

            if (entity == null) 
                return NotFound();

            var manyEntities = Context.Set<T3>().Where(e => e.GetRightId() == id);

            var entities = Context.Set<T2>()
                            .Where(e => manyEntities
                            .Any(e2 => e2.GetLeftId() == e.GetId()));

            return Ok(entities);
        }

        protected async Task<bool> EntityExistsAsync<T>(int id) where T : class, IDatabase => await Context.Set<T>().AnyAsync(e => e.GetId() == id);

        private bool EntityRelationExists(int leftId, int rightId) => Context.Set<T3>().Any(e => e.GetLeftId() == leftId && e.GetRightId() == rightId);
    }
}

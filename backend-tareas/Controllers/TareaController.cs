﻿using backend_tareas.Context;
using backend_tareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace backend_tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public TareaController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try 
            {
                var listareas = await _context.Tareas.ToListAsync();
                return Ok(listareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] Tarea tarea)
        {
            try
            {
                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();
                return Ok(new {message = "El envío ha sido exitoso"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Tarea tarea)
        {
            try
            {
                if(id != tarea.Id)
                {
                    return NotFound();
                }
                tarea.Estado = !tarea.Estado;
                _context.Entry(tarea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new { message = "La actualización ha sido exitosa" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);
                if(tarea == null)
                {
                    return NotFound();
                }
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
                return Ok(new { message = "La tarea fue eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    

    public class async
    {
    }
}

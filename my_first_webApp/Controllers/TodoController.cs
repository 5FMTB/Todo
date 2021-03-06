﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_first_webApp.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_first_webApp.Controllers
{
    [Route("api/Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
            
            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "gras maaien", IsComplete = true });
                _context.TodoItems.Add(new TodoItem { Name = "Fietsband repareren" });
                _context.TodoItems.Add(new TodoItem { Name = "API programeren" });
                _context.TodoItems.Add(new TodoItem { Name = "water bijvullen", IsComplete = true });
                _context.TodoItems.Add(new TodoItem { Name = "tandarts" });
                _context.TodoItems.Add(new TodoItem { Name = "API database" });
                _context.SaveChanges();
            }
        }

        //return new JsonResult(book) //hoe een json object terug te geven
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            try
            {
                _context.TodoItems.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        /*
        [HttpDelete("/Clear")]
        public async Task<IActionResult> ClearTodoItem()
        {
            var todoItem = await _context.TodoItems.FindAsync(todoitem => todoitem.Id == 1);
            return true;


        }
        */
        /*
        //delete range (BETA: does not remove last element of the range
        [HttpDelete("{startID}-{stopID}")]
        public async Task<IActionResult> DeleteTodoItems(long startID, long stopID)
        {
            long ID = startID;
            while(ID < stopID)
            {
                
                var todoItem = await _context.TodoItems.FindAsync(ID);

                if (todoItem != null)
                {
                    _context.TodoItems.Remove(todoItem);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                ID++;
            }
            return Ok();
        }
        */
    }
}

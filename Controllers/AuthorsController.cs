using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fisher.Bookstore.Api.Models;

namespace Fisher.Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly BookstoreContext db;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.Authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetById(int id)
        {
            var temp = db.Authors.Find(id);

            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
        }

        [HttpPost]
        public IActionResult Post ([FromBody]Author temp)
        {
            if (temp == null)
            {
                return BadRequest();
            }

            this.db.Authors.Add(temp);
            this.db.SaveChanges();

            return CreatedAtRoute("GetAuthor", new {id = temp.Id}, temp);
        }

        [HttpPut("{id}")]
        public IActionResult Put (int id, [FromBody]Author newAuthor)
        {
            if (newAuthor == null || newAuthor.Id != id)
            {
                return BadRequest();
            }

            var currentAuthor = this.db.Authors.FirstOrDefault(x => x.Id == id);

            if (currentAuthor == null)
            {
                return NotFound();
            }

            currentAuthor.Name = newAuthor.Name;

            this.db.Authors.Update(currentAuthor);
            this.db.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var temp = this.db.Authors.FirstOrDefault(x => x.Id == id);

            if (temp == null)
            {
                return NotFound();
            }

            this.db.Authors.Remove(temp);
            this.db.SaveChanges();

            return NoContent();
        }

        public AuthorsController(BookstoreContext db)
        {
            this.db = db;

            if (this.db.Authors.Count() == 0)
            {

                this.db.Authors.Add(new Author 
                {
                    Id = 1,
                    Name = "Chad Thomas"
                });

                this.db.Authors.Add(new Author 
                {
                    Id = 2,
                    Name = "Shakespeare"
                });

                this.db.SaveChanges();
            }
        }
    }
}

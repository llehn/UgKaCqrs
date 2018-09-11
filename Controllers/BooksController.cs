using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Mvc;

namespace UgKaCqrs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ActorService actorService;
        private readonly BookQueryService bookQueryService;

        public BooksController(ActorService actorService,
            BookQueryService bookQueryService)
        {
            this.actorService = actorService;
            this.bookQueryService = bookQueryService;
        }

        [HttpGet]
        public IEnumerable<BookQueryModel> Index() => bookQueryService.Books;

        [HttpPost("[action]")]
        public void AddBook([FromBody] AddBookCommand command) 
            => actorService.ActorRef.Tell(command);
    }
}

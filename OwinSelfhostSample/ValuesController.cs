using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Web.Http;
using Model;
using MongoDB;

namespace OwinSelfhostSample
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var dao = Factory.Create<Book>();
                return Ok(await dao.GetAll());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/values/5 
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var dao = Factory.Create<Book>();
                return Ok(await dao.Get(id));
            }
            catch (AggregateException)
            {
                return Content(HttpStatusCode.BadRequest, "id not found");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST api/values 
        public IHttpActionResult Post([FromBody] Book book)
        {
            try
            {
                var dao = Factory.Create<Book>();
                dao.Add(book);
                return Created("", book);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // PUT api/values/5 
        public IHttpActionResult Put(string id, [FromBody] Book book)
        {
            try
            {
                var dao = Factory.Create<Book>();
                dao.Update(id, book);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE api/values/5 
        public IHttpActionResult Delete(string id)
        {
            try
            {
                var dao = Factory.Create<Book>();
                dao.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
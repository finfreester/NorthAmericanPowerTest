using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NorthAmericanPower.Data;
using NorthAmericanPower.Domain;

namespace NorthAmericanPower.Web.Services.Controllers
{
    public class AccountController : ApiController
    {

        IAccountRepository _repository;

        public AccountController(IAccountRepository AccountRepo)
        {
            _repository = AccountRepo;
        }

        // GET: api/Account
        public IEnumerable<UserAccount> Get()
        {
            return _repository.GetAll();
        }

        // GET: api/Account/5
        public IHttpActionResult Get(int id)
        {
            var account = _repository.GetUserAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST: api/Account
        public HttpResponseMessage Post([FromBody]UserAccount value)
        {

            var account = _repository.Add(value);
            return this.Request.CreateResponse(HttpStatusCode.Created, account);

        }

        // PUT: api/Account/1
        public IHttpActionResult Put(int id, [FromBody]UserAccount value)
        {

            if (!_repository.Update(value))
            {
                return NotFound();
            }

            return Ok(value);

        }

        // DELETE: api/Account/5
        public IHttpActionResult Delete(int id)
        {

            _repository.Remove(id);
            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}

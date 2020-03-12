//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Dtos;
using RESTAPI.Entities;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    //Controller for logging in and out
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //Get list of all accounts or sessions in database
        [HttpGet]
        public IActionResult GetAllSessions()
        {
            var allAccounts = _accountRepository.GetAll().ToList();

            var allAccountsDto = allAccounts.Select(x => Mapper.Map<AccountDto>(x));

            return Ok(allAccountsDto);
        }

        //return info about specific account based on id
        [HttpGet]
        [Route("{id}", Name = "GetSingleSession")]
        public IActionResult GetSingleSession(Guid ID)
        {
            var accountRepo = _accountRepository.GetSingle(ID);

            if (accountRepo == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AccountDto>(accountRepo));
        }

        // api/login
        [HttpPost]
        public IActionResult Login([FromBody] AccountDto accountDto)
        {
            Account toAdd = Mapper.Map<Account>(accountDto);

            _accountRepository.Add(toAdd);

            bool result = _accountRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingleSession", new { id = toAdd.ID }, Mapper.Map<AccountDto>(toAdd));
        }

        // Logout 
        //Log out of database given id of logged in user
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Logout(Guid ID)
        {
            var repoSession = _accountRepository.GetSingle(ID);

            if (repoSession == null)
            {
                return NotFound();
            }

            _accountRepository.Delete(ID);

            bool result = _accountRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

    }
}
using AutoMapper;
using backend.Core.Models;
using backend.Core.Repositories;
using backend.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AccountsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Returns the account data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Account</returns>

        [HttpGet, Route("{account_id}")]
        public async Task<IActionResult> GetById([FromRoute] string account_id)
        {
            if(!Guid.TryParse(account_id, out Guid id))
                return BadRequest("Account not found.");

            var result = await unitOfWork.AccountRepo.GetAsync(id);
            if(result == null)
                return BadRequest("Account not found.");
            
            int AccountBalance = 0;
            var resultTransactions = await unitOfWork.TransactionRepo.FindAsync(p => p.Account.Id == id);
            resultTransactions.ToList().ForEach(transaction => { AccountBalance += transaction.Amount; });

            AccountDto accountDto = new AccountDto()
            {
                Id = id,
                Balance = AccountBalance
            };

            return Ok(accountDto);
        }
    }
}

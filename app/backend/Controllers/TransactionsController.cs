using AutoMapper;
using backend.Core.Models;
using backend.Core.Repositories;
using backend.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TransactionsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Transaction</returns>
        [HttpPost, ActionName("insert")]
        public async Task<IActionResult>? InsertAsync([FromBody] TransactionForCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Mandatory body parameters missing or have incorrect type.");
                return BadRequest(ModelState);
            }
           
            var account = await unitOfWork.AccountRepo.GetAsync(Guid.Parse(model.AccountId));

            var newTransaction = mapper.Map<Transaction>(model);
            newTransaction.Added = DateTime.Now;

            if (account == null)
            {
                //Create new account
                unitOfWork.AccountRepo.Add(new Account() {
                    Id = newTransaction.AccountId,
                    Added=DateTime.Now,
                    Balance = model.Amount,
                    Transactions = new List<Transaction>() { newTransaction }
                });
            }
            else
            {
                //Update balance for account
                account.Balance += model.Amount;
                unitOfWork.TransactionRepo.Add(newTransaction);
            }

            var result = await unitOfWork.CompleteAsync();

            return result > 0
            ? CreatedAtAction(nameof(GetById), new { transaction_id = newTransaction.Id }, mapper.Map<TransactionDto>(newTransaction))
            : BadRequest();
        }

        /// <summary>
        /// Returns the transaction by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction</returns>

        [HttpGet, Route("{transaction_id}"), ActionName("GetById")]
        public async Task<IActionResult> GetById([FromRoute] string transaction_id)
        {
            if (!Guid.TryParse(transaction_id, out Guid id))
                return BadRequest(new { description = "transaction_id missing or has incorrect type." });

            var result = await unitOfWork.TransactionRepo.GetAsync(Guid.Parse(transaction_id));
            if(result == null)
                return NotFound(new { description = "Transaction not found." });

            return Ok(mapper.Map<TransactionDto>(result));
        }

        /// <summary>
        /// Returns all previously created transactions.
        /// </summary>
        /// <returns>ArrayOfTransactions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>?> GetAllAsync()
        {
            var getResult = await unitOfWork.TransactionRepo.GetAllAsync();

            return Ok(mapper.Map<IEnumerable<TransactionDto>>(getResult));
        }
    }
}

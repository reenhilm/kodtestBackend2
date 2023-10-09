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
            if (unitOfWork is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Mandatory body parameters missing or have incorrect type.");
                return BadRequest(ModelState);
            }

            var account = await unitOfWork.AccountRepo.GetAsync(model.AccountId);
            if (account == null)
            {
                //Create new account
                unitOfWork.AccountRepo.Add(new Account() { Id = new Guid(), Added=DateTime.Now, Balance = model.Amount });
            }
            else
            {
                //Update balance for account
                account.Balance += model.Amount;
            }

            //TODO possible to use shadow property instead
            var newTransaction = mapper.Map<Transaction>(model);
            newTransaction.Added = DateTime.Now;

            unitOfWork.TransactionRepo.Add(newTransaction);
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
                return BadRequest("Transaction not found.");

            return Ok(mapper.Map<TransactionDto>(await unitOfWork.TransactionRepo.GetAsync(Guid.Parse(transaction_id))));
        }

        /// <summary>
        /// Returns all previously created transactions.
        /// </summary>
        /// <returns>ArrayOfTransactions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>?> GetAllAsync()
        {
            if (unitOfWork is null) return BadRequest();

            var getResult = await unitOfWork.TransactionRepo.GetAllAsync();

            return Ok(mapper.Map<IEnumerable<TransactionDto>>(getResult));
        }
    }
}

using AutoMapper;
using backend.Core.Models;
using backend.Core.Repositories;
using backend.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("transactions/[controller]")]
    public class TransactionsController : ControllerBase
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

            var getResult = await GetById(model.Id);
            if (getResult is not null)
            {
                ModelState.AddModelError("Name", $"Transaction {model.Id} already exist");
                return BadRequest();
            }

            unitOfWork.TransactionRepo.Add(mapper.Map<Transaction>(model));
            var result = await unitOfWork.CompleteAsync();

            return result > 0
            ? CreatedAtAction("Get", new { id = model.Id }, model)
            : BadRequest();
        }

        /// <summary>
        /// Returns the transaction by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Transaction</returns>

        [HttpGet, Route("transactions/{transaction_id}")]
        private async Task<TransactionDto?> GetById(System.Guid id) =>
                    mapper.Map<TransactionDto>(await unitOfWork.TransactionRepo.GetAsync(id));

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

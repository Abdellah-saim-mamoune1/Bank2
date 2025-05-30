using bankApI.BusinessLayer.Dto_s.ClientDto_s.DTransactionsHistory;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace bankApI.Controllers.ClientController.Transactions
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsHistoryController : ControllerBase
    {

        ITransactionsHistory _TransactionHistory;
       public TransactionsHistoryController(ITransactionsHistory t)
        {
            _TransactionHistory = t;
        }

        [HttpGet("GetTransactionHistoryById{AccountId}")]

        public async Task<ActionResult<IEnumerable<DGetTransactionsHistory>>> GetTransactionHistoryById(string AccountId)
        {
            var TH = await _TransactionHistory.getTransactionsHistoryAsync(AccountId);

            if (TH == null)
                return NotFound();

            return Ok(TH);
        }

        [HttpPut("TransferFund/")]
        async public Task<ActionResult<string>> TransferFundAsync(DSetTransferFund infos)
        {
            var result = await _TransactionHistory.TransferFundAsync(infos);
            if (!result)
            {
return BadRequest("Requist Not Valid ");
            }

            else
                return Ok("Valid Requist");
        }
    }
}

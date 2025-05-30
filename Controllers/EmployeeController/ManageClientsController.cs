using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.EmployeeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageClientsController : ControllerBase
    {

        IClientsManagement _ManageClientService;


        public ManageClientsController(IClientsManagement ManageClientService)
        {
            _ManageClientService = ManageClientService;

        }

        [HttpPost("AddNewClient")]
        public async Task<ActionResult> AddNewClientAsync([FromBody] DPersonClientS client)
        {
            bool check = await _ManageClientService.AddNewClientAsync(client);
            if (check)
                return Ok();
            else
                return BadRequest();

        }



        [HttpPost("AddClientNotification")]
        async public Task<ActionResult> AddClientNotification(DCNotifications n)
        {
            bool result = await _ManageClientService.SendClientAccountMessage(n);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Deposit")]
        public async Task<ActionResult> Deposit(DDepositWithdraw depositinfos)
        {
            bool result = await _ManageClientService.Deposit(depositinfos);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult> Withdraw(DDepositWithdraw withdrawinfos)
        {
            bool result = await _ManageClientService.Withdraw(withdrawinfos);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}

using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Services.SClient.IClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace bankApI.Controllers.ClientController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        DbContext _db;
        IClientService _ClientService;

        public ClientController(IClientService ClientService)
        {
            _ClientService = ClientService;
            
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<DPersonClientG>>> GetAllClientsAsync()
        {

            var result = await _ClientService.GetAllClientsAsync();
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("GetClientById{id}")]
        public async Task<ActionResult<DPersonClientG>> GetClientByIdAsync(int id)
        {
            var result = await _ClientService.GetClientByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("UpdateClient/")]
        public async Task<ActionResult> UpdateClientByIdAsync( [FromBody] DUpdateClient client)
        {
            bool a = await _ClientService.UpdateClientByIdAsync( client);
            if (a)
            {
                return Ok();
            }

            else
                return NotFound();

        }
     

        [HttpPost("AddAccount")]
        public async Task<ActionResult<DAccountGet?>> AddNewAccountAsync([FromBody] DBankEmail email)
        {
            var check = await _ClientService.AddNewAccountAsync(email);
            if (check==null)
                return BadRequest();
           
            else
                return Ok(check);

        }

        [HttpPost("SendMessageToClientAccount")]
        public async Task<ActionResult> SendMessageToClientAccount([FromBody] DBankEmail email)
        {
            var check = await _ClientService.AddNewAccountAsync(email);
            if (check!=null)
                return Ok();
            else
                return BadRequest();

        }



        [HttpGet("GetAllClientsAccounts")]
        public async Task<ActionResult<IEnumerable<DGetEmails>>> GetAllAccountsAsync()
        {

            var result = await _ClientService.GetAllClientsAccountsAsync();
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

      


    

        [HttpPut("FreezeUnfreezeClientAccount")]
        public async Task<ActionResult> FreezeUnfreezeClientAccountAsync(DSetEmailState state)
        {
            bool result = await _ClientService.FreezeClientAccountAsync(state);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("AddGetHelpRequist/")]
        async public Task<ActionResult> AddGetHelpRequistAsync(DCNotifications state)
        {
            bool response = await _ClientService.AddGetHelpRequistAsync(state);

            if (response)
                return Ok("Requist Added Successfuly");

            return BadRequest("Unvalid Requist");
        }



        [Authorize]
        [HttpGet("GetClientInfo")]
        public async Task<ActionResult<DPersonClientG>> GetClientInfo()
        {
            var clientId = User.FindFirst(ClaimTypes.Email)?.Value;

            if (clientId == null)
                return Unauthorized();

            var clientinfos = await _ClientService.GetClientInfo(clientId);
            if (clientinfos == null)
                return NotFound();

            return Ok(clientinfos);
        }


    }
}
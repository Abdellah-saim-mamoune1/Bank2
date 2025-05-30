using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.ClientController.NotificationsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CNotificationsController : ControllerBase
    {
        private  IClientNotifications _CNotifications { get; set; }

       public CNotificationsController(IClientNotifications CNotifications)
        {
           _CNotifications = CNotifications;   
        }

        [HttpGet("GetClientNotifications/{AccountId}")]

        public async Task<ActionResult<Stack<DCNotificationsGet>>> GetClientNotifications(string AccountId)
        {
            Stack<DCNotificationsGet> ntf = new Stack<DCNotificationsGet>(await _CNotifications.GetNotifications(AccountId));
            if (ntf == null)
                return NotFound();

            return Ok(ntf);

        }

      
        [HttpPut("UpdateIsNotificationviewed/{Notificationid}")]
        public async Task<ActionResult> SetIsviewed(int Notificationid)
        {
            bool check = await _CNotifications.SetIsviewed(Notificationid);
            if (check == false)
                return BadRequest();

            return Ok();

        }

    }
}

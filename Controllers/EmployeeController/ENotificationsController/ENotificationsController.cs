using bankApI.BusinessLayer.Dto_s.ClientDto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.BusinessLayer.Services.ClientServer.IClient;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using Microsoft.AspNetCore.Mvc;

namespace bankApI.Controllers.EmployeeController.ENotificationsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ENotificationsController : ControllerBase
    {
        private IENotifications _ENotifications { get; set; }

        public ENotificationsController(IENotifications ENotifications)
        {
            _ENotifications = ENotifications;
        }

        [HttpGet("GetEmployeeNotifications/{AccountId}")]

        public async Task<ActionResult<Stack<DCNotificationsGet>>> GetClientNotifications(string AccountId)
        {
            Stack<DCNotificationsGet> ntf = new Stack<DCNotificationsGet>(await _ENotifications.GetNotifications(AccountId));
            if (ntf == null)
                return NotFound();

            return Ok(ntf);

        }





        [HttpPut("UpdateIsEmployeeNotificationviewed/")]
        public async Task<ActionResult> SetIsviewed(DSetIsNotificationV info)
        {
            bool check = await _ENotifications.ESetIsviewed(info);
            if (check == false)
                return BadRequest();

            return Ok();

        }

    }
}

using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.EmployeeDto_s;
using bankApI.BusinessLayer.Services.EmployeeServer.IEmployee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace bankApI.Controllers.EmployeeController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

      //  [Authorize]
        [HttpPost("AddNewEmployee/")]
        public async Task<ActionResult<DGetEmployee>> AddEmployeeAsync(DEmployeePerson employee)
        {
        
            var emp = await _EmployeeService.AddNewEmployeeAsync(employee);
            if (emp == null)
                return BadRequest();

            return Ok(emp);
        }

        [Authorize]
        [HttpGet("GetCardsInfo")]
        public async Task<ActionResult<DGetCardsInfo>> GetCardsInfoAsync()
        {
            var infos= await _EmployeeService.GetCardsInfoAsync();
            if(infos == null)
                return NotFound();

            return Ok(infos);
        }

      

        [Authorize]
        [HttpGet("EGetAllTransactions")]
        public async Task<ActionResult<DGetResentTransactions>> GetAllTransactionsAsync()
        {
            DGetResentTransactions transactions = await _EmployeeService.GetAllTransactionsAsync();

           
            if (transactions == null)
                return NotFound();

            return Ok(transactions);
        }


        [Authorize]
        [HttpGet("GetEmployeeInfo")]
        public async Task<ActionResult<DGetEmployeeInfos>> GetEmployeeInfo()
        {
            var employeeId = User.FindFirst(ClaimTypes.Email)?.Value;

            if (employeeId == null)
                return Unauthorized();

            var employeeinfos = await _EmployeeService.GetEmployeeInfo(employeeId);


            if (employeeinfos == null)
                return NotFound();

            return Ok(employeeinfos);
        }

      
        [HttpGet("GetAllEmployeesInfos")]
        public async Task<ActionResult<IEnumerable<DGetEmployeeInfos?>>> GetAllEmployeesInfos()
        {
           // var employeeId = User.FindFirst(ClaimTypes.Email)?.Value;
           // if (employeeId == null)
                //return Unauthorized();

            var employeeinfos = await _EmployeeService.GetAllEmployeesInfos();


            if (employeeinfos == null)
                return NotFound();

            return Ok(employeeinfos);
        }

        [HttpPut("UpdateEmployee/")]
        public async Task<ActionResult> UpdateClientByIdAsync( DUpdateEmployee EmployeeInfos)
        {
            bool a = await _EmployeeService.UpdateEmployeeInfos(EmployeeInfos);
            if (a)
            {
                return Ok();
            }

            else
                return BadRequest();

        }


        [HttpPut("FreezeUnfreezeEmployeeAccount/")]
        public async Task<ActionResult> FreezeUnfreezeEmployeeAccountAsync(DSetEmailState state)
        {
            bool result = await _EmployeeService.FreezeUnfreezeEmployeeAccountAsync(state);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("SendMessageToEmployee")]
        public async Task<ActionResult> SendMessageToEmployee([FromBody] DENotifications email)
        {
            bool check = await _EmployeeService.SendEmployeeMessage(email);
            if (check)
                return Ok();
            else
                return BadRequest();

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using StripeCheck.Application;
using StripeCheck.Application.Admin;
using StripeCheck.Application.DBOperation;
using StripeCheck.Application.Platform;
using StripeCheck.Application.ServiceProvider;


namespace StripeCheck.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISPOperation _ISPOperation;
        private readonly IPlatformOperation _IPlatformOperation;
        private readonly IAdminOperation _IAdminOperation;
        private readonly DBOperations _DBOperation;

        public PaymentController(
            ISPOperation _ISPOperation,
            IPlatformOperation _IPlatformOperation,
            IAdminOperation _IAdminOperation,
            DBOperations _DBOperation
            )
        {
            this._ISPOperation = _ISPOperation;
            this._IPlatformOperation = _IPlatformOperation;
            this._IAdminOperation = _IAdminOperation;
            this._DBOperation = _DBOperation;
        }

        [HttpGet("GetAllConnectedAccounts")]
        public ActionResult<List<String>> GetAllConnectedAccounts()
        {
            return Ok(_DBOperation.getData());
        }
        [HttpPost("PayToServiceProvider")]
        public ActionResult<PaymentResponse> PayToServiceProvider([FromBody] PaymentRequest req)
        {
            return Ok(_ISPOperation.PayToServiceProvider(req.Amount,req.Referral));
        }
        [HttpPost("PayToPlatform")]
        public ActionResult<PaymentResponse> PayToPlatform([FromBody] PaymentRequest req)
        {
            return Ok(_IPlatformOperation.PayToPlatform(req.Amount, req.Referral));
        }
        [HttpPost("Onboard")]
        public ActionResult<PaymentResponse> ServiceProviderOnboard([FromBody] OnboardRequest req)
        {
            return Ok(_IAdminOperation.OnBoardServiceProvider(req.FirstName,req.LastName,req.Email, req.Referral));
        }


    }
}

using eClaim.Entities;
using eClaim.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace eClaim.Web.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/claim")]
    public class ClaimController : ApiController
    {
        [HttpPost]
        [Route("GetClaimList")]
        public List<Claim> GetClaimList([FromBody] GetClaimListRequest request)
        {
            var controller = new ClaimProcess();

            try
            {

               return controller.GetClaimList(request);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("GetClaim")]
        public Claim GetClaim([FromBody] Claim claim)
        {
            var controller = new ClaimProcess();

            try
            {

                return controller.GetClaim(claim);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("SaveDraft")]
        public Claim SaveDraft([FromBody] Claim claim)
        {
            var controller = new ClaimProcess();

            try
            {
                return controller.SaveDraft(claim);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("SubmitClaim")]
        public Claim SubmitClaim([FromBody] Claim claim)
        {
            var controller = new ClaimProcess();

            try
            {
                return controller.SubmitClaim(claim);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }
        [HttpPost]
        [Route("ApproveRejectClaim")]
        public void ApproveRejectClaim([FromBody] Claim claim)
        {
            var controller = new ClaimProcess();

            try
            {
           
                 controller.ApproveRejectClaim(claim);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("GetCurrencyList")]
        public List<Currency> GetCurrencyList()
        {
            var controller = new ClaimProcess();

            try
            {
                return controller.GetCurrencyList();
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }
        [HttpPost]
        [Route("checkrate")]
        public double CheckRate([FromBody] ClaimDetail claimDetail)
        {
            var controller = new ClaimProcess();

            try
            {
              
              return  controller.CheckRate(claimDetail);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("ValidateClaimDetail")]
        public void ValidateClaimDetail([FromBody] ClaimDetail claimdetail)
        {
            var controller = new ClaimProcess();

            try
            {
                controller.ValidateClaimDetail(claimdetail);
            }
            catch (HttpResponseException ex)
            {
                var httpError = new HttpResponseMessage()
                {
                    Content = new StringContent(ex.Response.ReasonPhrase),
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Response.ReasonPhrase
                };


                throw new HttpResponseException(httpError);
            }
        }


    }
}
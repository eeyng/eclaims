using eClaim.Business;
using eClaim.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eClaim.Services.Http
{
    [RoutePrefix("api/claim")]
    public class ClaimService : ApiController
    {
        [HttpPost]
        [Route("SaveDraft")]
        public Claim CreateClaim(Claim claim)
        {
            try
            {
                var bc = new ClaimComponent();
                return bc.SaveDraft(claim);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }


        [HttpPost]
        [Route("SubmitClaim")]
        public Claim SubmitClaim(Claim claim)
        {
            try
            {
                var bc = new ClaimComponent();
                return bc.SubmitClaim(claim);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }


        [HttpPost]
        [Route("ValidateClaimDetail")]
        public void ValidateClaimDetail(ClaimDetail claimdetail)
        {
            try
            {
                var bc = new ClaimComponent();
                bc.ValidateClaimDetail(claimdetail);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("ApproveRejectClaim")]
        public void ApproveRejectClaim(Claim claim)
        {
            try
            {
                var bc = new ClaimComponent();
                bc.ApproveRejectClaim(claim);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }


        [HttpPost]
        [Route("GetClaimList")]
        public List<Claim> GetClaimList(GetClaimListRequest request)
        {
            try
            {
                var bc = new ClaimComponent();
              return   bc.GetClaimList(request.Employee,request.SearchCondition);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("GetClaim")]
        public Claim GetClaim(Claim claim)
        {
            try
            {
                var bc = new ClaimComponent();
                return bc.GetClaim(claim);
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }

        [HttpPost]
        [Route("GetCurrencyList")]
        public List<Currency> GetCurrencyList()
        {
            try
            {
                var bc = new ClaimComponent();
                return bc.GetCurrencyList();
            }
            catch (Exception ex)
            {
                // Repack to Http error.
                var httpError = new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)422,
                    ReasonPhrase = ex.Message
                };

                throw new HttpResponseException(httpError);
            }
        }
        

    }
}

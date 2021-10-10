using eClaim.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace eClaim.Process
{
    public class ClaimProcess : ProcessBase
    {
        public Claim SubmitClaim(Claim claim)
        {
            Claim result = default(Claim);

            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<Claim>(HttpMethod.Post, "api/claim/SubmitClaim", claim)).Result)
            {
                result = base.VerifyStatusCodeAsync<Claim>(response);
            }

            return result;
        }

        public Claim SaveDraft(Claim claim)
        {
            Claim result = default(Claim);
            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<Claim>(HttpMethod.Post, "api/claim/SaveDraft", claim)).Result)
            {
                result = base.VerifyStatusCodeAsync<Claim>(response);
            }

            return result;

        }

        public void ApproveRejectClaim(Claim claim)
        {
            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<Claim>(HttpMethod.Post, "api/claim/ApproveRejectClaim", claim)).Result)
            {
                base.VerifyStatusCode(response);
            }


        }

        public Claim GetClaim(Claim claim)
        {
            Claim result = default(Claim);

            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<Claim>(HttpMethod.Post, "api/claim/GetClaim", claim)).Result)
            {
                result = base.VerifyStatusCodeAsync<Claim>(response);
            }

            return result;
        }

        public List<Claim> GetClaimList(GetClaimListRequest request)
        {
            List<Claim> result = default(List<Claim>);

            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<GetClaimListRequest>(HttpMethod.Post, "api/claim/GetClaimList", request)).Result)
            {
                result = base.VerifyStatusCodeAsync<List<Claim>>(response);
            }

            return result;
        }

        public double CheckRate(ClaimDetail claimDetail)
        {
            double result = 0;

            ExchangeRateReponse exchangeRateReponse = new ExchangeRateReponse();

            if(!string.IsNullOrEmpty(claimDetail.Currency))
            {
                string url = System.Configuration.ConfigurationManager.AppSettings.Get("fixerurl") + "&base=" + claimDetail.Currency + "&symbols=MYR";
                url = url.Replace("{date}", claimDetail.TransactionDate.ToString("yyyy-MM-dd"));
                base.Url = url;

                using (var response = base.HTTPClient.SendAsync(base.InitializeRequest(HttpMethod.Get, "")).Result)
                {

                    exchangeRateReponse = base.VerifyStatusCodeAsync<ExchangeRateReponse>(response);
                    if (exchangeRateReponse.success == true)
                        result = Convert.ToDouble(exchangeRateReponse.rates["MYR"]);
                    else
                    {
                        var httpError = new HttpResponseMessage()
                        {
                            StatusCode = (HttpStatusCode)422,
                            ReasonPhrase = "Exchange rate not available"
                        };

                        throw new HttpResponseException(httpError);
                    }
                }

            }
         

            return result;
        }

        public List<Currency> GetCurrencyList()
        {
            List<Currency> result = default(List<Currency>);
           

            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest(HttpMethod.Post, "api/claim/GetCurrencyList")).Result)
            {
                result = base.VerifyStatusCodeAsync<List<Currency>>(response);
            }

            return result;
        }

        public void ValidateClaimDetail(ClaimDetail claimdetail)
        {
            

            using (var response = base.HTTPClient.SendAsync(base.InitializeRequest<ClaimDetail>(HttpMethod.Post, "api/claim/ValidateClaimDetail",claimdetail)).Result)
            {
                base.VerifyStatusCode(response);
            }
        }
    }
}

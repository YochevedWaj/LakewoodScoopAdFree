using LakewoodScooper.Scraping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakewoodScooper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TLSController : ControllerBase
    {
        [HttpGet]
        [Route("scrape")]
        public List<TLSPost> Scrape()
        {
            return TLSScraper.Scrape();
        }
    }
}

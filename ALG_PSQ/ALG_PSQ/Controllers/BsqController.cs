using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ALG_PSQ.Bsq;

namespace ALG_PSQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BsqController : ControllerBase
    {
        // POST api/bsq
        [HttpPost]
        public string Post([FromBody] string postData)
        {
            //Console.WriteLine("\n\n" + postData + "\n\n");

            AlgoBsq Bsq = new AlgoBsq();
            Bsq.run(postData);

            return JsonConvert.SerializeObject(Bsq);
        }
    }
}

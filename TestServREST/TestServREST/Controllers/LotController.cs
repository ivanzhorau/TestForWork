using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestServREST.ModelAndJSON;

namespace TestServREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotController : ControllerBase
    {
        private string JSONPath = "D:\\00Sprobniki\\TestServREST\\TestServREST\\ModelAndJSON\\lots.json";
        private static readonly string[] Titles = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static readonly string[] ImagePaths = new[]
        {
            "https://sun9-83.userapi.com/impg/3xckxzho4C2ZaKsCopmdeEhR_zfqfNpZPHHdXA/r6PKzC1QLXs.jpg?size=688x784&quality=96&sign=30ec8cf13bf1980291c855b02f0ab3ab&type=album",
            "https://sun9-14.userapi.com/impg/vWzjfl6n55vRVr6QTLA1pJ8a5iXTc-3jCyfjfQ/_UjQDRnztIw.jpg?size=700x688&quality=96&sign=7869f6af999db13192f08d3f13753ae6&type=album",
            "https://sun9-54.userapi.com/impg/HPaMiaM8nCNVq4Pgg60DfJBZYgb8bfGQoEk0xw/Sukb-JplYq4.jpg?size=1028x579&quality=96&sign=c95275d947e6d1ae959d962663d44cfb&type=album"
        };

        private readonly ILogger<LotController> _logger;

        public LotController(ILogger<LotController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Lot> Get()    //get list
        {
            Lot[] lots = DataWorkJSON.ReadJSON(JSONPath);
            return lots;
        }

        [HttpPost]
        public async Task<ActionResult<Lot>> Post(Lot lot) //add new value
        {
            if (lot == null)
            {
                return BadRequest();
            }

            await Task.Run(()=>DataWorkJSON.Add(JSONPath, lot));
            return Ok(lot);
        }
        
        [HttpPut]
        public async Task<ActionResult<Lot>> Put(Lot lot) //change value
        {
            if (lot == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await Task.Run(() => DataWorkJSON.Change(JSONPath, lot));
            return Ok(lot);
        }

        [HttpDelete]
        public async Task<ActionResult<Lot>> Delete(Lot lot) //delete value
        {
            if (lot == null)
            {
                return BadRequest();
            }
            if (lot.Id == 0) {
                ModelState.AddModelError("ID", "ID не может быть нулевым");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await Task.Run(() => DataWorkJSON.Delete(JSONPath, lot));
            return Ok(lot);
        }
    }
}

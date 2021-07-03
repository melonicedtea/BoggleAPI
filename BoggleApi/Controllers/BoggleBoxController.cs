using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BoggleApi.Models;
using BoggleApi.Services;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoggleApi.Controllers
{
    [Route("api/boggle")]
    [ApiController]
    public class BoggleBoxController : ControllerBase
    {

        [HttpGet("getbogglebox")]
        public BoggleBox GetBoggleBox() => BoggleBoxService.GetBoggleBox();

        [HttpGet("getbogglebox/{boggleBoxId}")]
        public ActionResult<BoggleBox> GetBoggleBox(Guid boggleBoxId)
        {
            var boggleBox = BoggleBoxService.GetBoggleBox(boggleBoxId);

            if (boggleBox == null)
            {
                return NotFound();
            }

            return boggleBox;
        }

        [HttpGet("getbogglebox/{boggleBoxId}/{word}")]
        public bool IsValidWord(Guid boggleBoxId, string word)
        {
            bool isValidWord = false;

           //check if word is valid
           //set true if valid

            return isValidWord;
        }
    }
}

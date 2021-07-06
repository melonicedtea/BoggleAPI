using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BoggleApi.Models;
using BoggleApi.Services;
using System.Linq;
using Microsoft.Office.Interop.Word;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoggleApi.Controllers
{
    [Route("api/boggle")]
    [ApiController]
    public class BoggleBoxController : ControllerBase
    {
        private readonly IBoggleBoxService _boggleBoxService;
        private static Application wordApp;
        private static Language language;
        public BoggleBoxController(IBoggleBoxService boggleBoxService)
        {
            _boggleBoxService = boggleBoxService ?? throw new ArgumentNullException(nameof(boggleBoxService));
            if(wordApp == null)
            {
                wordApp = new Application();
                language = wordApp.Languages[WdLanguageID.wdDutch]; 
            }
            
            
        }

        [HttpGet("getbogglebox")]
        public BoggleBox GetBoggleBox() => _boggleBoxService.GetBoggleBox();

        [HttpGet("getbogglebox/{boggleBoxId}")]
        public ActionResult<BoggleBox> GetBoggleBox(Guid boggleBoxId)
        {
            var boggleBox = _boggleBoxService.GetBoggleBox(boggleBoxId);

            if (boggleBox == null)
            {
                return NotFound();
            }

            return boggleBox;
        }

        [HttpGet("isvalidword/{boggleBoxId}/{word}")]
        public bool IsValidWord(Guid boggleBoxId, string word)
        {
            bool isWordPresent = false;
            bool isValidWord = false;

            var boggleBox = _boggleBoxService.GetBoggleBox(boggleBoxId);
            isWordPresent = _boggleBoxService.CheckWordPresent(boggleBox, word);

            if (word.Length >= 3)
            {
                isValidWord = wordApp.CheckSpelling(word.ToLower(), MainDictionary: language.Name);
            }

            return isWordPresent && isValidWord;
        }

        [HttpGet("scoreword/{word}")]
        public int ScoreWord(string word)
        {
            int score = 0;

            if (word.Length == 3 || word.Length == 4)
            {
                score = 1;
            }
            else if (word.Length == 5)
            {
                score = 2;
            }
            else if (word.Length == 6)
            {
                score = 3;
            }
            else if (word.Length == 7)
            {
                score = 5;
            }
            else if (word.Length >= 8)
            {
                score = 11;
            }

            return score;
        }

    }
}

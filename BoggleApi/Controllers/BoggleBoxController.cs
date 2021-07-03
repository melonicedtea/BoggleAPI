using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BoggleApi.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoggleApi.Controllers
{
    [Route("api/boggle")]
    [ApiController]
    public class BoggleBoxController : ControllerBase
    {
        private static List<BoggleBox> boggleBoxes = new List<BoggleBox>();

        char[,] dice = new char[,] {
                {'R', 'I', 'F', 'O', 'B', 'X'},
                {'I', 'F', 'E', 'H', 'E', 'Y'},
                {'D', 'E', 'N', 'O', 'W', 'S'},
                {'U', 'T', 'O', 'K', 'N', 'D'},
                {'H', 'M', 'S', 'R', 'A', 'O'},
                {'L', 'U', 'P', 'E', 'T', 'S'},
                {'A', 'C', 'I', 'T', 'O', 'A'},
                {'Y', 'L', 'G', 'K', 'U', 'E'},
                {'Q', 'B', 'M', 'J', 'O', 'A'},
                {'E', 'H', 'I', 'S', 'P', 'N'},
                {'V', 'E', 'T', 'I', 'G', 'N'},
                {'B', 'A', 'L', 'I', 'Y', 'T'},
                {'E', 'Z', 'A', 'V', 'N', 'D'},
                {'R', 'A', 'L', 'E', 'S', 'C'},
                {'U', 'W', 'I', 'L', 'R', 'G'},
                {'P', 'A', 'C', 'E', 'M', 'D'}
            };

        [HttpGet("getbogglebox")]
        public BoggleBox GetBoggleBox()
        {
            BoggleBox boggleBox = new BoggleBox(); // make new box

            Guid guid = Guid.NewGuid(); // generate new GUID

            boggleBox.BoggleBoxID = guid; // assign GUID

            List<Die> board = new List<Die>(); // make new list of Die

            for (int i = 0; i < dice.GetLength(0); i++) // foreach die in dice
            {
                Random random = new Random(); // new random
                int r = random.Next(dice.GetLength(1)); // generate random number from 0 to length of dice sides
                char Value = dice[i,r]; // get random value
                Die die = new Die(Value); // make new die with random value
                board.Add(die); // add new die to board
            }

            List<List<Die>> diceList = board.Cast<Die>()
                .Select((x, i) => new { x, index = i / (int)(Math.Sqrt(dice.GetLength(0))) })  // Use overloaded 'Select' and calculate row index.
                .GroupBy(x => x.index)                                   // Group on Row index
                .Select(x => x.Select(s => s.x).ToList())                  // Create List for each group.  
                .ToList();

            boggleBox.Dies = diceList; // assignment

            boggleBoxes.Add(boggleBox); // add bogglebox to list of boggleboxes

            return boggleBox;
        }

        [HttpGet("getbogglebox/{boggleBoxId}")]
        public BoggleBox GetBoggleBox(Guid boggleBoxId)
        {
            BoggleBox boggleBox = null;

            foreach (BoggleBox bb in boggleBoxes)
            {
                if (bb.BoggleBoxID == boggleBoxId)
                {
                    boggleBox = bb;
                    break;
                }
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

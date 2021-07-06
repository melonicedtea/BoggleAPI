using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoggleApi.Models;

namespace BoggleApi.Services
{
    public class BoggleBoxService : IBoggleBoxService
    {
        private static List<BoggleBox> boggleBoxes = new List<BoggleBox>();

        private char[,] dice = new char[,] {
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

        public BoggleBox GetBoggleBox()
        {
            BoggleBox boggleBox = new BoggleBox
            {
                BoggleBoxID = Guid.NewGuid(),
                Dies = CreateRandomDies()
            }; // make new box

            boggleBoxes.Add(boggleBox); // add bogglebox to list of boggleboxes

            return boggleBox;
        }

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

        private List<List<Die>> CreateRandomDies()
        {
            List<List<Die>> diceList = null;

            List<Die> board = new List<Die>(); // make new list of Die

            for (int i = 0; i < dice.GetLength(0); i++) // foreach die in dice
            {
                Random random = new Random(); // new random
                int r = random.Next(dice.GetLength(1)); // generate random number from 0 to length of dice sides
                char Value = dice[i, r]; // get random value
                Die die = new Die(Value); // make new die with random value
                board.Add(die); // add new die to board
            }

            diceList = board.Cast<Die>()
                .Select((x, i) => new { x, index = i / (int)(Math.Sqrt(dice.GetLength(0))) })  // Use overloaded 'Select' and calculate row index.
                .GroupBy(x => x.index)                                   // Group on Row index
                .Select(x => x.Select(s => s.x).ToList())                  // Create List for each group.  
                .ToList();

            return diceList;
        }

        public bool CheckWordPresent(BoggleBox boggleBox, string word)
        {
            bool isWordPresent = false;

            List<List<Die>> dice = boggleBox.Dies;

            string _word = word;

            foreach (List<Die> row in dice)
            {
                foreach (Die die in row)
                {
                    if (_word.Contains(die.Value))
                    {
                        int index = _word.IndexOf(die.Value);
                        _word = (index < 0) ?
                            _word : _word.Remove(index, 1);
                    }

                }
            }


            if (_word.Length == 0)
            {
                isWordPresent = true;
            }

            return isWordPresent;
        }
    }
}

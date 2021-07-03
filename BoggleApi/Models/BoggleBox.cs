using System;
using System.Collections.Generic;

namespace BoggleApi.Models
{
    public class BoggleBox
    {
        public Guid BoggleBoxID { get; set; }
        public List<List<Die>> Dies { get; set; }

    }

    public class Die
    {
        public Die(char Value)
        {
            this.Value = Value;
        }
        public char Value { get; set; }
    }
}

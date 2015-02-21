using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherlockPuzzle.Models
{
    public class Interviewer
    {
        public enum Directions {Clockwise, Anti};

        public int Id { get; set; }

        public string Name { get; set; }

        public int Duration { get; set; }

        public Directions Direction {get; set;}

        public bool IsDog { get; set; }

        public Room CurrentRoom { get; set; }
    }
}

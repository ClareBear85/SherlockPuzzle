using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SherlockPuzzle.Models
{
    public class Room
    {
        public Room(string name)
        {
            this.Name = name;
            Interviewers = new List<Interviewer>();
        }

        public void SetAdjacentRooms(Room previous, Room next)
        {
            PreviousRoom = previous;
            NextRoom = next;
        }

        public string Name { get; set; }

        public Room PreviousRoom { get; set; }

        public Room NextRoom { get; set; }

        public List<Interviewer> Interviewers { get; set; }
    }
}

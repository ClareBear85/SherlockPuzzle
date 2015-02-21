namespace SherlockPuzzle.Models
{
    public class Interviewer
    {
        public enum Directions {Clockwise, Anti}; 

        public string Name { get; set; }

        public int Duration { get; set; }

        public Directions Direction {get; set;}

        public Room CurrentRoom { get; set; }
    }
}

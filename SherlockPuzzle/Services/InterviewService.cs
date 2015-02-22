using System.Collections.Generic;
using SherlockPuzzle.Models;

namespace SherlockPuzzle.Services
{
    public interface IInterviewService
    {
        /// <summary>
        /// Incriments time by 1 minute
        /// Moves interviewers as necessary
        /// Checks pass criteria
        /// </summary>
        void IncrimentInterviews();

        /// <summary>
        /// True if the thief has been found.
        /// </summary>
        bool ThiefFound();

        /// <summary>
        /// Returns found thief.
        /// </summary>
        Room GetThief();
    }

    class InterviewService : IInterviewService
    {
        Interviewer Sherlock;

        Interviewer Watson;

        Interviewer Wellington;

        IEnumerable<Room> Rooms;

        private int CurrentTime;

        Room FoundRoom;

        public InterviewService(Interviewer sh, Interviewer wa, Interviewer we, IEnumerable<Room> rooms)
        {
            Sherlock = sh;

            Watson = wa;

            Wellington = we;

            Rooms = rooms;

            CurrentTime = 0;
        }

        public void IncrimentInterviews()
        {
            CurrentTime++;

            bool isWellingtonMoving = CurrentTime % Wellington.Duration == 0;
            bool isWatsonMoving = CurrentTime % Watson.Duration == 0;
            bool isSherlockMoving = CurrentTime % Sherlock.Duration == 0;

            if (isWellingtonMoving)
            {
                Room newRoom = GetNextRoom(Wellington);
                MoveToNewRoom(Wellington, newRoom);
            }

            if (isWatsonMoving)
            {
                Room newRoom = GetNextRoom(Watson);
                MoveToNewRoom(Watson, newRoom);

                if (newRoom.Interviewers.Contains(Sherlock) && !isSherlockMoving)
                {
                    Room changeRoom = GetNextRoom(Watson);
                    MoveToNewRoom(Watson, changeRoom);
                }
            }

            if (isSherlockMoving)
            {
                Room newRoom = GetNextRoom(Sherlock);
                MoveToNewRoom(Sherlock, newRoom);
                                
                if (newRoom.Interviewers.Contains(Watson) && newRoom.Interviewers.Contains(Wellington))
                {
                    FoundRoom = newRoom;
                }
                else if (newRoom.Interviewers.Contains(Watson))
                {
                    Room changeRoom = GetNextRoom(Sherlock);
                    MoveToNewRoom(Sherlock, changeRoom);
                }
            }
        }

        private Room GetNextRoom(Interviewer interviewer)
        {
            return (interviewer.Direction == Interviewer.Directions.Clockwise) ? interviewer.CurrentRoom.NextRoom : interviewer.CurrentRoom.PreviousRoom;
        }

        private void MoveToNewRoom(Interviewer interviewer, Room newRoom)
        {
            interviewer.CurrentRoom.Interviewers.Remove(interviewer);
            interviewer.CurrentRoom = newRoom;
            newRoom.Interviewers.Add(interviewer);
        }

        public bool ThiefFound()
        {
            return FoundRoom != null;
        }

        public Room GetThief()
        {
            return FoundRoom;
        }
    }
}

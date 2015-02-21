using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SherlockPuzzle.Models;

namespace SherlockPuzzle.Services
{
    public interface IInterviewService
    {
        /// <summary>
        /// Incriments time by 1 minute, and moves any Interviewers as necessary.
        /// </summary>
        void IncrimentInterviews();

        /// <summary>
        /// True if the thief has been found.
        /// </summary>
        bool TheifFound();

        /// <summary>
        /// Returns found theif.
        /// </summary>
        Room GetTheif();
    }

    class InterviewService : IInterviewService
    {
        Interviewer Sherlock;

        Interviewer Watson;

        Interviewer Dog;

        IEnumerable<Room> Rooms;

        private int CurrentTime;

        Room FoundRoom;

        public InterviewService(Interviewer sh, Interviewer wh, Interviewer dog, IEnumerable<Room> rooms)
        {
            Sherlock = sh;

            Watson = wh;

            Dog = dog;

            Rooms = rooms;

            CurrentTime = 0;
        }

        public void IncrimentInterviews()
        {
            CurrentTime++;
            bool isDogMoving = CurrentTime % Dog.Duration == 0;
            bool isWatsonMoving = CurrentTime % Watson.Duration == 0;
            bool isSherlockMoving = CurrentTime % Sherlock.Duration == 0;

            if (isDogMoving)
            {
                Room newRoom = GetNextRoom(Dog);
                MoveToNewRoom(Dog, newRoom);
            }

            if (isWatsonMoving)
            {
                Room newRoom = GetNextRoom(Watson);
                MoveToNewRoom(Watson, newRoom);

                //Move Watson on
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
                                
                if (newRoom.Interviewers.Contains(Watson) && newRoom.Interviewers.Contains(Dog))
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

        public bool TheifFound()
        {
            return FoundRoom != null;
        }

        public Room GetTheif()
        {
            return FoundRoom;
        }
    }
}

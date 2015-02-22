using System;
using System.Collections.Generic;
using System.Linq;
using SherlockPuzzle.Models;
using SherlockPuzzle.Services;

namespace SherlockPuzzle
{
    class Program
    {        
        static void Main(string[] args)
        {
            // configure
            var sherlock = new Interviewer { Name = "Shelock Holmes", Duration = 15, Direction = SherlockPuzzle.Models.Interviewer.Directions.Clockwise };
            var watson = new Interviewer { Name = "Watson", Duration = 20, Direction = SherlockPuzzle.Models.Interviewer.Directions.Anti };
            var wellington = new Interviewer { Name = "Wellington", Duration = 30, Direction = SherlockPuzzle.Models.Interviewer.Directions.Anti };
            var rooms = GetRooms();

            LinkRoomsAndPeople(rooms, sherlock, watson, wellington);

            IInterviewService interviewSerivce = new InterviewService(sherlock, watson, wellington, rooms);

            Room roomFound = null;
            var minutes = 0;

            while (roomFound == null)
            {
                interviewSerivce.IncrementInterviews();

                if (interviewSerivce.ThiefFound())
                    roomFound = interviewSerivce.GetThief();

                minutes++;
            }

            Console.WriteLine("Found " + roomFound.Name + " after " + minutes + " mintues.");
            Console.ReadLine();
        }

        private static void LinkRoomsAndPeople(List<Room> rooms, Interviewer sherlock, Interviewer watson, Interviewer wellington)
        {
            var room1 = rooms.Where(x => x.Name == "Mustard").Single();

            room1.Interviewers.Add(wellington);
            wellington.CurrentRoom = room1;

            room1.Interviewers.Add(sherlock);
            sherlock.CurrentRoom = room1;

            var room6 = rooms.Where(x => x.Name == "White").Single();

            room6.Interviewers.Add(watson);
            watson.CurrentRoom = room6;
        }

        private static List<Room> GetRooms()
        {
            Room room1 = new Room("Mustard");
            Room room2 = new Room("Plum");
            Room room3 = new Room("Green");
            Room room4 = new Room("Peacock");
            Room room5 = new Room("Scarlett");
            Room room6 = new Room("White");

            room1.SetAdjacentRooms(room6, room2);
            room2.SetAdjacentRooms(room1, room3);
            room3.SetAdjacentRooms(room2, room4);
            room4.SetAdjacentRooms(room3, room5);
            room5.SetAdjacentRooms(room4, room6);
            room6.SetAdjacentRooms(room5, room1);

            return new List<Room>()
            {
                room1, room2, room3, room4, room5, room6
            };
        }   
    }
}

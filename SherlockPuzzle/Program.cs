using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SherlockPuzzle.Services;
using SherlockPuzzle.Models;

namespace SherlockPuzzle
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // configure stuff
            var sherlock = new Interviewer { Id = 1, Name = "Shelock Holmes", Duration = 15, Direction = SherlockPuzzle.Models.Interviewer.Directions.Clockwise };
            var watson = new Interviewer { Id = 2, Name = "Watson", Duration = 20, Direction = SherlockPuzzle.Models.Interviewer.Directions.Anti };
            var dog = new Interviewer { Id = 3, Name = "Wellington", Duration = 30, Direction = SherlockPuzzle.Models.Interviewer.Directions.Anti };

            var rooms = GetRooms();

            LinkRoomsAndPeople(rooms, sherlock, watson, dog);

            IInterviewService interviewSerivce = new InterviewService(sherlock, watson, dog, rooms);
            //run program

            Room roomFound = null;
            var minutes = 0;

            while (roomFound == null)
            {
                interviewSerivce.IncrimentInterviews();

                if (interviewSerivce.TheifFound())
                    roomFound = interviewSerivce.GetTheif();
                minutes++;
            }

            Console.WriteLine("Found" + roomFound.Name + " after " + minutes + ".");
            Console.ReadLine();
        }

        private static void LinkRoomsAndPeople(List<Room> rooms, Interviewer sherlock, Interviewer watson, Interviewer dog)
        {
            var room1 = rooms.Where(x => x.Name == "Mustard").Single();

            room1.Interviewers.Add(dog);
            dog.CurrentRoom = room1;

            room1.Interviewers.Add(sherlock);
            sherlock.CurrentRoom = room1;

            var room6 = rooms.Where(x => x.Name == "White").Single();

            room6.Interviewers.Add(watson);
            watson.CurrentRoom = room6;
        }

        private static List<Room> GetRooms()
        {
            var room1 = new Room("Mustard");
            var room2 = new Room("Plum");
            var room3 = new Room("Green");
            var room4 = new Room("Peacock");
            var room5 = new Room("Scarlett");
            var room6 = new Room("White");

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

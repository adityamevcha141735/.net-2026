using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineEventRegistrationPortal
{
    public class RegistrationException : Exception
    {
        public RegistrationException(string message) : base(message) { }
    }

    public class Attendee
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public int TicketCount { get; set; }
        public string PaymentMode { get; set; } = string.Empty;
    }

    public class EventRegistrationSystem
    {
        private readonly List<Attendee> registrations = new();

        public void RegisterAttendee(Attendee attendee)
        {
            Validate(attendee);
            registrations.Add(attendee);
            Console.WriteLine($"Registration successful for {attendee.Name}.");
        }

        private static void Validate(Attendee attendee)
        {
            if (string.IsNullOrWhiteSpace(attendee.Name))
                throw new RegistrationException("Name is required.");

            if (string.IsNullOrWhiteSpace(attendee.Email) || !attendee.Email.Contains("@"))
                throw new RegistrationException("Valid email is required.");

            if (string.IsNullOrWhiteSpace(attendee.Phone) || attendee.Phone.Length < 10)
                throw new RegistrationException("Phone number must contain at least 10 digits.");

            if (string.IsNullOrWhiteSpace(attendee.EventName))
                throw new RegistrationException("Event name is required.");

            if (attendee.TicketCount <= 0 || attendee.TicketCount > 5)
                throw new RegistrationException("Ticket count must be between 1 and 5.");

            if (string.IsNullOrWhiteSpace(attendee.PaymentMode))
                throw new RegistrationException("Please select a payment mode.");
        }

        public void DisplayRegistrations()
        {
            Console.WriteLine("\n=== Registered Attendees ===");
            if (!registrations.Any())
            {
                Console.WriteLine("No registrations yet.");
                return;
            }

            foreach (var attendee in registrations)
            {
                Console.WriteLine($"Name: {attendee.Name}");
                Console.WriteLine($"Email: {attendee.Email}");
                Console.WriteLine($"Phone: {attendee.Phone}");
                Console.WriteLine($"Event: {attendee.EventName}");
                Console.WriteLine($"Tickets: {attendee.TicketCount}");
                Console.WriteLine($"Payment: {attendee.PaymentMode}");
                Console.WriteLine("----------------------------");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Online Event Registration Portal ===");

            var system = new EventRegistrationSystem();

            try
            {
                system.RegisterAttendee(new Attendee
                {
                    Name = "Aditya Mevcha",
                    Email = "aditya@example.com",
                    Phone = "9876543210",
                    EventName = "Microsoft 2026",
                    TicketCount = 2,
                    PaymentMode = "Online"
                });

                system.RegisterAttendee(new Attendee
                {
                    Name = "",
                    Email = "invalid",
                    Phone = "123",
                    EventName = "AI Summit",
                    TicketCount = 0,
                    PaymentMode = ""
                });
            }
            catch (RegistrationException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }

            system.DisplayRegistrations();
        }
    }
}

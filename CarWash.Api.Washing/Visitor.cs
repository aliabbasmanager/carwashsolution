using System;

namespace CarWash.Api.Operations
{
    public class Visitor
    {
        public Guid VisitorIdentifier { get; set; }

        public DateTime EntryTimeInQueue { get; set; }
    }
}

namespace CarWash.Core.Entities
{
    /// <summary>
    /// Entity class that stores all the runtime statistics
    /// </summary>
    public class OperationalStatistics
    {
        /// <summary>
        /// Gets or sets the visitors generated.
        /// </summary>
        /// <value>
        /// The visitors generated.
        /// </value>
        public int VisitorsGenerated { get; set; }

        /// <summary>
        /// Gets or sets the visitors accepted.
        /// </summary>
        /// <value>
        /// The visitors accepted.
        /// </value>
        public int VisitorsAccepted { get; set; }

        /// <summary>
        /// Gets or sets the visitors rejected.
        /// </summary>
        /// <value>
        /// The visitors rejected.
        /// </value>
        public int VisitorsRejected { get; set; }

        /// <summary>
        /// Gets or sets the total time spent in washing.
        /// </summary>
        /// <value>
        /// The total time spent in washing.
        /// </value>
        public double TotalTimeSpentInWashing { get; set; }

        /// <summary>
        /// Gets or sets the total time spent in drying.
        /// </summary>
        /// <value>
        /// The total time spent in drying.
        /// </value>
        public double TotalTimeSpentInDrying { get; set; }

        /// <summary>
        /// Gets or sets the total time spent in waiting.
        /// </summary>
        /// <value>
        /// The total time spent in waiting.
        /// </value>
        public double TotalTimeSpentInWaiting { get; set; }

        /// <summary>
        /// Gets the average time spent in washing.
        /// </summary>
        /// <value>
        /// The average time spent in washing.
        /// </value>
        public double AverageTimeSpentInWashing { get { return TotalNumberOfCarsWashed == 0 ? 0 : TotalTimeSpentInWashing / TotalNumberOfCarsWashed; } }

        /// <summary>
        /// Gets the average time spent in drying.
        /// </summary>
        /// <value>
        /// The average time spent in drying.
        /// </value>
        public double AverageTimeSpentInDrying { get { return TotalNumberOfCarsDried == 0 ? 0 : TotalTimeSpentInDrying / TotalNumberOfCarsDried; } }

        /// <summary>
        /// Gets the average time spent in waiting.
        /// </summary>
        /// <value>
        /// The average time spent in waiting.
        /// </value>
        public double AverageTimeSpentInWaiting { get { return TotalNumberOfCarsDried == 0 ? 0 : TotalTimeSpentInWaiting / TotalNumberOfCarsDried; } }

        /// <summary>
        /// Gets or sets the total number of cars washed.
        /// </summary>
        /// <value>
        /// The total number of cars washed.
        /// </value>
        public int TotalNumberOfCarsWashed { get; set; }

        /// <summary>
        /// Gets or sets the total number of cars dried.
        /// </summary>
        /// <value>
        /// The total number of cars dried.
        /// </value>
        public int TotalNumberOfCarsDried { get; set; }

    }
}

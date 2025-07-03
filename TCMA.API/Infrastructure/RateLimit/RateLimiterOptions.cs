namespace TCMA.API.Infrastructure.RateLimit
{
    public class RateLimiterOptions
    {
        public WriteOperationsOptions WriteOperations { get; set; }
        public ReadOperationsOptions ReadOperations { get; set; }
    }

    public class WriteOperationsOptions
    {
        public int PermitLimit { get; set; }
        public int QueueLimit { get; set; }
    }

    public class ReadOperationsOptions
    {
        public int PermitLimit { get; set; }
        public int WindowSeconds { get; set; }
        public int SegmentsPerWindow { get; set; }
        public int QueueLimit { get; set; }
    }
}

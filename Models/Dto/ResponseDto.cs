namespace Models.Dto
{
    public class ResponseDto
    {
        public RainfallReadingResponse RainfallReadingResponse { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class RainfallReadingResponse
    {
        public string Description { get; set; }
        public string Content { get; set; }
    }

    public class ErrorResponse
    {
        public string Description { get; set; }
        public string Content { get; set; }
    }
}

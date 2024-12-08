namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class PictureResultDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}

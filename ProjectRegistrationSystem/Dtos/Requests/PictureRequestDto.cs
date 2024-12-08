namespace ProjectRegistrationSystem.Dtos.Results
{
    public class PictureRequestDto
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}

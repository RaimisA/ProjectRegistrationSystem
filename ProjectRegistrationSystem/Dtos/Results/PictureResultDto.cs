namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents the result DTO for a picture.
    /// </summary>
    public class PictureResultDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the picture.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the file name of the picture.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the data of the picture.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the content type of the picture.
        /// </summary>
        public string ContentType { get; set; }
    }
}
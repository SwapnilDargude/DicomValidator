namespace DicomValidator.Models
{
	public class FileUploadRequest
	{
		public IFormFile File { get; set; }
		public bool Deidentify { get; set; } = false;
	}

}

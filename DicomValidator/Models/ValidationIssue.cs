namespace DicomValidator.Models
{
	public class ValidationIssue
	{
		public string Tag { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Issue { get; set; } = string.Empty;
		public string Severity { get; set; } = "Info";
		public string Suggestion { get; internal set; }
	}
}

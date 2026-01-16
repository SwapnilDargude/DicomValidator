namespace DicomValidator.Models
{
	public class ValidationReport
	{
		public List<ValidationIssue> Issues { get; set; } = new();
		public bool IsValid => !Issues.Any(i => i.Severity == "Error");
	}
}

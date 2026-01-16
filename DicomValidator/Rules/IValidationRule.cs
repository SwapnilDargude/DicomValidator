using Dicom;
using DicomValidator.Models;

namespace DicomValidator.Rules
{
	public interface IValidationRule
	{
		IEnumerable<ValidationIssue> Validate(DicomDataset dataset);
	}
}

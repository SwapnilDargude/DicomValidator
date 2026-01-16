using Dicom;
using DicomValidator.Models;

namespace DicomValidator.Rules
{
	public class RequiredTagsRule : IValidationRule
	{
		private static readonly DicomTag[] RequiredTags = {
			DicomTag.PatientName,
			DicomTag.PatientID,
			DicomTag.StudyInstanceUID,
			DicomTag.SeriesInstanceUID,
			DicomTag.SOPInstanceUID,
			DicomTag.SOPClassUID,
			DicomTag.Modality
		};
		public IEnumerable<ValidationIssue> Validate(DicomDataset ds)
		{
			foreach (var tag in RequiredTags)
			{
				if (!ds.Contains(tag) || string.IsNullOrWhiteSpace(ds.Get<string>(tag, string.Empty)))
				{
					yield return new ValidationIssue
					{
						Tag = tag.ToString(),
						Name = tag.DictionaryEntry.Name,
						Issue = "Missing required tag or empty value",
						Severity = "Error",
						Suggestion = "Populate this tag with a valid value according to DICOM standard."
					};
				}
			}
		}


	}
}
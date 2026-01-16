using Dicom;
using DicomValidator.Models;
using System.Text.RegularExpressions;

namespace DicomValidator.Rules
{
	public class UidRule : IValidationRule
	{
		private static readonly Regex UidRegex = new(@"^[0-9]+(\.[0-9]+)+$", RegexOptions.Compiled);

		private static readonly DicomTag[] UidTags =
		{
		DicomTag.StudyInstanceUID,
		DicomTag.SeriesInstanceUID,
		DicomTag.SOPInstanceUID,
		DicomTag.SOPClassUID
	};
		public IEnumerable<ValidationIssue> Validate(DicomDataset ds)
		{
			foreach (var tag in UidTags)
			{
				if (!ds.TryGetString(tag, out var value) || string.IsNullOrWhiteSpace(value))
					continue;

				if (value.Length > 64 || !UidRegex.IsMatch(value))
				{
					yield return new ValidationIssue
					{
						Tag = tag.ToString(),
						Name = tag.DictionaryEntry.Name,
						Issue = "Invalid UID format or length",
						Severity = "Error",
						Suggestion = "Use numeric components separated by dots, max length 64 characters."
					};
				}
			}
		}
	}
}

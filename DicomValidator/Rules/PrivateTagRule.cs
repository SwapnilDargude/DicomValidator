using Dicom;
using DicomValidator.Models;

namespace DicomValidator.Rules
{
	public class PrivateTagRule : IValidationRule
	{
		public IEnumerable<ValidationIssue> Validate(DicomDataset ds)
		{
			foreach (var item in ds)
			{
				if (item.Tag.IsPrivate)
				{
					yield return new ValidationIssue
					{
						Tag = item.Tag.ToString(),
						Name = item.Tag.DictionaryEntry.Name,
						Issue = "Private tag present.",
						Severity = "Info",
						Suggestion = "Review if this private tag should be removed before AI processing."
					};
				}

				if (item is DicomSequence seq)
				{
					foreach (var nested in seq.Items)
					{
						foreach (var nestedItem in nested)
						{
							if (nestedItem.Tag.IsPrivate)
							{
								yield return new ValidationIssue
								{
									Tag = nestedItem.Tag.ToString(),
									Name = nestedItem.Tag.DictionaryEntry.Name,
									Issue = "Private tag present in sequence.",
									Severity = "Info",
									Suggestion = "Review private tags in nested sequences."
								};
							}
						}
					}
				}
			}
		}
	}
}

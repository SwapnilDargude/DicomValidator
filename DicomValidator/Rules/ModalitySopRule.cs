using Dicom;
using DicomValidator.Models;

namespace DicomValidator.Rules
{
	public class ModalitySopRule : IValidationRule
	{
		private static readonly Dictionary<string, HashSet<string>> ModalityToSop = new()
	{
		{ "CT", new HashSet<string> { DicomUID.CTImageStorage.UID } },
		{ "MR", new HashSet<string> { DicomUID.MRImageStorage.UID } },
        // extend as needed
    };
		public IEnumerable<ValidationIssue> Validate(DicomDataset ds)
		{
			var modality = ds.Get<string>(DicomTag.Modality, string.Empty);
			var sopClass = ds.Get<string>(DicomTag.SOPClassUID, string.Empty);

			if (string.IsNullOrWhiteSpace(modality) || string.IsNullOrWhiteSpace(sopClass))
				yield break;

			if (ModalityToSop.TryGetValue(modality, out var allowed) && !allowed.Contains(sopClass))
			{
				yield return new ValidationIssue
				{
					Tag = DicomTag.SOPClassUID.ToString(),
					Name = DicomTag.SOPClassUID.DictionaryEntry.Name,
					Issue = $"SOPClassUID '{sopClass}' may be inconsistent with Modality '{modality}'.",
					Severity = "Warning",
					Suggestion = "Verify modality and SOP class mapping."
				};
			}
		}
	}
}

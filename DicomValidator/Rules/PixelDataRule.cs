using Dicom;
using Dicom.Imaging;
using DicomValidator.Models;

namespace DicomValidator.Rules
{
	public class PixelDataRule : IValidationRule
	{
		public IEnumerable<ValidationIssue> Validate(DicomDataset ds)
		{
			if (!ds.Contains(DicomTag.PixelData))
			{
				yield return new ValidationIssue
				{
					Tag = DicomTag.PixelData.ToString(),
					Name = DicomTag.PixelData.DictionaryEntry.Name,
					Issue = "Missing PixelData",
					Severity = "Error",
					Suggestion = "Ensure image pixel data is present."
				};
				yield break;
			}

			var rows = ds.Get<int>(DicomTag.Rows, 0);
			var cols = ds.Get<int>(DicomTag.Columns, 0);

			if (rows <= 0 || cols <= 0)
			{
				yield return new ValidationIssue
				{
					Tag = $"{DicomTag.Rows}/{DicomTag.Columns}",
					Name = "Rows/Columns",
					Issue = "Rows or Columns are zero or missing.",
					Severity = "Error",
					Suggestion = "Set valid image dimensions."
				};
				yield break;
			}

			var pixelData = DicomPixelData.Create(ds, false);
			var spp = pixelData.SamplesPerPixel;
			var bits = pixelData.BitsAllocated;

			var expectedBytesPerFrame = rows * cols * spp * bits / 8;

			for (int i = 0; i < pixelData.NumberOfFrames; i++)
			{
				var buffer = pixelData.GetFrame(i).Data;
				if (buffer.Length != expectedBytesPerFrame)
				{
					yield return new ValidationIssue
					{
						Tag = DicomTag.PixelData.ToString(),
						Name = DicomTag.PixelData.DictionaryEntry.Name,
						Issue = $"PixelData length mismatch in frame {i}. Expected {expectedBytesPerFrame}, got {buffer.Length}.",
						Severity = "Error",
						Suggestion = "Verify Rows, Columns, SamplesPerPixel, BitsAllocated and raw pixel buffer."
					};
				}
			}
		}
	}
}

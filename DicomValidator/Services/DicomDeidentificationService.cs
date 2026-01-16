using Dicom;

namespace DicomValidator.Services
{
	public class DicomDeidentificationService
	{
		public DicomFile Deidentify(DicomFile file)
		{
			var anonymizer = new DicomAnonymizer();
			return anonymizer.Anonymize(file);
		}
	}
}

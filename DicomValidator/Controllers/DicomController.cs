using Dicom;
using DicomValidator.Models;
using DicomValidator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;

namespace DicomValidator.Controllers
{

	[ApiController]
	[Route("api/dicom")]
	public class DicomController : ControllerBase
	{
		private readonly DicomValidationService _validator;
		private readonly DicomDeidentificationService _deidentifier;

		public DicomController(
			DicomValidationService validator,
			DicomDeidentificationService deidentifier)
		{
			_validator = validator;
			_deidentifier = deidentifier;
		}

		[HttpPost("validate")]
		public async Task<ActionResult<ValidationReport>> Validate([FromForm] FileUploadRequest fur)
		{
			if (fur.File == null || fur.File.Length == 0)
				return BadRequest("No file uploaded.");

			DicomFile dicomFile;
			await using (var stream = fur.File.OpenReadStream())
			{
				dicomFile = await DicomFile.OpenAsync(stream);
			}

			if (fur.Deidentify)
			{
				dicomFile = _deidentifier.Deidentify(dicomFile);
			}

			var report = _validator.Validate(dicomFile.Dataset);

			return Ok(report);
		}
	}

}
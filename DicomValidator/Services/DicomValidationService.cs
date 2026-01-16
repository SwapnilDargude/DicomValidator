using Dicom;
using DicomValidator.Models;
using DicomValidator.Rules;

namespace DicomValidator.Services
{
    public class DicomValidationService

    {
        private readonly IEnumerable<IValidationRule> _rules;

        public DicomValidationService(IEnumerable<IValidationRule> rules)
        {
            _rules = rules;
        }

        public ValidationReport Validate(DicomDataset dataset)
        {
            var report = new ValidationReport();

            foreach (var rule in _rules)
            {
                report.Issues.AddRange(rule.Validate(dataset));
            }

            return report;
        }
    }
}

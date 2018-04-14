using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePyramidv1.ViewModels
{
    public class ProgressAndAssessmentViewModel
    {
        public Dictionary<String, Int16> AssessmentScores { get; set; }
        public List<String> CompletedLessons { get; set; }
    }
}

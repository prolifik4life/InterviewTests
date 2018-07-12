using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker.Tests.Unit
{
	class TestStudents
	{
		public enum TestStudentIds
		{
			remedial = 4,
			average = 3,
			magnaCumLaude = 2,
			summaCumLaude = 1,
			noCourses = 5,
			missingCourses = 6,
			over100 = 7,
			highAverageFailsRequirement = 8,
			repeatedCourse = 9,
			varyingGrades = 10,
			allZeroGrades = 11,
			averageUnderZero = 12,
			remedialInRequirementCoursesOnly = 13,
			failingOneRequirementCredit = 14,
			missingOneRequirementCredit = 15,
			enoughCreditsToMeetRequirementButNotAll = 16,
			wouldNeedToReuseARequirementCredit = 17
		}
	}
}

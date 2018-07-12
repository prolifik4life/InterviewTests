using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.DAL.Interfaces;

namespace GraduationTracker
{
	public partial class GraduationTracker


	{
		private readonly IGraduationTracker repository;

		public GraduationTracker(IGraduationTracker repository)
		{
			this.repository = repository;
		}

		// For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
		// and their standing based on their grade average
		public GraduationStatus HasGraduated(Diploma diploma, Student student)
		{
			List<Course> completedRequirementCourses = new List<Course>();
			List<int> completedRequirementIds = new List<int>();

			foreach (int requirementId in diploma.Requirements)
			{
				int completedCreditsCount = 0;
				Requirement requirement = repository.GetRequirement(requirementId);
				foreach (int courseId in requirement.Courses)
				{
					Course completedRequirementCourse = GetStudentRequiredCourse(courseId, student.Courses);
					if (completedRequirementCourse != null &&
						completedRequirementCourse.Mark >= requirement.MinimumMark &&
						completedRequirementCourses.Contains(completedRequirementCourse) == false
						)
					{
						completedRequirementCourses.Add(completedRequirementCourse);
						completedCreditsCount += 1;
					}
				}
				if (completedCreditsCount >= requirement.Credits)
				{
					completedRequirementIds.Add(requirementId);
				}
			}
			diploma.Requirements.Sort();
			completedRequirementIds.Sort();
			Boolean metRequirements = completedRequirementIds.SequenceEqual(diploma.Requirements);

			Standing standing = GetStandingFromAverage(completedRequirementCourses);//TODO: test edge cases... no data, letters, negative, zero

			Boolean hasGraduated = (metRequirements && standing != Standing.Remedial && standing != Standing.None);
			return new GraduationStatus { HasGraduated = hasGraduated, Standing = standing };
		}

		private Course GetStudentRequiredCourse(int courseId, List<Course> studentCourses)
		{
			return studentCourses.Find(x => x.Id == courseId);
		}

		private Standing GetStandingFromAverage(List<Course> completedRequirementCourses)
		{
			Standing standing;
			double average =
				completedRequirementCourses.Any() ? Math.Round(completedRequirementCourses.Average(course => course.Mark)) : 0;

			switch (average)
			{
				case var exp when (average >= 0 && average < 50):
					standing = Standing.Remedial;//TODO: would these thresholds be better stored in the data source?
					break;
				case var exp when (average < 80):
					standing = Standing.Average;
					break;
				case var exp when (average < 95):
					standing = Standing.MagnaCumLaude;
					break;
				case var exp when (average >= 95 && average <= 100):
					standing = Standing.SummaCumLaude;
					break;
				default:
					standing = Standing.None;
					break;
			}
			return standing;
		}
	}

}

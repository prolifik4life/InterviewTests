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
		//TODO: would rather inject repository choice, or put it in config 
		public IGraduationTracker repository = new GraduationTrackerRepository();

		// For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
		// and their standing based on their grade average
		public Tuple<bool, Standing> HasGraduated(Diploma diploma, Student student)
		{
			IGraduationTracker repository = new GraduationTrackerRepository();
			List<Course> studentUnusedCourses = student.Courses;//courses not yet used for to fulfill a credit requirement
			List<Course> completedRequirementCourses = new List<Course>();
			List<int> completedRequirementIds = new List<int>();

			foreach (int requirementId in diploma.Requirements)
			{
				int completedCreditsCount = 0;
				Requirement requirement = repository.GetRequirement(requirementId);
				foreach (int courseId in requirement.Courses)
				{
					Course completedRequirementCourse = GetStudentRequiredCourse(courseId, ref studentUnusedCourses);
					if (completedRequirementCourse != null && completedRequirementCourse.Mark >= requirement.MinimumMark)
					{
						//TODO: should these be globally available, to make it easier to separate this logic out? Can this logic be separated out into one thing, or is doing a few things?
						studentUnusedCourses.Remove(completedRequirementCourse);//cannot be reused for later requirements
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
			Boolean metRequirements = diploma.Requirements == completedRequirementIds;

			Standing standing = GetStandingFromAverage(completedRequirementCourses);//TODO: test edge cases... no data, letters, negative, zero

			Boolean isGraduated = (metRequirements && standing != Standing.Remedial && standing != Standing.None);

			return new Tuple<bool, Standing>(isGraduated, standing);
		}

		private Course GetStudentRequiredCourse(int courseId, ref List<Course> studentUnusedCourses)
		{
			return studentUnusedCourses.Find(x => x.Id == courseId);
		}

		private Standing GetStandingFromAverage(List<Course> completedRequirementCourses)
		{
			Standing standing;
			double average = Math.Round(completedRequirementCourses.Average(course => course.Mark));
			switch (average)
			{
				case var exp when (average < 50):
					standing = Standing.Remedial;//TODO: would these thresholds be better stored in the data source?
					break;
				case var exp when (average < 80):
					standing = Standing.Average;
					break;
				case var exp when (average < 95):
					standing = Standing.MagnaCumLaude;
					break;
				case var exp when (average >= 95 && average <=100):
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

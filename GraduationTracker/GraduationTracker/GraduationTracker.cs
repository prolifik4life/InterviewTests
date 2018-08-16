using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.DAL.Interfaces;

namespace GraduationTracker
{
    public class GraduationTracker
    {
        private List<Course> unusedCourses;
        private readonly IGraduationTracker repository;
        public GraduationTracker(IGraduationTracker repository)
        {
            this.repository = repository;
        }

        // For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
        // and their standing based on their grade average
        public GraduationStatus HasGraduated(Diploma diploma, Student student)
        {
            //List<Course> completedRequirementCourses = new List<Course>();
            //List<int> completedRequirementIds = new List<int>();
            GraduationStatus result = null;

            unusedCourses = new List<Course>(student.Courses);

            Boolean hasGraduated = diploma.Requirements.TrueForAll(x => StudentPassesRequirement(student.Courses, x ));
            

            //for each requirement, get requirement details, check each course and see if student completed the course successfully
            //foreach (int requirementId in diploma.Requirements)
            //{
            //	int completedCreditsCount = 0;
            //	Requirement requirement = repository.GetRequirement(requirementId);
            //	foreach (int courseId in requirement.Courses)
            //	{
            //		Course completedRequirementCourse = GetStudentRequiredCourse(courseId, student.Courses);

            //		if (completedRequirementCourse != null)
            //		{
            //			if (completedRequirementCourse.Mark < 0 || completedRequirementCourse.Mark > 100)
            //			{
            //				throw new System.ArgumentOutOfRangeException("student", "Student course mark not between 0 to 100");
            //			}

            //			if (
            //				completedRequirementCourse.Mark >= requirement.MinimumMark &&
            //				completedRequirementCourses.Contains(completedRequirementCourse) == false
            //				)
            //			{
            //				completedCreditsCount += 1;
            //			}
            //			//completed whether got credit or not, will be added to average
            //			completedRequirementCourses.Add(completedRequirementCourse);
            //		}
            //	}
            //	if (completedCreditsCount >= requirement.Credits)
            //	{
            //		completedRequirementIds.Add(requirementId);
            //	}
            //}
            //diploma.Requirements.Sort();
            //completedRequirementIds.Sort();
            //Boolean metRequirements = completedRequirementIds.SequenceEqual(diploma.Requirements);

            Standing standing = GetStandingFromAverage(completedRequirementCourses);

            //Boolean hasGraduated = (metRequirements && standing != Standing.Remedial && standing != Standing.None);
            result = new GraduationStatus { HasGraduated = hasGraduated, Standing = standing };

            return result;
        }

        private Boolean StudentPassesRequirement(List<Course> studentCourses, int requirementId)
        {
            //foreach 
            return true;
        }

        private Requirement GetRequirementContainingCourse(int courseId, List<int> requirementIds) {
            foreach(int requirementId in requirementIds){
                Requirement requirement = repository.GetRequirement(requirementId);
                if(Array.Exists(requirement.Courses, x => x == courseId)){
                    return requirement;
                } 
            }
            return null;
        }

        private Standing GetStandingFromAverage(List<Course> completedRequirementCourses)
        {
            Standing standing;
            double average =
                completedRequirementCourses.Any() ? Math.Round(completedRequirementCourses.Average(course => course.Mark)) : 0;

            switch (average)
            {
                //would these thresholds be better stored in datasource?
                case var exp when (average >= 0 && average < 50):
                    standing = Standing.Remedial;
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

		//private Course GetStudentRequiredCourse(int courseId, List<Course> studentCourses)
		//{
		//	return studentCourses.Find(x => x.Id == courseId);
		//}

	}

}

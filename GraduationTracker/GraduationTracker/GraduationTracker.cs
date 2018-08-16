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
        private List<Course> coursesUsedToMeetRequirements;

        private readonly IGraduationTracker repository;
        public GraduationTracker(IGraduationTracker repository)
        {
            this.repository = repository;
        }

        // For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
        // and their standing based on their grade average
        public GraduationStatus HasGraduated(Diploma diploma, Student student)
        {
            coursesUsedToMeetRequirements = new List<Course>();

            List<Boolean> requirementResults = diploma.Requirements.ConvertAll(requirement => StudentPassesRequirement(requirement, student.Courses));
            Boolean hasMetDiplomaRequirements = requirementResults.All(result=>result==true);

            Standing standing = GetStandingFromCourses(coursesUsedToMeetRequirements);

            Boolean hasGraduated = (hasMetDiplomaRequirements && standing != Standing.Remedial && standing != Standing.None);
            return new GraduationStatus { HasGraduated = hasGraduated, Standing = standing };
        }

        //future improvement: coursesUsedToMeetRequirements gets mutated as a side effect of this function
        //would prefer to avoid this if possible so that the function could be used outside of this class in future if needed.
        private Boolean StudentPassesRequirement(int requirementId, List<Course> studentCourses)
        {
            Requirement requirement = repository.GetRequirement(requirementId);
            int requirementCreditsPassed = 0;
            List<Course> coursesWorthCredits = new List<Course>();
            List<Course> unusedStudentCourses = studentCourses.Except(coursesUsedToMeetRequirements).ToList();

            //if there is a student course matching a requirement course, assign credit if its mark is sufficient
            foreach (int requirementCourseId in requirement.Courses)
            {
                Course studentRequirementCourse = GetStudentRequiredCourse(requirementCourseId, unusedStudentCourses);
                if (studentRequirementCourse != null && studentRequirementCourse.Mark >= requirement.MinimumMark)
                {
                    requirementCreditsPassed += 1;
                    coursesWorthCredits.Add(studentRequirementCourse);
                }
            }
            //only consider courses to be "used" if they were applied to a requirement that was passed.
            if (requirementCreditsPassed >= requirement.Credits)
            {
                coursesUsedToMeetRequirements.AddRange(coursesWorthCredits);
            }
            return requirementCreditsPassed >= requirement.Credits;
        }

        private Course GetStudentRequiredCourse(int courseId, List<Course> studentCourses)
        {
            return studentCourses.Find(x => x.Id == courseId);
        }

        private Standing GetStandingFromCourses(List<Course> courses)
        {
            Standing standing;
            double average =
                courses.Any() ? Math.Round(courses.Average(course => course.Mark)) : 0;

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
    }

}

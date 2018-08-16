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
        private readonly IGraduationTracker repository;
        public GraduationTracker(IGraduationTracker repository)
        {
            this.repository = repository;
        }

        // For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
        // and their standing based on their grade average
        public GraduationStatus HasGraduated(Diploma diploma, Student student)
        {
            List<StudentRequirementCourse> studentRequirementCourses = GetStudentRequirementCourses(student.Courses, diploma.Requirements);

            Boolean hasMetDiplomaRequirements = diploma.Requirements.TrueForAll(requirementId => StudentPassesRequirement(requirementId, studentRequirementCourses));

            Standing standing = GetStandingFromCourses(studentRequirementCourses.Select(studentRequirementCourse => studentRequirementCourse.Course).ToList());

            Boolean hasGraduated = (hasMetDiplomaRequirements && standing != Standing.Remedial && standing != Standing.None);
            return new GraduationStatus { HasGraduated = hasGraduated, Standing = standing };
        }

        //Get list of student courses matched to requirement courses. 
        //Note that we're taking the first matching requirement... In a real app we'd want logic to determine
        //which requirement to use the student course for. 
        private List<StudentRequirementCourse> GetStudentRequirementCourses(List<Course> studentCourses, List<int> requirementIds)
        {
            List<StudentRequirementCourse> studentRequirementCourses = studentCourses.Select(studentCourse => MatchStudentCourseToRequirement(studentCourse, requirementIds))
                                                                                 .Where(studentRequirementCourse => studentRequirementCourse != null).ToList();
            return studentRequirementCourses;
        }

        private StudentRequirementCourse MatchStudentCourseToRequirement(Course studentCourse, List<int> requirementIds)
        {

            foreach (int requirementId in requirementIds)
            {
                Requirement requirement = repository.GetRequirement(requirementId);
                //TODO: in future, convert arrays to lists so we can use methods like "Contains" that would be more readable
                if (Array.Exists(requirement.Courses, requirementCourse => requirementCourse == studentCourse.Id))
                {
                    return new StudentRequirementCourse { Course = studentCourse, Requirement = requirement };
                }
            }
            return null;
        }

        //for each course in requirement, check for matching student course. If found, check if grade meets requirement
        //and assign credit. If credits are sufficient for requirement, student passes requirement.
        private Boolean StudentPassesRequirement (int requirementId, List<StudentRequirementCourse> studentRequirementCourses){
            Requirement requirement = repository.GetRequirement(requirementId);
            int creditsPassed = 0;
            foreach(int requirementCourseId in requirement.Courses){
                StudentRequirementCourse studentRequirementCourse = studentRequirementCourses.Find(x => x.Course.Id == requirementCourseId && x.Requirement.Id == requirementId);
                if (studentRequirementCourse!=null) {
                    Course studentCourse = studentRequirementCourse.Course;
                    if (studentCourse.Mark < 0 || studentCourse.Mark > 100)
                    {
                        throw new System.ArgumentOutOfRangeException("student", "Student course mark not between 0 to 100");
                    }

                    if (studentCourse.Mark >= requirement.MinimumMark)
                    {
                        creditsPassed += 1;
                    }
                }
            }
            return creditsPassed >= requirement.Credits;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker : IGraduationTracker
    {   
        private double GetStudentCoursesAverage(Diploma diploma, Student student)
        {
            var credits = 0;
            double average = 0;

            var requirements = diploma.Requirements.AsQueryable();
            foreach(Requirement requirement in requirements)
            {
                for (int j = 0; j < student.Courses.Length; j++)
                {
                    for (int k = 0; k < requirement.Courses.Length; k++)
                    {
                        if (requirement.Courses[k] == student.Courses[j].Id)
                        {
                            average += student.Courses[j].Mark;
                            if (student.Courses[j].Mark > requirement.MinimumMark)
                            {
                                credits += requirement.Credits;
                            }
                        }
                    }
                }
            }

            average = average / student.Courses.Length;
            return average;
        }

        public Tuple<bool, STANDING>  HasGraduated(Diploma diploma, Student student)
        {
            var average = GetStudentCoursesAverage(diploma, student);
            var standing = StandingCalculator.GetStanding(average);

            switch (standing)
            {
                case STANDING.Remedial:
                    return new Tuple<bool, STANDING>(false, standing);
                case STANDING.Average:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.SumaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.MagnaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);

                default:
                    return new Tuple<bool, STANDING>(false, standing);
            } 
        }
    }
}

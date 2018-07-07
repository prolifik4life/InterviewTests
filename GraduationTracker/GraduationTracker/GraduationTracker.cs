using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {   
        //will output boolean of whether student has graduated, and with what standing given diploma and student as inputs
        public Tuple<bool, STANDING>  HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;

            //loop through list of requirements for the given diploma
            for(int i = 0; i < diploma.Requirements.Length; i++)
            {
                //loop through the given student's course records
                for(int j = 0; j < student.Courses.Length; j++)
                {
                    //find requirement that matches current diploma requirement 
                    var requirement = Repository.GetRequirement(diploma.Requirements[i]);

                    //loop through requirement courses (this could be multiple, though in examples there's only ever one)
                    for (int k = 0; k < requirement.Courses.Length; k++)
                    {
                        //find the requirement course that matches the ID of the student course at the given index
                        if (requirement.Courses[k] == student.Courses[j].Id)
                        {
                            //add the course mark for the current student course to the average var (will change)
                            average += student.Courses[j].Mark;
                            //if the given student course mark GREATER THAN minimum mark, requirement credits added to
                            // student credits
                            if (student.Courses[j].Mark > requirement.MinimumMark)
                            {
                                credits += requirement.Credits;
                            }
                        }
                    }
                }
            }
            //sum / course length... not multiplied x100?
            average = average / student.Courses.Length;

            var standing = STANDING.None;
            //set standing by average mark
            if (average < 50)
                standing = STANDING.Remedial;
            else if (average < 80)
                standing = STANDING.Average;
            else if (average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.MagnaCumLaude;

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

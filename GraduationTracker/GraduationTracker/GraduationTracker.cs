using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker

    //General Requirements: 
    // For a given student and diploma, need to determine whether they've graduated (met the requirements of the diploma)
    // and calculate their standing based on their grade average

    //Steps:
    //Get list of diploma requirements
    //For each requirement, get list of courses (remember that although the examples only show one, there could be several)
    //For each requirement course in requirement, check if student has completed the course   
    //If they have the required course, then 
        //once applied to that requirement, it can not be used to fill another requirement) --> try to find a way to give unneeded credits 
        //add the mark to the mark sum to calculate their average
        //if their grade in the course was sufficient to pass, 
            //add the credit to the list of credits for the given requirement
    // After going through requirement, check if student credits for requirement is greater than or equal to requirement credits
    //If so, we'll need to track that they passed the requirement? Or failed it.
    // After going through requirements, calculate average, and use average to calculate standing. 
    // Check if standing is sufficient to graduate, as well as checking if all requirements have been met.

//* There is a missing check
//* A studentmust fulfill all requirements(i.e.they must have a course(s)
//that has enough credits to fill a given requirement, and once applied to that requirement, it can not be used to fill another requirement)
//* There can be multiple courses per requirement.
//* A requirement is considered complete when any number of courses it
//contains have been completed so that the total number of course credits is greater than or equal to the number of requirement credits.
//* No the student does not need to have received the minimum mark in all
//courses a requirement contains.Only in enough courses to have a number of credits equal to or greater than the number of credits in the requirement.


    {   
    // For a given student and diploma, determines whether they've graduated (met the requirements of the diploma)
    // and their standing based on their grade average
        public Tuple<bool, Standing>  HasGraduated(Diploma diploma, Student student)
        {
            //get requirements from diploma input
            int[] requirements = diploma.Requirements;
            getAllCourseRequirements
            foreach(int requirement in requirements){
                int reqCourse = 
                foreach (int reqCourse in requirements){
                    
                }
            }
            return new Tuple<bool, Standing>(true, Standing.Average);
        }
        //
        private [] getAllCourseRequirements () {
            
        }
    }

}

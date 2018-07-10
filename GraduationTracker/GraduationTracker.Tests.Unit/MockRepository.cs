using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.DAL.Interfaces;

namespace GraduationTracker.Tests.Unit
{
    public class MockRepository
    {
		public class GraduationTrackerRepository : IGraduationTracker
		{
			//students
			public Student GetStudent(int id)
			{
				List<Student> students = GetStudents();
				return students.Find(x => x.Id == id);
			}

			private List<Student> GetStudents()
			{
				return new List<Student>
			{
			   new Student
			   {
				   Id = 1,
				   Courses = new Course[]
				   {
						new Course{Id = 1, Name = "Math", Mark=95 },
						new Course{Id = 2, Name = "Science", Mark=95 },
						new Course{Id = 3, Name = "Literature", Mark=95 },
						new Course{Id = 4, Name = "Physical Education", Mark=95 }
				   }
			   },
			   new Student
			   {
				   Id = 2,
				   Courses = new Course[]
				   {
						new Course{Id = 1, Name = "Math", Mark=80 },
						new Course{Id = 2, Name = "Science", Mark=80 },
						new Course{Id = 3, Name = "Literature", Mark=80 },
						new Course{Id = 4, Name = "Physical Education", Mark=80 }
				   }
			   },
			new Student
			{
				Id = 3,
				Courses = new Course[]
				{
					new Course{Id = 1, Name = "Math", Mark=50 },
					new Course{Id = 2, Name = "Science", Mark=50 },
					new Course{Id = 3, Name = "Literature", Mark=50 },
					new Course{Id = 4, Name = "Physical Education", Mark=50 }
				}
			},
			new Student
			{
				Id = 4,
				Courses = new Course[]
				{
					new Course{Id = 1, Name = "Math", Mark=40 },
					new Course{Id = 2, Name = "Science", Mark=40 },
					new Course{Id = 3, Name = "Literature", Mark=40 },
					new Course{Id = 4, Name = "Physical Education", Mark=40 }
				}
			}

			};
			}

			//diplomas
			public Diploma GetDiploma(int id)
			{
				List<Diploma> diplomas = GetDiplomas();
				return diplomas.Find(x => x.Id == id);
			}

			private List<Diploma> GetDiplomas()
			{
				return new List<Diploma>
			{
				new Diploma
				{
					Id = 1,
					Credits = 4,
					Requirements = new int[]{100,102,103,104}
				}
			};
			}

			//requirements
			public Requirement GetRequirement(int id)
			{
				List<Requirement> requirements = GetRequirements();
				return requirements.Find(x => x.Id == id);
			}

			private List<Requirement> GetRequirements()
			{
				return new List<Requirement>
				{
					new Requirement{Id = 100, Name = "Math", MinimumMark=50, Courses = new int[]{1}, Credits=1 },
					new Requirement{Id = 102, Name = "Science", MinimumMark=50, Courses = new int[]{2}, Credits=1 },
					new Requirement{Id = 103, Name = "Literature", MinimumMark=50, Courses = new int[]{3}, Credits=1},
					new Requirement{Id = 104, Name = "Physical Education", MinimumMark=50, Courses = new int[]{4}, Credits=1 }
				};
			}
		}
	}


}

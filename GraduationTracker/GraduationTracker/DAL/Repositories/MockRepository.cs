using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraduationTracker.DAL.Interfaces;

namespace GraduationTracker.Tests.Unit
{

	public class MockRepository : IGraduationTracker
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
				   Courses = new List<Course>
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
				   Courses = new List<Course>
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
				Courses = new List<Course>
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
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=40 },
					new Course{Id = 2, Name = "Science", Mark=40 },
					new Course{Id = 3, Name = "Literature", Mark=40 },
					new Course{Id = 4, Name = "Physical Education", Mark=40 }
				}
			},
			new Student
			{
				Id = 5,
				Courses = new List<Course>
				{
				}
			},
			new Student
			{
				Id = 6,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=100 }
				}
			},
			new Student
			{
				Id = 7,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=110 },
					new Course{Id = 2, Name = "Science", Mark=120 },
					new Course{Id = 3, Name = "Literature", Mark=130 },
					new Course{Id = 4, Name = "Physical Education", Mark=140 }
				}
			},
			new Student
			{
				Id = 8,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=100 },
					new Course{Id = 3, Name = "Literature", Mark=100 },
					new Course{Id = 4, Name = "Physical Education", Mark=49 }
				}
			},
			new Student
			{
				Id = 9,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 3, Name = "Literature", Mark=100 },
					new Course{Id = 4, Name = "Physical Education", Mark=100 }
				}
			},
			new Student
			{
				Id = 10,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=0 },
					new Course{Id = 3, Name = "Literature", Mark=30 },
					new Course{Id = 4, Name = "Physical Education", Mark=90 }
				}
			},
			new Student
			{
				Id = 11,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=0 },
					new Course{Id = 2, Name = "Science", Mark=0 },
					new Course{Id = 3, Name = "Literature", Mark=0 },
					new Course{Id = 4, Name = "Physical Education", Mark=0 }
				}
			},
			new Student
			{
				Id = 12,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=-10 },
					new Course{Id = 2, Name = "Science", Mark=0 },
					new Course{Id = 3, Name = "Literature", Mark=0 },
					new Course{Id = 4, Name = "Physical Education", Mark=0 }
				}
			},
			new Student
			{
				Id = 13,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=40 },
					new Course{Id = 2, Name = "Science", Mark=40 },
					new Course{Id = 3, Name = "Literature", Mark=40 },
					new Course{Id = 4, Name = "Physical Education", Mark=40 },
					new Course{Id = 5, Name = "Biology", Mark=100 },
					new Course{Id = 6, Name = "Psychology", Mark=100 },
					new Course{Id = 7, Name = "Drama", Mark=100 },
					new Course{Id = 8, Name = "Art", Mark=100 }
				}
			},
			new Student
			{
				Id = 14,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=100 },
					new Course{Id = 3, Name = "Literature", Mark=100 },
					new Course{Id = 4, Name = "Physical Education", Mark=100 },
					new Course{Id = 5, Name = "Biology", Mark=49 }
				}
			},
			new Student
			{
				Id = 15,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=100 },
					new Course{Id = 3, Name = "Literature", Mark=100 },
					new Course{Id = 4, Name = "Physical Education", Mark=100 }
				}
			},
			new Student
			{
				Id = 16,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 2, Name = "Science", Mark=100 },
					new Course{Id = 3, Name = "Literature", Mark=100 },
					new Course{Id = 4, Name = "Physical Education", Mark=100 },
					new Course{Id = 5, Name = "Art", Mark=100 }
				}
			},
			new Student
			{
				Id = 17,
				Courses = new List<Course>
				{
					new Course{Id = 1, Name = "Math", Mark=100 },
					new Course{Id = 5, Name = "Art", Mark=100 }
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
					Requirements = new List<int>{100,102,103,104}
				},
				new Diploma
				{
					Id = 2,
					Credits = 4,
					Requirements = new List<int>{102,103,104,105}
				},
				new Diploma
				{
					Id = 3,
					Credits = 4,
					Requirements = new List<int>{102,106}
				},
				new Diploma
				{
					Id = 4,
					Credits = 4,
					Requirements = new List<int>{100,105}
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
					new Requirement{Id = 104, Name = "Physical Education", MinimumMark=50, Courses = new int[]{4}, Credits=1 },
					new Requirement{Id = 105, Name = "Computer Science", MinimumMark=50, Courses = new int[]{1,5}, Credits=2 },
					new Requirement{Id = 106, Name = "Liberal Arts", MinimumMark=50, Courses = new int[]{1,5,6}, Credits=2 },
				};
		}
	}
}

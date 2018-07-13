using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using GraduationTracker.DAL.Interfaces;

namespace GraduationTracker.Tests.Unit
{
	[TestClass]
	public class GraduationTrackerTests
	{
		private IGraduationTracker mock_repository = new MockRepository();

		//Standings calculated as expected: 
		//all standing types calculated as expected with homogeneous marks
		//standing type calculated correctly with varying marks (inc 0, 100) - average comes to decimal
		//standing type calculated all 0s
		//standing type invalid over 100
		//standing type invalid under 0
		//student has high grades in non-requirement courses but remedial grades in required courses

		[TestMethod]
		public void WhenStudentIsRemedial_StandingIsRemedial()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.remedial;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.Remedial, graduationStatus.Standing);
		}

		[TestMethod]
		public void WhenStudentIsAverage_StandingIsAverage()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.average;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.Average, graduationStatus.Standing);
		}

		[TestMethod]
		public void WhenStudentIsMagnaCumLaude_StandingIsMagnaCumLaude()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.magnaCumLaude;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.MagnaCumLaude, graduationStatus.Standing);
		}

		[TestMethod]
		public void WhenStudentIsSummaCumLaudee_StandingIsSummaCumLaude()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.summaCumLaude;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.SummaCumLaude, graduationStatus.Standing);
		}

		[TestMethod]
		public void WhenStudentHasVaryingGrades_StandingIsAverage()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.varyingGrades;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.Average, graduationStatus.Standing);
		}


		[TestMethod]
		public void WhenStudentHasAllZeros_StandingIsRemedial()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.allZeroGrades;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.Remedial, graduationStatus.Standing);
		}



		[TestMethod]
		public void WhenStudentIsRemedialOnlyInRequiredCourses_StandingIsRemedial()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.remedialInRequirementCoursesOnly;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.AreEqual(Standing.Remedial, graduationStatus.Standing);
		}

		//Successful graduation scenarios:
		//passing standings
		//all met requirements

		[TestMethod]
		public void WhenStudentIsAverage_HasGraduatedIsTrue()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.average;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsTrue(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentIsMagnaCumLaude_HasGraduatedIsTrue()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.magnaCumLaude;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsTrue(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentIsSummaCumLaude_HasGraduatedIsTrue()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.summaCumLaude;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsTrue(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentHasEnoughCreditsToMeetRequirementButNotAll_HasGraduatedIsTrue()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.enoughCreditsToMeetRequirementButNotAll;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.hasThreeCreditRequirementNeedTwo;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsTrue(graduationStatus.HasGraduated);
		}

		//Failing graduation scenarios: 
		//student with remedial standing
		//student with a high average failing one requirement
		//student with high grades in non-requirements but remedial in required
		//student missing required courses does not graduate
		//student with one course less than requirement credit does not graduate
		//student with repeated passing requirement course does not graduate
		//student who would need to re-use credit to meet a requirment does not graduate
		[TestMethod]
		public void WhenStudentIsRemedial_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.remedial;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentHasHighGradesFailingOneRequirement_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.highAverageFailsRequirement;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentHasLowGradesInRequirementCoursesOnly_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.remedialInRequirementCoursesOnly;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentIsMissingRequirementCourses_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.missingCourses;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentFailingOneRequirementCredit_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.failingOneRequirementCredit;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.hasTwoCreditRequirementNeedBoth;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}


		[TestMethod]
		public void WhenStudentCantReuseRequirementCreditToMeetAnotherRequirement_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.wouldNeedToReuseARequirementCredit;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.requiresRepeatCourseCredit;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentMissingOneRequirementCredit_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.missingOneRequirementCredit;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.hasTwoCreditRequirementNeedBoth;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		[TestMethod]
		public void WhenStudentRepeatedRequirementCourse_HasGraduatedIsFalse()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.repeatedCourse;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.hasTwoCreditRequirementNeedBoth;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
			Assert.IsFalse(graduationStatus.HasGraduated);
		}

		//exceptions
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void WhenStudentAverageOver100_StandingIsNone()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.over100;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void WhenStudentAverageUnderZero_StandingIsNone()
		{
			GraduationTracker tracker = new GraduationTracker(mock_repository);
			int testStudentId = (int)TestStudents.TestStudentIds.averageUnderZero;
			Student student = mock_repository.GetStudent(testStudentId);
			int testDiplomaId = (int)TestDiplomas.TestDiplomaIds.singleCourseRequirements;
			Diploma diploma = mock_repository.GetDiploma(testDiplomaId);

			GraduationStatus graduationStatus = tracker.HasGraduated(diploma, student);
		}
	}

}

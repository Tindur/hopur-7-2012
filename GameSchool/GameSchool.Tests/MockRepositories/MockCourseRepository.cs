using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;
using GameSchool.Models;

namespace GameSchool.Tests.MockRepositories
{
    /*public class MockCourseRepository : ICourseRepository
    {
        public List<CourseModel> testRep;
        public IUsersRepository userTestRep;
        public List<MockCourseRegistration> regTestRep;

        public MockCourseRepository(int count)
        {
            for (int i = 0; i < count; i++)
            {
                testRep.Add(new CourseModel
                {
                    ID = i + 1,
                    Description = "Herp Derp " + (i + 1),
                    Name = "Test Course " + (i + 1)
                });
            }

            userTestRep = new MockUserRepository(100);
        }

        public IQueryable<Models.dbLINQ.CourseModel> GetAllCourses()
        {
            return testRep.AsQueryable();
        }

        public IQueryable<Models.dbLINQ.CourseModel> GetCoursesForStudent(string studentUsername)
        {
            List<CourseModel> rep = new List<CourseModel>();
            for (int i = 1; i < 6; i++)
            {
                rep.Add (new CourseModel 
                { 
                    ID = i,
                    Name = "Course for Student " + i,
                    Description = "Wee",
                    
                });
            }

            return rep.AsQueryable();
        }

        public Models.dbLINQ.CourseModel GetCourseById(int id)
        {
            var result = (from course in testRep
                         where course.ID == id
                         select course).SingleOrDefault();

            return result;
        }

        public void AddCourse(Models.dbLINQ.CourseModel Course)
        {
            Course.ID = testRep.Count + 1;
            testRep.Add(Course);
        }

        public void UpdateCourse(int id, Models.dbLINQ.CourseRegistration registration)
        {
            /*var result = (from course in testRep
                         where course.ID == id
                         select course).SingleOrDefault();
            
            //TODO: Finna út úr því hvað þetta fall á að gera og útfæra
        }

        public void Save()
        {
            //Þarf að útfæra? kv Björn
            return;
        }

        public void RegisterStudentToCourse(string StudentUsername, int courseID)
        {
            regTestRep.Add(new MockCourseRegistration
            {
                StudentName = StudentUsername, 
                CourseID = courseID
            });
        }
        CoursesDBDataContext m_courseDB = new CoursesDBDataContext();
        public IQueryable<TeacherRegistration> GetTeachersForCourse(int courseID)
        {
            var result = from Teacher in m_courseDB.TeacherRegistrations
                         where Teacher.CourseID == courseID
                         select Teacher;
            return result;
        }
    }

    public class MockCourseRegistration
    {
        public string StudentName { get; set; }
        public int CourseID { get; set; }

        public MockCourseRegistration()
        { }

        public MockCourseRegistration(MockCourseRepository crep, MockUserRepository urep)
        {
            foreach (var course in crep.GetAllCourses())
            {
                int i;
                for (i = 0; i < urep.GetAllStudents().ToList().Count; i += 7 )
                {
                    this.CourseID = course.ID;
                    this.StudentName = urep.testUsersRep.ElementAt(i).UserName;
                }
                i++;
            }
        }
    }*/
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameSchool.Models.Interfaces;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Tests.MockRepositories
{
    public class MockUserRepository : IUsersRepository
    {
        public List<aspnet_User> testUsersRep;

        public MockUserRepository(int count)
        {
            testUsersRep = new List<aspnet_User>();
            for (int i = 0; i < count; i++)
            {
                testUsersRep.Add(new aspnet_User
                {
                    UserId = Guid.NewGuid(),
                    UserName = "Bob " + (i + 1),
                    ApplicationId = new Guid("791d26fd-0911-46a1-a3b3-1a51caf75ffc"),
                    LoweredUserName = "bob " + (i + 1),
                    IsAnonymous = false,
                    LastActivityDate = DateTime.Now.AddDays(i)
                });
            }
        }


        public IQueryable<Models.dbLINQ.aspnet_User> GetAllStudents()
        {
            return testUsersRep.AsQueryable();
        }

        public Models.dbLINQ.aspnet_User GetUserById(string id)
        {
            var result = (from user in testUsersRep
                          where user.UserId.ToString() == id
                          select user).SingleOrDefault();

            return result;
        }

        public void AddUser(Models.dbLINQ.aspnet_User User)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.dbLINQ.aspnet_User> GetAllTeachers()
        {
            throw new NotImplementedException();
        }
    }
}

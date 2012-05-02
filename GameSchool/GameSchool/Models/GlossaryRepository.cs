using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameSchool.Models
{
    public class GlossaryRepository : IGlossaryRepository
    {
        GlossaryDBDataContext m_glossaryDB = new GlossaryDBDataContext();

        public List<GlossaryModel> GetGlossaryForCourse(int? id)
        {
            throw new NotImplementedException();
        }

        public void AddGlossaryForCourse(int? id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUpvotes(GlossaryModel glossary)
        {
            throw new NotImplementedException();
        }

        public void UpdateDownvotes(GlossaryModel glossary)
        {
            throw new NotImplementedException();
        }
    }
}
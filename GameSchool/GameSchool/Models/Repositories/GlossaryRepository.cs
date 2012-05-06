using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameSchool.Models.dbLINQ;
using GameSchool.Models.Interfaces;

namespace GameSchool.Models
{
    public class GlossaryRepository : IGlossaryRepository
    {
        GlossaryDBDataContext m_glossaryDB = new GlossaryDBDataContext();

        public List<GlossaryModel> GetGlossaryForCourse(int? id)
        {
            if (id.HasValue)
            {
                var result = from x in m_glossaryDB.GlossaryModels
                             where x.CouseID == id.Value
                             select x;
                return result.ToList();

            }
            else
            {
                return (new List<GlossaryModel>());
            }

            //throw new NotImplementedException();
        }

        public void AddGlossaryForCourse(GlossaryModel NewGlossary)
        {
            NewGlossary.DatePublished = DateTime.Now;

            m_glossaryDB.GlossaryModels.InsertOnSubmit(NewGlossary);
            m_glossaryDB.SubmitChanges();
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
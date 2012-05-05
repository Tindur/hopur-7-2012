using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameSchool.Models.dbLINQ;

namespace GameSchool.Models
{
    public interface IGlossaryRepository
    {
        List<GlossaryModel> GetGlossaryForCourse(int? id);
        void AddGlossaryForCourse(int? id);
        void UpdateUpvotes(GlossaryModel glossary);
        void UpdateDownvotes(GlossaryModel glossary);
    }
}

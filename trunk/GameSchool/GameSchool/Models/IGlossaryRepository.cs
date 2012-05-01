using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSchool.Models
{
    interface IGlossaryRepository
    {
        List<GlossaryModel> GetGlossaryForCourse(int? id);
        void AddGlossaryForCourse(int? id);
        void UpdateUpvotes(GlossaryModel glossary);
        void UpdateDownvotes(GlossaryModel glossary);
    }
}

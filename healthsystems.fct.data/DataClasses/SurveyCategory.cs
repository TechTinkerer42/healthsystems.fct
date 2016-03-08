using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthsystems.fct.data.DataClasses
{
    public class SurveyCategory
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
    }
}

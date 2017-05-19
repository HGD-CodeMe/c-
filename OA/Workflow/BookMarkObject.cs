using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow
{
    public class BookMarkObject<T>
    {
        //封装参数
        public string BookMarkName { get; set; }
        public int StepId { get; set; }
        public T Result { get; set; }
    }
}

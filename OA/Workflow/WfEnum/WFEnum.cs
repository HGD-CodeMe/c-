using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.WfEnum
{
    public enum WFEnum
    {
        /// <summary>
        /// 通过
        /// </summary>
        IsPass = 0,
        /// <summary>
        /// 继续
        /// </summary>
        IsContinue = 1,
        /// <summary>
        /// 驳回
        /// </summary>
        IsBack = 2
    }
}

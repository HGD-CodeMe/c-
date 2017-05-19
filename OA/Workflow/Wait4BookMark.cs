using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Workflow;

namespace Workflow
{

    public sealed class Wait4BookMark<T> : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InOutArgument<string> BookMarkName { get; set; }

        //输入每一个步骤的编号
        public OutArgument<int> SetId { get; set; }

        public OutArgument<T> Result { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            string bookMarkName = context.GetValue(BookMarkName);
            context.CreateBookmark(bookMarkName, continueExecute);
        }

        //获得参数继续执行
        public void continueExecute(NativeActivityContext context,Bookmark bookmark,object value)
        {
            var data = (BookMarkObject<T>)value;
            context.SetValue(BookMarkName, data.BookMarkName);
            context.SetValue(SetId,data.StepId);
            context.SetValue(Result, data.Result);
        }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        //protected override void Execute(CodeActivityContext context)
        //{
        //    // 获取 Text 输入参数的运行时值
        //    string text = context.GetValue(this.Text);
        //}
    }
}

using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Workflow
{
    public class WorkflowApplicationHelper
    {
        public static void CreateWorkflow(Activity activity,IDictionary<string,object> dict,out Guid guid)
        {
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            string connStr = 
                ConfigurationManager.ConnectionStrings["OAEntities"].ConnectionString;

            //工作流持久化
            SqlWorkflowInstanceStore sqlStore = new SqlWorkflowInstanceStore(connStr);

            WorkflowApplication wfApp = new WorkflowApplication(activity, dict);
            wfApp.InstanceStore = sqlStore;//完成持久化

            //开启新的线程执行工作流
            wfApp.Run();

            guid = wfApp.Id;
            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                Console.WriteLine("工作流终止！！");
                syncEvent.Set();
            };

            wfApp.Idle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                Console.WriteLine("工作流空闲！！");
                syncEvent.Set();
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                Console.WriteLine("工作流持久化！！");
                syncEvent.Set();
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded = delegate (WorkflowApplicationEventArgs e)
            {
                Console.WriteLine("工作流卸载！！");
                syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                Console.WriteLine("工作流异常！！");
                syncEvent.Set();
                return UnhandledExceptionAction.Abort;
            };
            syncEvent.WaitOne();//主线程停止了
        }

        public static WorkflowApplication LoadWorkflow(Activity activity,Guid guid, AutoResetEvent syncEvent)
        {
            
            string connStr =
                ConfigurationManager.ConnectionStrings["OAEntities"].ConnectionString;

            //工作流持久化
            SqlWorkflowInstanceStore sqlStore = new SqlWorkflowInstanceStore(connStr);

            WorkflowApplication wfApp = new WorkflowApplication(activity);
            wfApp.InstanceStore = sqlStore;//完成持久化

            //开启新的线程执行工作流
            wfApp.Run();

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                Console.WriteLine("工作流完成！！");
                syncEvent.Set();
            };

            wfApp.Aborted = delegate (WorkflowApplicationAbortedEventArgs e)
            {
                Console.WriteLine("工作流终止！！");
                syncEvent.Set();
            };

            wfApp.Idle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                Console.WriteLine("工作流空闲！！");
                syncEvent.Set();
            };

            wfApp.PersistableIdle = delegate (WorkflowApplicationIdleEventArgs e)
            {
                Console.WriteLine("工作流持久化！！");
                syncEvent.Set();
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded = delegate (WorkflowApplicationEventArgs e)
            {
                Console.WriteLine("工作流卸载！！");
                syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate (WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                Console.WriteLine("工作流异常！！");
                syncEvent.Set();
                return UnhandledExceptionAction.Abort;
            };
            wfApp.Load(guid);
            return wfApp;
        }
    }
}

using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using GameCore;

namespace HuaXueSha
{

    class Program
    {
        static void Main(string[] args)
        {

            GameContextConsole gCtx = new GameContextConsole();
            
            Dictionary<string, object> wfArgs = new Dictionary<string, object>();
            wfArgs.Add("ctx", gCtx);
            
            Activity workflow1 = new GameMain();
            WorkflowInvoker.Invoke(workflow1,wfArgs);
        }
    }
}

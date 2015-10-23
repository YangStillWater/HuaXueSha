using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameCore;

namespace WindowsForms
{
    static class Program
    {
        public static Form1 frm1;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frm1 = new Form1();
            string url = "http://localhost:8080";
            WebApp.Start(url);

            Application.Run(frm1);
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        public override Task OnConnected()
        {
            Program.frm1.playerlist.Add(new Player() { ConnectionID = Context.ConnectionId });
            return base.OnConnected();
        }
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }
    }
}

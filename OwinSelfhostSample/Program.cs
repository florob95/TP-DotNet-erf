using System;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using System.Reflection;
using MongoDB;

namespace OwinSelfhostSample
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";
            //Singleton.Instance.setMongo(new Mongo());
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}

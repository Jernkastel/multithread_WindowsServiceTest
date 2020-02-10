using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace APR400_lab4_max_jern_WindowsService
{
    public partial class TestService : ServiceBase
    {
        
        //Text file folder location
        private static string filePath = @"INSERT FOLDER PATH HERE";
        //Text file name + final location
        private static string truePath = filePath + @"\test.txt";

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
        public TestService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }
        //Defines OnStart behavior
        //Prints timestamp to document whenever the service was started
        protected override void OnStart(string[] args)
        {
            //Update state to 'pending'
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            eventLog1.WriteEntry("In OnStart.");

            //Set timer
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            CreateText("started");

            //Update state to 'running'
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

        }
        //Prints timestamp to document whenever the service was stopped
        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
            CreateText("stopped");
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            Directory.CreateDirectory(filePath);
        }

        public void CreateText(string text)
        {
            //If no folder exists - create one
            Directory.CreateDirectory(filePath);
            string dateTime = DateTime.Now.ToString();

            FileStream fs = new FileStream(truePath, FileMode.Append, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write("Service {0}: {1}\n", text, dateTime);
            w.Flush();
            fs.Close();
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

        //Implementing service pending status
        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
    }
}

using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace lightControlService
{
    public partial class lightControlService : ServiceBase
    {
        public lightControlService()
        {
            InitializeComponent();
            CanHandlePowerEvent = true;
            CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            using (System.IO.StreamWriter file = File.AppendText(@"C:\Users\Ryan\Desktop\serviceLog.txt"))
            {
                file.WriteLine("OnStart");
            }
            SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            try
            {
                port.Open();
            }
            catch (Exception e)
            {
                return;
            }
            port.WriteLine("Wake");
            try
            {
                port.Close();
            }
            catch
            {
                return;
            }
            return;

        }

        protected override void OnStop()
        {
            using (System.IO.StreamWriter file = File.AppendText(@"C:\Users\Ryan\Desktop\serviceLog.txt"))
            {
                file.WriteLine("OnStop");
            }
            SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            try
            {
                port.Open();
            }
            catch (Exception e)
            {
                return;
            }
            port.WriteLine("Sleep");
            try
            {
                port.Close();
            }
            catch
            {
                return;
            }
            return;
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            if (powerStatus == PowerBroadcastStatus.Suspend || powerStatus == PowerBroadcastStatus.ResumeSuspend || powerStatus == PowerBroadcastStatus.ResumeAutomatic)
            {
                SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
                try
                {
                    port.Open();
                }
                catch (Exception e)
                {
                    return true;
                }

                if (powerStatus == PowerBroadcastStatus.Suspend)
                {
                    port.WriteLine("Sleep");
                }
                else
                {
                    port.WriteLine("Wake");
                }
                try
                {
                    port.Close();
                }
                catch
                {
                    return true;
                }
            }
            return true;
        }
    }
}

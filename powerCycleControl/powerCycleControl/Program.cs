using System;
using System.IO.Ports;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace powerCycleControl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Read();
            bool on = Properties.Settings.Default.powerOn;
         
            SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            port.Open();

            if (on == true)
            {
                port.WriteLine("Sleep");
                Properties.Settings.Default.powerOn = false;
            }
            else
            {
                port.WriteLine("Wake");
                Properties.Settings.Default.powerOn = true;
            }
            Properties.Settings.Default.Save();
            port.Close();
        }
    }
}

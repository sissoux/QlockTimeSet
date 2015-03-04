using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;


namespace Clock_Setting
{
    public class ArduinoCom
    {
        private SerialPort ArduinoPort = new SerialPort();
        
        public String ID;

        public String Port
        {
            get { return ArduinoPort.PortName; }
            set { ArduinoPort.PortName = value; }
        }

        private MessageType SetTime = new MessageType();

        public ArduinoCom(String ID)
        {
            this.ID = ID;
        }
        
    }
}

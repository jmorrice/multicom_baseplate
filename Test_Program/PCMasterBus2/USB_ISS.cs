using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace PCMasterBus2
{
    public class USB_ISS
    {
        public string Status;
        private SerialPort serialPort = new SerialPort();
        private Byte[] command = new Byte[40];
        private Byte[] response = new Byte[40];

        public bool Init (string portname)
        {
            try
            {
                serialPort.Close();
                serialPort.PortName = portname;
                serialPort.Parity = 0;
                serialPort.BaudRate = 19200;
                serialPort.StopBits = StopBits.Two;
                serialPort.DataBits = 8;
                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;
                serialPort.Open();
            }
            catch
            {
                // error on init com port
                Status = "Port Fehler";
                return(false);
            }
            // set usb_iss to i2C 400kHz and get Status
            command[0] = 0x5a;  // command usb iss
            command[1] = 0x02;  // sub command iss mode
            command[2] = 0x40;  // operation mode I2C_H_400_khz
            command[3] = 0x00;  // io mode output low
            if (!SendUSB_ISS(4, 2) || response[0] != 0xff)
            {
                Status = "I2C Init Fehler";
                return(false);
            }

            // get usb iss status
            command[0] = 0x5a;  // command usb iss
            command[1] = 0x01;  // sub command version
            if (!SendUSB_ISS(2, 3) || response[0] != 0x07)
            {
                Status = "I2C Version Fehler";
                return (false);
            }

            Status = "I2C Firmware Ver.: " + response[1].ToString("D") + " Modus: ";
            switch(response[2])
            {
                case 0x20: Status = Status + "I2C 20kHz Software"; break;
                case 0x40: Status = Status + "I2C 100kHz Software"; break;
                case 0x50: Status = Status + "I2C 400kHz Software"; break;
                case 0x60: Status = Status + "I2C 100kHz Hardware"; break;
                case 0x70:  Status = Status + "I2C 400kHz Hardware"; break;
                default:    Status = Status + "unbekannt"; break;
            }
            return (true);
        }


        public bool SetBank(int bank, UInt16 port)
        {
            int adresse = 0;
            port &= 0x0fff;
            switch (bank)
            {
                case 1: adresse = 0; port |= 0x2000; break;
                case 2: adresse = 1; port |= 0x4000; break;
                case 3: adresse = 2; port |= 0x6000; break;
                case 4: adresse = 3; port |= 0x0000; break;
            }
            return (PCA8575_SetOutput(adresse, port));
        }


        public bool PCA8575_SetOutput(int adresse, UInt16 port)
        {
            adresse = ((adresse & 0x03) | 0x20) << 1;   // adresse set to PCA8575 
            command[0] = 0x54;                          // command I2C_AD0
            command[1] = (Byte)adresse;                 // adress with bit0 write
            command[2] = 0x02;                          // 2 byte to send
            command[3] = (Byte)(port & 0xff);           // low byte;
            command[4] = (Byte)(port >> 8);             // high Byte;

            if (!SendUSB_ISS(5, 1) || response[0] == 0x00)
                return (false);

            return (true);
        }

        public bool Send_Generalcall (byte value, byte register)
        {
            bool result;
            command[0] = 0x55;                          // command I2C_AD1
            command[1] = 0;                 // adress with bit0 write
            command[2] = register;  
            command[3] = 0x01;                          // 2 byte to send
            command[4] = (Byte)value;           // low byte;

            if (!SendUSB_ISS(5, 1) || response[0] == 0x00)
                result = false;
            else
                result = true;

            return (result);
        }

        public bool Send_Byte(byte address, byte register, byte[] data)
        {
            command[0] = 0x55;                          // command I2C_AD1
            command[1] = Convert.ToByte(address << 1);                 // adress with bit0 write
            command[2] = register;
            command[3] = Convert.ToByte(data.Length);                          // 2 byte to send

            int i;

            for(i = 0; i < data.Length; i++)
                command[4 + i] = data[i];           // low byte;

            if (!SendUSB_ISS(4+i, 1) || response[0] == 0x00)
                return (false);

            return (true);
        }

        public bool Send_SingleByte(byte address, byte data)
        {
            command[0] = 0x53;
            command[1] = Convert.ToByte(address << 1);   // adress with bit0 write
            command[2] = data;

            if (!SendUSB_ISS(3,1) || response[0] == 0x00)
                return (false);
            return (true);
        }

        public byte[] ReadByte(byte address, byte register, byte no_of_bytes)
        {
            byte[] result = new byte[no_of_bytes];
            command[0] = 0x55;                          // command I2C_AD1
            command[1] = Convert.ToByte((address << 1) | 1) ;                 // adress with bit0 write
            command[2] = register;
            command[3] = no_of_bytes;                          // 2 byte to send

            if (!SendUSB_ISS(4, no_of_bytes))
                result = null;
            else
                result = response;

            return(result);
        }

        bool SendUSB_ISS (int count, int answer)
        {
            bool result = true;

            for (int i = 0; i < response.Length; i++)
                response[i] = 0;

            try
            {
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                serialPort.Write(command, 0, count);
                //Thread.Sleep(500);
                while (serialPort.BytesToRead != answer);
                serialPort.Read(response, 0, answer);
            }
            catch
            {
                result = false;
            }

            for (int i = 0; i < command.Length; i++)
                command[i] = 0;

            return (result);
        }

    }
}

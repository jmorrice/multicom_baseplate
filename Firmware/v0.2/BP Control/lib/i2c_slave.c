/********************************************************************************
 *    Bibliothek: i2c_slave.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Initialisiert den Controller als Slave.
 *                   
 *    Hardware: Standard I²C Verbindung über I²C1 (P0.19 und P0.20)     
 *
 ********************************************************************************/

#include "incl.h"

 
struct i2c I2C0_CNTRL;
struct command_status COMMAND_CNTRL;
extern enum CELL_STAT CELL_STATUS;


/*************************************************************************
 *    Funktion: i2c_slave_init()
 *
 *    Beschreibung: - Initialisert alle notwendigen I/Os.
 *                  - Stellt den Peripherietakt (PCLKI2C0) auf 50 MHz.
 *                  - Stellt die I²C Übertragungsrate auf 100 KHz.
 *                  - Aktiviert den Slave-Modus.
 *                  - Aktiviert den Interrupt.
 *
 *
 **************************************************************************/

void i2c_slave_init (void)
{
  FIO0DIR_bit.P0_2=0;           //Addresspin Input
  
  PCLKSEL0_bit.PCLK_I2C0=2;     //PCLKI2C0 = CCLK/2=50MHz
  
  PINSEL1_bit.P0_27=1;          // Select SDA and SCL function                         
  PINSEL1_bit.P0_28=1;
  
  PINMODE1_bit.P0_27=2;         //Neither pull up or pull down
  PINMODE1_bit.P0_28=2;
  
  I2C0SCLH=251;                 //I2C frequency= PCLKI2C / (I2CSCLH + I2CSCLL) --> I2C frequency= 50Mhz / 500= 100 KHz  
  I2C0SCLL=249;                 // I2SCLL and I2SCLH values should not necessarily be the same.
 
 
  I2C0CONSET=0x44;              // Set AA and EN bit at the same time enables slave mode.
  
  
  I2C0_CNTRL.ReceiveSize=4100;     //init flags and parameters
  I2C0_CNTRL.Error_ID=ok;
  I2C0_CNTRL.Busy=free;
  
        clear_command();

  SETENA0_bit.SETENA10=1;       //enable I2C0 interrupt
}

/*************************************************************************
 *    Funktion: get_address()
 *
 *    Beschreibung: - Initialisert die Address Pins.
 *                  - Wenn default_address= 0, dann wartet die Funktion auf eine Addresszuweisung.
 *                  - Wenn default_address !=0, dann wird der wert von default_address übernommen.
 *
 *
 **************************************************************************/

void get_address(unsigned char default_address)
{  
  if(I2C0_CNTRL.address==0)
  {
      if(default_address!=0)
      {
        I2C0ADR1=default_address<<1|1;
        I2C0_CNTRL.address=1;
        //while(FIO0PIN_bit.P0_2);
        LED1(LED_ON);
      }

      else
      {
        do
        {
          if(COMMAND_CNTRL.command_received==1) 
          {
            
            if(COMMAND_CNTRL.Kommando==receive_address && FIO0PIN_bit.P0_2)
            {
                I2C0ADR=COMMAND_CNTRL.param[0]<<1|1;
                  LED1(LED_ON);
                I2C0_CNTRL.address=1;
            }            
            clear_command();
          }
        }while(!I2C0_CNTRL.address);
      }
        //set AddrOut
      FIO0PIN_bit.P0_3 = 1;
      LED2_BLINK(2);//COMMAND_CNTRL.param[0]);
  }
}

/*************************************************************************
 *    Funktion: I2C0_IRQHandler
 *
 *    Beschreibung: Interrupt-Handler für den I²C0 Interrupt.
 *
 *
 **************************************************************************/


void I2C0_IRQHandler (void)
{   
   
    i2c_slave_statehandler(I2C0STAT&0xF8);
    
}

/*************************************************************************
 *    Funktion: i2c_slave_statehandler
 *
 *    Beschreibung: Wickelt die I²C Kommunikation ab.
 *
 *
 **************************************************************************/

void i2c_slave_statehandler (unsigned char state)
{
    
    
    switch (state)
    {
      
       case 0x60:                      // Own Slave Address + Write has been received, ACK has been returned.
      I2C0_CNTRL.Busy=busy;            // Data will be received and ACK returned.               
      I2C0_CNTRL.ReceiveCounter=0;
      I2C0CONSET=0x04;
      I2C0CONCLR=0x08;
      break;
    
      
      case 0x68:                      // Arbitration has been lost in Slave Address and R/W bit as bus Master.
                                      // Own Slave Address+ Write has been received, ACK has been returned.
      I2C0_CNTRL.ReceiveCounter=0;    // Data will be received and ACK will be returned. 
      I2C0CONSET=0x24;                // STA is set to restart Master mode after the bus is free again.
      I2C0CONCLR=0x08;
      break;                               
    
    case 0x70:                        // General Call has been received, ACK has been returned.
      I2C0_CNTRL.Busy=busy;           // Data will be received and ACK returned.
      I2C0_CNTRL.ReceiveCounter=0;    
      I2C0CONSET=0x04;
      I2C0CONCLR=0x08;
      break;
      
    case 0x78:                        // Arbitration has been lost in Slave Address + R/W bit as bus Master.          
                                      // General Call has been received and ACK has been returned.
      I2C0_CNTRL.ReceiveCounter=0;    // Data will be received and ACK returned. STA is set to restart Master mode after the bus is free again.
      I2C0CONSET=0x24;                // STA is set to restart Master mode after the bus is free again.
      I2C0CONCLR=0x08;
      break;
      
    case 0x80:                        // Previously addressed with own Slave Address.
                                      // Data has been received and ACK has been returned. Additional data will be read.                             
       if (I2C0_CNTRL.ReceiveCounter < I2C0_CNTRL.ReceiveSize)
      {
        I2C0_CNTRL.ReceiveBuffer[I2C0_CNTRL.ReceiveCounter]=I2C0DAT;
        I2C0_CNTRL.ReceiveCounter++;
        //I2C2CONSET=0x04;  
        I2C0CONCLR=0x08;
      }
    
      else 
      {
        I2C0CONCLR=0x0C;
      }
      break;
      
      case 0x90:                        // Previously addressed with own Slave Address.
                                      // Data has been received and ACK has been returned. Additional data will be read.                             
       if (I2C0_CNTRL.ReceiveCounter < I2C0_CNTRL.ReceiveSize)
      {
        I2C0_CNTRL.ReceiveBuffer[I2C0_CNTRL.ReceiveCounter]=I2C0DAT;
        I2C0_CNTRL.ReceiveCounter++;
        //I2C2CONSET=0x04;  
        I2C0CONCLR=0x08;
      }
    
      else 
      {
        I2C0CONCLR=0x0C;
      }
      break;
      
      
    case 0xA0:                      // A STOP condition or repeated START has been received, while still addressed as a Slave.
                                    //  Data will not be saved. Not addressed Slave mode is entered.     
      
        
        COMMAND_CNTRL.Kommando=I2C0_CNTRL.ReceiveBuffer[0];
       /* if(COMMAND_CNTRL.Kommando==flash)
        {
          for(char i=0;i<50;i++)
          {
            IAP_CNTRL.software[i]=I2C0_CNTRL.ReceiveBuffer[i+1];
          }
        }*/
        
        for(char i=0;i<6;i++)
        {
          COMMAND_CNTRL.param[i]=I2C0_CNTRL.ReceiveBuffer[i+1];
        }
      COMMAND_CNTRL.command_received=1;
      I2C0_CNTRL.ReceiveCounter=0;
      I2C0_CNTRL.Busy=free; 
      //I2C2CONSET=0x04;  
      I2C0CONCLR=0x08;
      break;
      
    case 0xA8:                      // Own Slave Address + Read has been received, ACK has been returned. 
                                    //Data will be transmitted, ACK bit will be received.
        I2C0_CNTRL.Busy=busy; 
        I2C0_CNTRL.SendCounter=0;
        I2C0DAT=I2C0_CNTRL.SendBuffer[I2C0_CNTRL.SendCounter];
        I2C0_CNTRL.SendCounter++;
        //I2C2CONSET=0x04;  
        I2C0CONCLR=0x08;
      
      break;
      
    case 0xB8:                    // Data has been transmitted, ACK has been received. 
                                  // Data will be transmitted, ACK bit will be received.
      if (I2C0_CNTRL.SendCounter==0)
       {
         I2C0DAT=I2C0_CNTRL.SendBuffer[I2C0_CNTRL.SendCounter];
         I2C0_CNTRL.SendCounter++;
         //I2C2CONSET=0x04;  
         I2C0CONCLR=0x08;
       }
                  
       else if (I2C0_CNTRL.SendCounter < I2C0_CNTRL.SendSize && I2C0_CNTRL.SendCounter > 0)
       {
         I2C0DAT=I2C0_CNTRL.SendBuffer[I2C0_CNTRL.SendCounter];
         I2C0_CNTRL.SendCounter++;
         //I2C2CONSET=0x04;   
         I2C0CONCLR=0x08;
       }
      
      else if(I2C0_CNTRL.SendCounter == I2C0_CNTRL.SendSize)
      {
        I2C0_CNTRL.Busy=free;
        //I2C2CONSET=0x04;   
         I2C0CONCLR=0x08;
      }
      
      else if (I2C0_CNTRL.SendSize < I2C0_CNTRL.SendCounter)
      {
        I2C0_CNTRL.Error_ID= send_fail;
        I2C0CONCLR=0x08;
      }
      break;
      
    case 0xC0:                  // Data has been transmitted, NOT ACK has been received. 
      I2C0_CNTRL.Busy=free;                          // Not addressed Slave mode is entered.
      I2C0CONSET_bit.AA=1;
      I2C0CONCLR=0x08;
      break;
      
    case 0xC8:                  // The last data byte has been transmitted, ACK has been received. 
      I2C0_CNTRL.Busy=free;     // Not addressed Slave mode is entered.
      I2C0CONSET_bit.AA=1;
      I2C0CONCLR=0x08;
      break;
      
    default:
      
        I2C0_CNTRL.Error_ID= unknown_state;
        I2C0CONCLR=0x08;
      break;
    }
}


/*************************************************************************
 *    Funktion: clear_command
 *
 *    Beschreibung: Löscht den Kommandospeicher.
 *
 *
 **************************************************************************/


void clear_command(void)
{
  COMMAND_CNTRL.command_received=0;
  COMMAND_CNTRL.Kommando=0;
}
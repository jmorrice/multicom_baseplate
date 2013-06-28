/********************************************************************************
 *    Bibliothek: i2c_master.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Initialisiert den Controller als Master.
 * 
 *    Hardware: Standard I²C Verbindung über I²C2 (P0.10 und P0.11)                      
 *    
 ********************************************************************************/

#include "incl.h"


struct i2c I2C2_CNTRL;
int I2C2_TIMEOUT = I2C_Timeout;



/*************************************************************************
 *    Funktion: i2c_init()
 *
 *    Beschreibung: - Initialisert alle notwendigen I/Os.
 *                  - Stellt den Peripherietakt (PCLKI2C2) auf 50 MHz.
 *                  - Stellt die I²C Übertragungsrate auf 100 KHz.
 *                  - Aktiviert den Master-Modus.
 *                  - Aktiviert den Interrupt.
 *
 *
 **************************************************************************/

void i2c_init(void)
{
    I2C2CONSET_bit.I2EN=0;
    PCLKSEL1_bit.PCLK_I2C2=2;                 //PCLK = CCLK/2=50MHz
    
    PINSEL0_bit.P0_10 = 2;
    PINSEL0_bit.P0_11 = 2;                    //I2C Pinfunction 
    
    PINMODE0_bit.P0_10=2;                     // No Pull Up or Pull Down.
    PINMODE0_bit.P0_11=2; 
    
    PINMODE_OD0_bit.P0_10=1;                  // Open Drain 
    PINMODE_OD0_bit.P0_11=1;
    
    I2C2CONSET_bit.I2EN=1;                    //Enable I2C and Master-Mode
    
    I2C2SCLH=251;                             //I2C frequency= PCLKI2C / (I2CSCLH + I2CSCLL) --> I2C frequency= 50Mhz / 500= 100 KHz  
    I2C2SCLL=249;                             // I2SCLL and I2SCLH values should not necessarily be the same.

   // I2C2CONCLR=0x08;
    
    I2C2_CNTRL.Error_ID=ok;                   //init flags and parameters
    I2C2_CNTRL.Busy=free;
    
          clear_command();
    
    SETENA0_bit.SETENA12=0;                    //I2C interrupt deaktivieren.(Polled Mode)
}

/*************************************************************************
 *    Funktion: i2c_get_status()
 *
 *    Beschreibung: - Gibt das busyflag zurück.
 *                  - Wird verwendet, um zu prüfen, ob der Bus noch verwendet wird
 *                  
 *
 *
 **************************************************************************/

unsigned char i2c_get_status(void)
{

    return I2C2_CNTRL.Busy;
}

/*************************************************************************
 *    Funktion: i2c_get_data()
 *
 *    Beschreibung: - Liest empfangene Daten und schreibt sie in den Receive Buffer.
 *                 
 *                  
 **************************************************************************/

enum error i2c_get_data(unsigned char *data)
{
      if(I2C2_CNTRL.Data==1)
    {
        if( I2C2_CNTRL.ReceiveSize > 1)
        {
          for(unsigned char i=0; i < I2C2_CNTRL.ReceiveSize;i++)
          {
            data[i]=I2C2_CNTRL.ReceiveBuffer[i];
          }
        }
        
        else
        {
            data[0]=I2C2_CNTRL.ReceiveBuffer[0];
        }
        
      I2C2_CNTRL.Data=0;
      I2C2_CNTRL.Error_ID=ok;
    }
    
    else
    {
       I2C2_CNTRL.Error_ID=no_data;
       return I2C2_CNTRL.Error_ID;
    }
    
    
    return I2C2_CNTRL.Error_ID; 
}

/*************************************************************************
 *    Funktion: I2C2_IRQHandler
 *
 *    Beschreibung: Interrupt-Handler für den I²C2 Interrupt.
 *
 *
 **************************************************************************/

void I2C2_IRQHandler (void)
{   
   
    i2c_statehandler(I2C2STAT&0xF8);
    
}


/*************************************************************************
 *    Funktion: i2c_statehandler
 *
 *    Beschreibung: Wickelt die I²C Kommunikation ab.
 *
 *
 **************************************************************************/


void i2c_statehandler (unsigned char state)
{
    
    switch (state)
    {
     case 0x00:            // Bus Error. Enter not addressed Slave mode and release bus.                           
       I2C2CONSET=0x14;
       I2C2CONCLR=0x08;
      break;
      
     case 0x08:           // A (repeated) START condition has been transmitted.
     case 0x10:           // The Slave Address + R/W bit will now be transmitted.
       i2c_load((I2C2_CNTRL.Addr<<1)|I2C2_CNTRL.RoW);
       I2C2CONSET=0x04;
       I2C2CONCLR=0x20;
       I2C2CONCLR=0x08;
       
       
       break;
       
     case 0x18:         // Previous state was State 0x08 or State 0x10, Slave Address + Write has been transmitted, ACK has been received.
                        // The first data byte will be transmitted.
       if(I2C2_CNTRL.SendCounter==I2C2_CNTRL.SendSize)
       {
          I2C2CONSET_bit.STO=1; 
       }
       
       else
       {
         i2c_load(I2C2_CNTRL.SendBuffer[I2C2_CNTRL.SendCounter]);
         I2C2_CNTRL.SendCounter++;
         I2C2CONCLR=0x08;
       } 
       break;
     case 0x20:
         I2C2CONSET=0x14;
         I2C2_CNTRL.Error_ID=no_ack;
         I2C2_CNTRL.Busy=free;
         //i2c_send(I2C2_CNTRL.Addr,I2C2_CNTRL.SendBuffer,I2C2_CNTRL.SendSize);
         I2C2CONCLR=0x08;
       break;
     case 0x28:
       if (I2C2_CNTRL.SendCounter==0)
       {
         i2c_load(I2C2_CNTRL.SendBuffer[I2C2_CNTRL.SendCounter]);
         I2C2CONSET=0x04;
        
         I2C2_CNTRL.SendCounter++;
         I2C2CONCLR=0x08;
       }
                  
       else if (I2C2_CNTRL.SendCounter < I2C2_CNTRL.SendSize && I2C2_CNTRL.SendCounter > 0)
       {
         i2c_load(I2C2_CNTRL.SendBuffer[I2C2_CNTRL.SendCounter]);
         I2C2CONSET=0x04; 
         
         I2C2_CNTRL.SendCounter++;
         I2C2CONCLR=0x08;
       }
                  
       else if(I2C2_CNTRL.SendCounter == I2C2_CNTRL.SendSize)
       {
         I2C2CONSET=0x14;  
         I2C2_CNTRL.Busy=free;
         I2C2_CNTRL.Error_ID=ok;
         I2C2CONCLR=0x08;
         if(I2C2_CNTRL.WriteRead)
         {
           i2c_receive(I2C2_CNTRL.Addr,I2C2_CNTRL.SendSize);
         } 
         
        }
       break;
       
     case 0x30:                   // Data has been transmitted, NOT ACK received.                   
         I2C2CONSET=0x14;         // A STOP condition will be transmitted.
         I2C2_CNTRL.Error_ID=no_ack;
         I2C2_CNTRL.Busy=free;
         I2C2CONCLR=0x08;
       break;
       
     case 0x38:                 // Arbitration has been lost during Slave Address + Write or data.            
         I2C2CONSET=0x24;       // The bus has been released and not addressed Slave mode is entered.
         I2C2CONCLR=0x08;       // A new START condition will be transmitted when the bus is free again.
       break;
       
     case 0x40:                // Previous state was State 08 or State 10. Slave Address + Read has been transmitted, ACK has been received.
         I2C2CONSET=0x04;      // Data will be received and ACK returned.
         I2C2CONCLR=0x08;
       break;
       
     case 0x48:               // Slave Address + Read has been transmitted, NOT ACK has been received.
         I2C2CONSET=0x14;     // A STOP condition will be transmitted.
         I2C2CONCLR=0x08; 
       break;
       
     case 0x50:               // Data has been received, ACK has been returned.
                              // Data will be read from I2DAT. Additional data will be received.
                              // If this is the last data byte then NOT ACK will be returned, otherwise ACK will be returned.
       if (I2C2_CNTRL.ReceiveCounter== 0)
       { 
         I2C2_CNTRL.ReceiveCounter=0;
         I2C2_CNTRL.ReceiveBuffer[I2C2_CNTRL.ReceiveCounter]=i2c_return();
         I2C2_CNTRL.ReceiveCounter++;
         I2C2CONSET=0x04;  
         I2C2CONCLR=0x08;
       }
       
       else if (I2C2_CNTRL.ReceiveCounter < I2C2_CNTRL.ReceiveSize && I2C2_CNTRL.ReceiveCounter > 0)
       {
         I2C2_CNTRL.ReceiveBuffer[I2C2_CNTRL.ReceiveCounter]=i2c_return();
         I2C2_CNTRL.ReceiveCounter++;
         I2C2CONSET=0x04;  
         I2C2CONCLR=0x08;
         
       }
       
       else if (I2C2_CNTRL.ReceiveCounter == I2C2_CNTRL.ReceiveSize)
       {

        I2C2CONCLR=0x04;
        I2C2CONCLR=0x08;
       }       
             
     break;
     
     case 0x58:           // Data has been received, NOT ACK has been returned.
                          // Data will be read from I2DAT. A STOP condition will be transmitted.
       if(I2C2_CNTRL.ReceiveCounter < I2C2_CNTRL.ReceiveSize)
       {
       
          I2C2_CNTRL.ReceiveBuffer[I2C2_CNTRL.ReceiveCounter]=i2c_return();
          I2C2CONSET=0x14;  
          I2C2_CNTRL.Data=1;
          I2C2_CNTRL.Busy=free;
          I2C2_CNTRL.Error_ID=ok;
          I2C2CONCLR=0x08;
       }
       
       else
       {
          I2C2CONSET=0x14;  
          I2C2_CNTRL.Data=1;
          I2C2_CNTRL.Busy=free;
          I2C2CONCLR=0x08;
       }
       break;
       
    default:
      
      I2C2_CNTRL.Error_ID= unknown_state;
      break;
      
    } 
}

/*************************************************************************
 *    Funktion: i2c_send()
 *
 *    Beschreibung: Schickt die angegebene Anzahl von Bytes an die übergebene Addresse.
 *
 *
 **************************************************************************/

unsigned int i2c_send(unsigned char addr, unsigned char *data, unsigned char size)
{
      NVIC_IntDisable(NVIC_TIMER1);     //disable flashing leds
      
      if(!i2c_get_status())
      {
        I2C2_CNTRL.Error_ID=i2c_busy;
        return I2C2_CNTRL.Error_ID;
      }
      
      else
      {
        I2C2_CNTRL.Busy=busy; 
        I2C2_CNTRL.SendCounter=0;
        I2C2_CNTRL.Addr=addr;
        I2C2_CNTRL.SendSize=size;
        if( I2C2_CNTRL.SendSize > 1)
        {
          for(unsigned char i=0; i< I2C2_CNTRL.SendSize;i++)
          {
            I2C2_CNTRL.SendBuffer[i]=data[i];
          }
        }
        else
        {
         I2C2_CNTRL.SendBuffer[0]=data[0];
        }
          
      
        I2C2_CNTRL.RoW=write; 
        I2C2_CNTRL.WriteRead=0; 

        I2C2CONSET_bit.STA=1;
        
        while(I2C2_CNTRL.Busy == busy)
        {
          if (I2C2_TIMEOUT-- == 0)
          {
            return I2C2_CNTRL.Error_ID = timeout;
          }          
          // Wait the interrupt
          if (I2C2CONSET & 0x08)
          {
            i2c_statehandler(I2C2STAT&0xF8);    // Master Mode
          }
        }
        
      }
      
      for(unsigned int u=0;u<300;u++);
      NVIC_IntEnable(NVIC_TIMER1);     //enable flashing leds
      I2C2_TIMEOUT = I2C_Timeout;
      return I2C2_CNTRL.Error_ID;
}

/*************************************************************************
 *    Funktion: i2c_receive()
 *
 *    Beschreibung: Liest die angegebne Anzahl von Bytes von der Zieladdresse.
 *
 *
 **************************************************************************/

unsigned int i2c_receive(unsigned char addr, unsigned char size)
{
      NVIC_IntDisable(NVIC_TIMER1);     //disable flashing leds
      
      if(!i2c_get_status())
      {
        I2C2_CNTRL.Error_ID=i2c_busy;
        return I2C2_CNTRL.Error_ID;
      }
      
      else if(I2C2_CNTRL.Data==1)
      {
          I2C2_CNTRL.Error_ID=unused_data;
          return I2C2_CNTRL.Error_ID;
      }
      
      else
      {
        I2C2_CNTRL.Busy=busy; 
        I2C2_CNTRL.ReceiveCounter=0;
        I2C2_CNTRL.Addr=addr;
        I2C2_CNTRL.ReceiveSize=size;
      
        I2C2_CNTRL.RoW=read; 
       
        I2C2CONSET_bit.STA=1;
        
        while(I2C2_CNTRL.Busy == busy)
        {
          if (I2C2_TIMEOUT-- == 0)
          {
            return I2C2_CNTRL.Error_ID = timeout;
          }          
          // Wait the interrupt
          if (I2C2CONSET & 0x08)
          {
            i2c_statehandler(I2C2STAT&0xF8);    // Master Mode
          }
        }
      }
      
      NVIC_IntEnable(NVIC_TIMER1);     //enable flashing leds
      I2C2_TIMEOUT = I2C_Timeout;
      return I2C2_CNTRL.Error_ID;
}

/*************************************************************************
 *    Funktion: i2c_send_receive()
 *
 *    Beschreibung: Schickt und empfängt die jeweils angegebene Anzahl von Bytes.
 *
 *
 **************************************************************************/

unsigned int i2c_send_receive(unsigned int addr, unsigned char *data, unsigned char send_size,unsigned char receive_size)
{
      NVIC_IntDisable(NVIC_TIMER1);     //disable flashing leds
  
  
      if(!i2c_get_status())
      {
        I2C2_CNTRL.Error_ID=i2c_busy;
        return I2C2_CNTRL.Error_ID;
      }
      
      
      
      else
      {
      
        I2C2_CNTRL.Busy=busy; 
        I2C2_CNTRL.SendCounter=0;
        I2C2_CNTRL.Addr=addr;
        I2C2_CNTRL.SendSize=send_size;
        if( I2C2_CNTRL.SendSize > 1)
        {
          for(unsigned char i=0; i> I2C2_CNTRL.SendSize;i++)
          {
            I2C2_CNTRL.SendBuffer[i]=data[i];
          }
        }
       
        else
        {
          I2C2_CNTRL.SendBuffer[0]=data[0];
        }
      
        I2C2_CNTRL.ReceiveCounter=0;
        I2C2_CNTRL.ReceiveSize=receive_size;
      
        I2C2_CNTRL.WriteRead=1;      
        I2C2_CNTRL.RoW=write;
  
        I2C2CONSET_bit.STA=1;
        
        while(I2C2_CNTRL.Busy == busy)
        {
          if (I2C2_TIMEOUT-- <= 0)
          {
            I2C2_CNTRL.Busy = free;  
            I2C2_TIMEOUT = I2C_Timeout;  
            NVIC_IntEnable(NVIC_TIMER1);     //Enable flashing leds            
            return I2C2_CNTRL.Error_ID = timeout;
          }          
          // Wait the interrupt
          if (I2C2CONSET & 0x08)
          {
            i2c_statehandler(I2C2STAT&0xF8);    // Master Mode
          }
        }
      }
      
      NVIC_IntEnable(NVIC_TIMER1);     //Enable flashing leds
      I2C2_TIMEOUT = I2C_Timeout;
      return I2C2_CNTRL.Error_ID;
}
/*************************************************************************
 *    Funktion: i2c_load()
 *
 *    Beschreibung: Beschreibt das Datenregister I2C2DAT.
 *
 *
 **************************************************************************/  

void i2c_load(unsigned char data)
{
    I2C2DAT=data;
}

/*************************************************************************
 *    Funktion: i2c_return()
 *
 *    Beschreibung: Liest das Datenregister I2C2DAT.
 *
 *
 **************************************************************************/  
unsigned char i2c_return (void)
{
    return I2C2DAT;
}





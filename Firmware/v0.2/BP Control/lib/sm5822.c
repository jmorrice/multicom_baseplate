/********************************************************************************
 *    Bibliothek: sm5822.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktionen, um den Temperatur- und Drucksensor Sm5822 auslesen zu können.               
 *    
 ********************************************************************************/

#include "incl.h"


struct sm5822 SM5822_CNTRL;
extern unsigned char TEMP_OKAY, PRESS_OKAY, CAM_OKAY;

/*************************************************************************
 *    Funktion: sm5822_init()
 *
 *    Beschreibung: - Initialisiert alle wichtigen Flags.
 *  
 **************************************************************************/

void sm5822_init(void)
{
  
  SM5822_CNTRL.receive_status=0;
  SM5822_CNTRL.send_status=0;
  SM5822_CNTRL.busy_flag=0;
  
  //test communication
  get_data(temperature);
  get_data(pressure);
}

/*************************************************************************
 *    Funktion: get_data()
 *
 *    Beschreibung: Fordert die je nach Parameter Temperatur- oder Druckdaten an 
 *  
 **************************************************************************/


void get_data(enum sm5822_mode function)
{
    SM5822_CNTRL.mode=function;
    
   if ( SM5822_CNTRL.mode==temperature)
   {
          SM5822_CNTRL.LSB[0]=0x82;
          SM5822_CNTRL.MSB[0]=0x83;
          SM5822_CNTRL.temp1[0]=0;
          SM5822_CNTRL.temp2[0]=0;
          i2c_send_receive(0x5F,SM5822_CNTRL.LSB,1,1);
          SM5822_CNTRL.send_status=1;
          i2c_get_data(SM5822_CNTRL.temp1);
          i2c_send_receive(0x5F,SM5822_CNTRL.MSB,1,1);
          i2c_get_data(SM5822_CNTRL.temp2);
          SM5822_CNTRL.cache=((SM5822_CNTRL.temp2[0]<<6)|SM5822_CNTRL.temp1[0]);
          SM5822_CNTRL.Result= ((1-(SM5822_CNTRL.cache*0.0002442))*165)-40;
          SM5822_CNTRL.temperature = SM5822_CNTRL.Result *1000;
          
          if(I2C2_CNTRL.Error_ID == ok && TEMP_OKAY)
            TEMP_OKAY = 1;
          else
            TEMP_OKAY = 0;
   }
   
   if (SM5822_CNTRL.mode==pressure)
   {
          SM5822_CNTRL.LSB[0]=0x80;
          SM5822_CNTRL.MSB[0]=0x81;
          SM5822_CNTRL.temp1[0]=0;
          SM5822_CNTRL.temp2[0]=0;
          i2c_send_receive(0x5F,SM5822_CNTRL.LSB,1,1);
          SM5822_CNTRL.send_status=1;
          //while(!i2c_get_status());
          i2c_get_data(SM5822_CNTRL.temp1);
          i2c_send_receive(0x5F,SM5822_CNTRL.MSB,1,1);
          //while(!i2c_get_status());
          i2c_get_data(SM5822_CNTRL.temp2);
          SM5822_CNTRL.cache=((SM5822_CNTRL.temp2[0]<<6)|SM5822_CNTRL.temp1[0]);
          SM5822_CNTRL.Result= ((SM5822_CNTRL.cache*0.0002442)*15);
          SM5822_CNTRL.pressure = SM5822_CNTRL.Result *1000;
          
          if(I2C2_CNTRL.Error_ID == ok && PRESS_OKAY)
            PRESS_OKAY = 1;
          else
            PRESS_OKAY = 0;          
   }
   
   if(SM5822_CNTRL.mode==uncorrected_pressure)
   {
          SM5822_CNTRL.LSB[0]=0x84;
          SM5822_CNTRL.MSB[0]=0x85;
          SM5822_CNTRL.temp1[0]=0;
          SM5822_CNTRL.temp2[0]=0;
          i2c_send_receive(0x5F,SM5822_CNTRL.LSB,1,1);
          SM5822_CNTRL.send_status=1;
         // while(!i2c_get_status());
          i2c_get_data(SM5822_CNTRL.temp1);
          i2c_send_receive(0x5F,SM5822_CNTRL.MSB,1,1);
          //while(!i2c_get_status());
          i2c_get_data(SM5822_CNTRL.temp2);
          SM5822_CNTRL.cache=((SM5822_CNTRL.temp2[0]<<6)|SM5822_CNTRL.temp1[0]);
          SM5822_CNTRL.Result= ((SM5822_CNTRL.cache*0.0002442)*15);
          SM5822_CNTRL.u_pressure = SM5822_CNTRL.Result *1000;
     
          if(I2C2_CNTRL.Error_ID == ok && PRESS_OKAY)
            PRESS_OKAY = 1;
          else
            PRESS_OKAY = 0;        
   }
}

/*************************************************************************
 *    Funktion: request_sm5822()
 *
 *    Beschreibung: Fordert die je nach Parameter Temperatur- oder Druckdaten an 
 *  
 **************************************************************************/


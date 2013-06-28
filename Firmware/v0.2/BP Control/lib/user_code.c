/********************************************************************************
 *    Bibliothek: user_code.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012   
 *
 *    Beschreibung: Entählt das komplette Anwendungsprogramm für die Zelle.   
 *
 *
 ********************************************************************************/

#include "incl.h"
#include <math.h>
extern enum CELL_STAT CELL_STATUS;
extern unsigned char TEMP_OKAY, PRESS_OKAY, CAM_OKAY, BW_thresh, CAM_REG;

/****** Programmcode ******/
void user_program(void)
{
      //Dauerschleife
      //if(T0IR_bit.MR0INT && I2C0_CNTRL.Busy==free && !I2C0_CNTRL.Transmission)
      //{
      //  FIO0PIN_bit.P0_4 ^=1;
        //save_pic();
      //  get_data(temperature);
      //  get_data(pressure);
      //  T0IR_bit.MR0INT=1;
     // }
      if(COMMAND_CNTRL.command_received)
      {
        T0TCR_bit.CE = 0;     // counting Disable
        switch(COMMAND_CNTRL.Kommando)
        {
          //Kommando:           flash
          //Nummer:             1
          //Beschreibung:       Flasht die Zelle
          case flash:
            clear_command();
            for(unsigned char a=0;a<42;a++)
            {
              CAM_CNTRL.Picture[IAP_CNTRL.byte_counter]=I2C0_CNTRL.ReceiveBuffer[a+1];;
              IAP_CNTRL.byte_counter++;
            }
            Flash();        
            break;          
          
          //Kommando:           start_system
          //Nummer:             2
          //Beschreibung:       Starten die Dauerschleife
          case start_system:
            clear_command();
            SYSTEM_FLAGS.timer=1;
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;
            break;
          
          //Kommando:           stop_system
          //Nummer:             3
          //Beschreibung:       Stoppt die Dauerschleife            
          case stop_system:
            clear_command();
            SYSTEM_FLAGS.timer=0;
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;
            break;

          //Kommando:           software_reset
          //Nummer:             4
          //Beschreibung:       Setzt die Zelle zurück            
          case software_reset:
            clear_command();   
            PWM1_DISABLE();
            PWM2_DISABLE();
            PWM3_DISABLE();
            CELL_STATUS = INIT_ARM;          
            break;
            
          //Kommando:           send_status
          //Nummer:             6
          //Beschreibung:       Schickt den aktuellen Status in einem Byte             
          case send_status:
             clear_command();
             unsigned char status = 0;
             if(TEMP_OKAY)
               status = status | 1;
             if(PRESS_OKAY)
               status = status | 2;
             if(CAM_OKAY)
               status = status | 4;
             
             I2C0_CNTRL.SendSize=1;
             I2C0_CNTRL.SendBuffer[0] = status;
             T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
             break;  
   
          //Kommando:           send_temperature
          //Nummer:             7
          //Beschreibung:       Misst und sendet die aktuelle Temperatur       
          case send_temperature:
            clear_command();
            get_data(temperature);
            I2C0_CNTRL.SendSize=2;
            I2C0_CNTRL.SendBuffer[0] = (char) (SM5822_CNTRL.temperature & 0xFF);
            I2C0_CNTRL.SendBuffer[1] = (char)((SM5822_CNTRL.temperature>>8) & 0xFF);
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;             

          //Kommando:           send_pressure
          //Nummer:             8
          //Beschreibung:       Misst und sendet den aktuellen Druck
          case send_pressure:
             clear_command();
             get_data(pressure);
             I2C0_CNTRL.SendSize=2;
             I2C0_CNTRL.SendBuffer[0] = (char) (SM5822_CNTRL.pressure & 0xFF);
             I2C0_CNTRL.SendBuffer[1] = (char)((SM5822_CNTRL.pressure >>8) & 0xFF);
             T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
             break;
             
          //Kommando:           pwm
          //Nummer:             9
          //Beschreibung:       Übernimmt die empfangenen PWM Einstellungen           
          case pwm:
             clear_command();
             unsigned char hell = COMMAND_CNTRL.param[0];
             unsigned char sel = COMMAND_CNTRL.param[1];
             PWM_HELLIGKEIT(hell);
             if(sel & 1)
                PWM1_ENABLE();
             else
                PWM1_DISABLE();
             if(sel & 2)
                PWM2_ENABLE();
             else
                PWM2_DISABLE();
             if(sel & 4)
                PWM3_ENABLE();
             else
                PWM3_DISABLE();
             T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
             break;    
   
          //Kommando:           sel_cam_reg
          //Nummer:             10
          //Beschreibung:       Wählt ein Kamera Register           
          case sel_cam_reg:
             clear_command();
             CAM_REG = COMMAND_CNTRL.param[0];
             T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
             break;

          //Kommando:           write_cam_reg
          //Nummer:             11
          //Beschreibung:       Beschreibt ein Kamera Register
          case write_cam_reg:
              clear_command();
              cam_config(CAM_REG, COMMAND_CNTRL.param[0]);
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
 
          //Kommando:           read_cam_reg
          //Nummer:             12
          //Beschreibung:       Liest ein Kamera Register
          case read_cam_reg:
              clear_command();
              I2C0_CNTRL.SendSize = 1;
              I2C0_CNTRL.SendBuffer[0] = cam_read(CAM_REG);
                          
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
 
          //Kommando:           sel_start_pixel
          //Nummer:             13
          //Beschreibung:       Wählt das Start Pixel der nächsten Bildübertragung aus.
          //                    Die Daten werden nur temporär als I2C Parameter gespeichert.
          case sel_start_pixel:
              clear_command();
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;

          //Kommando:           sel_grey_area
          //Nummer:             14
          //Beschreibung:       Wählt den Bereich des Graubildes der aufgenommen werden soll.
          //                    Die Daten werden nur temporär als I2C Parameter gespeichert.  
          case sel_grey_area:
              clear_command();
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
      
          //Kommando:           take_test_pic
          //Nummer:             15
          //Beschreibung:       Speichert ein Testbild, das via Graubildübertragung ausgelesen werden kann           
          case take_test_pic:
            clear_command();
            test_pic();
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;

          //Kommando:           set_bw_thresh
          //Nummer:             16
          //Beschreibung:       Bestimmt den Schwellwert eines Binärbildes
          case set_bw_thresh:
              clear_command();
              BW_thresh = COMMAND_CNTRL.param[0];
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;      
    
          //Kommando:           take_bin_pic
          //Nummer:             17
          //Beschreibung:       Speichert ein Binärbild           
          case take_bin_pic:
            clear_command();
            analyse_grey_pic();
            save_bin_pic();
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;            

          //Kommando:           send_bin_pic
          //Nummer:             18
          //Beschreibung:       Verschickt ein komplettes Binärbild
          case send_bin_pic:     
            clear_command();
            I2C0_CNTRL.SendSize=COMMAND_CNTRL.param[5];

            for(unsigned char a=0; a < I2C0_CNTRL.SendSize; a++)
            {
              I2C0_CNTRL.Bytes=(COMMAND_CNTRL.param[0]*40)+(COMMAND_CNTRL.param[1]+COMMAND_CNTRL.param[2]+COMMAND_CNTRL.param[3]+COMMAND_CNTRL.param[4])+a;
              I2C0_CNTRL.SendBuffer[a]=CAM_CNTRL.Picture[I2C0_CNTRL.Bytes];
            }
              T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
            
          //Kommando:           take_grey_picture
          //Nummer:             19
          //Beschreibung:       Nimmt einen Graubildabschnitt auf
          case take_grey_pic:
            clear_command();
            save_grey_pic(COMMAND_CNTRL.param[0]);
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;    
    
          //Kommando:           send_grey_pic
          //Nummer:             20
          //Beschreibung:       Verschickt einen Grau- oder Testbildabschnitt
          case send_grey_pic:
            clear_command();
            I2C0_CNTRL.SendSize=COMMAND_CNTRL.param[5];

            for(unsigned char a=0; a < I2C0_CNTRL.SendSize; a++)
            {
              I2C0_CNTRL.Bytes=(COMMAND_CNTRL.param[0]*320)+(COMMAND_CNTRL.param[1]+COMMAND_CNTRL.param[2])+a;
              I2C0_CNTRL.SendBuffer[a]=CAM_CNTRL.Picture[I2C0_CNTRL.Bytes];
            }       

            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;             
            
          //Kommando:           bildbearbeitung
          //Nummer:             21
          //Beschreibung:       Bearbeitet das gespeicherte Bild
          case bildbearbeitung:
             clear_command();
             Bildbearbeitung();
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
            
          //Kommando:           koordinaten
          //Nummer:             22
          //Beschreibung:       Übergibt die berechneten Koordinaten          
          case koordinaten: 
            clear_command();
            switch(COMMAND_CNTRL.param[0])
            {
            case 0:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate0[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate0[1];
              break;
                
            case 1:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate1[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate1[1];
              break;
              
            case 2:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate2[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate2[1];
              break;
              
            case 3:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate3[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate3[1];
              break;
              
            case 4:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate4[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate4[1];
              break;
              
            case 5:
              I2C0_CNTRL.SendBuffer[0]=Koordinaten.Koordinate5[0];
              I2C0_CNTRL.SendBuffer[1]=Koordinaten.Koordinate5[1];
              break;
            }
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable; 
            break;
            
          case set_cam_gain:
             clear_command();
             int gain = COMMAND_CNTRL.param[0] & (COMMAND_CNTRL.param[1] << 7);
             write_gain(gain);
             T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;  
  
          case read_cam_gain:
             clear_command();
             int readgain = read_gain();
             I2C0_CNTRL.SendSize = 2;
             I2C0_CNTRL.SendBuffer[0] = readgain & 0xFF;
             I2C0_CNTRL.SendBuffer[1] = readgain >> 7;
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break; 

          case set_cam_exp:
             clear_command();
             int exp = COMMAND_CNTRL.param[0] & (COMMAND_CNTRL.param[1] << 7);
             write_exposure(exp);             
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break; 
            
          case read_cam_exp:
             clear_command();
             int exposure = read_exposure();
             I2C0_CNTRL.SendSize = 2;
             I2C0_CNTRL.SendBuffer[0] = exposure & 0xFF;
             I2C0_CNTRL.SendBuffer[1] = exposure >> 7;             
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break; 

          case set_rgb_gain:
             clear_command();
            write_r_gain(COMMAND_CNTRL.param[0]);
            write_g_gain(COMMAND_CNTRL.param[1]);
            write_b_gain(COMMAND_CNTRL.param[2]);
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break; 

          case read_rgb_gain:
             clear_command();
            I2C0_CNTRL.SendSize = 3; 
            I2C0_CNTRL.SendBuffer[0] = read_r_gain();
            I2C0_CNTRL.SendBuffer[1] = read_g_gain();
            I2C0_CNTRL.SendBuffer[2] = read_b_gain();
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;  
            
          case set_cam_auto:
             clear_command();
            if(COMMAND_CNTRL.param[0] & 0x01)
              AEC_ENABLE();
            else
              AEC_DISABLE();
            if(COMMAND_CNTRL.param[0] & 0x02)
              AWB_ENABLE();
            else
              AWB_DISABLE();
            if(COMMAND_CNTRL.param[0] & 0x04)
              AGC_ENABLE();
            else
              AGC_DISABLE();            
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;     
   
          case read_cam_auto:
             clear_command();
             
            unsigned char COM8 = cam_read(0x13);
            I2C0_CNTRL.SendSize = 1; 
            I2C0_CNTRL.SendBuffer[0] = COM8 & 7;            
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;
            
         //Kommando:           bootloader
         //Nummer:             33
         //Beschreibung:       gibt die bootloader versionsnummer aus
          case bootloader_version:
            clear_command();
            send_bootloader_version();
            break;
          
          case firmware_version:
            clear_command();
           I2C0_CNTRL.SendSize=2;
           I2C0_CNTRL.SendBuffer[0] = (char) firmware_major;
           I2C0_CNTRL.SendBuffer[1] = (char) firmware_minor;
           break;

          case height_req:
             clear_command();
            analyse_grey_pic();
            save_bin_pic();
            get_height();             
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;             
           
          case height_send:
             clear_command();          
            I2C0_CNTRL.SendSize = 2;
            I2C0_CNTRL.SendBuffer[0] = (unsigned char)CAM_CNTRL.height;
            I2C0_CNTRL.SendBuffer[1] = (unsigned char)((CAM_CNTRL.height - floor(CAM_CNTRL.height)) * 100);
            T0TCR_bit.CE = SYSTEM_FLAGS.timer;     // counting Enable;
            break;          
        }
      }  
}
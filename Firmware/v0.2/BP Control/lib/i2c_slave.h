/********************************************************************************
 *    Bibliothek: i2c_slave.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktions-Header und Kontrollstrukturen für i2c_slave.c.
 *                        
 *
 ********************************************************************************/


/****** Aufzählungen ******/



enum command {  
                prepare_flash = 0,
                get_code = 1,
                start_system,
                stop_system, 
                software_reset,
                receive_address,
                send_status, 
                send_temperature,
                send_pressure,
                pwm,
                sel_cam_reg,
                write_cam_reg,
                read_cam_reg,
                sel_start_pixel,
                sel_grey_area,
                take_test_pic,
                set_bw_thresh,
                take_bin_pic,                
                send_bin_pic, 
                take_grey_pic, 
                send_grey_pic, //20                
                bildbearbeitung,
                koordinaten,
                set_cam_gain,
                read_cam_gain,
                set_cam_exp,
                read_cam_exp,
                set_rgb_gain,
                read_rgb_gain,
                set_cam_auto,
                read_cam_auto, //30
                boot_status,
                i2c_test,
                flash,
                bootloader_version,
                firmware_version,
                dummy6,
                dummy7,
                dummy8,
                height_req,
                height_send};

/****** Strukturen ******/


extern struct i2c I2C0_CNTRL;

/****************************************************************************************
 *    Struktur: command_status
 *
 *    Beschreibung: Enthält die aktuellen Information über die Befehle. 
 *                  Kann zur Weiterverarbeitung genutzt werden.
 * 
 ****************************************************************************************/

struct command_status
{
  enum command Kommando;
  unsigned char command_received;
  unsigned char command_ready;
  unsigned char param[6];
};

extern struct command_status COMMAND_CNTRL;

/****** Header ******/

void i2c_slave_init (void);

void get_address(unsigned char default_address);

void I2C0_IRQHandler (void);

void i2c_slave_statehandler (unsigned char state);

void clear_command(void);

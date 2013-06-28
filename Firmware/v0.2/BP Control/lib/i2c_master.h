/********************************************************************************
 *    Bibliothek: i2c_master.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktions-Header für i2c_master.c  
 *                
 *    
 ********************************************************************************/


/****** Deklaration ******/

extern struct i2c I2C2_CNTRL;



/****** Header ******/

void i2c_init(void);

unsigned char i2c_get_status(void);

enum error i2c_get_data(unsigned char *data);

void I2C2_IRQHandler (void);

void i2c_statehandler (unsigned char state);

unsigned int i2c_send(unsigned char addr, unsigned char *data, unsigned char size);

unsigned int i2c_receive(unsigned char addr, unsigned char size);

unsigned int i2c_send_receive(unsigned int addr, unsigned char *data, unsigned char send_size,unsigned char receive_size);

void i2c_load(unsigned char data);

unsigned char i2c_return (void);


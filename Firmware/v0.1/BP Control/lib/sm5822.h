/********************************************************************************
 *    Bibliothek: sm5822.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktions-Header und Kontrollstrukturen für sm5822.c               
 *    
 ********************************************************************************/

/****** Aufzählungen ******/


enum sm5822_mode { temperature, pressure, uncorrected_pressure};

/****** Strukturen ******/

struct sm5822
{
    unsigned char LSB [1];
    unsigned char MSB [1];
    unsigned char temp1[1];
    unsigned char temp2[1];
    unsigned char send_status;
    unsigned char receive_status;
    unsigned char busy_flag;
    signed int cache;
    float Result;
    int temperature;
    int pressure;
    int u_pressure;
    enum sm5822_mode mode;
    enum error Error;
};

extern  struct sm5822 SM5822_CNTRL;

/****** Header ******/

void sm5822_init(void);

void get_data(enum sm5822_mode function);

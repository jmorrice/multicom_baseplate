/********************************************************************************
 *    Bibliothek: cam.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktions-Header und Kontrollstrukturen für cam.c               
 *    
 ********************************************************************************/


/****** Definitionen ******/

#define DB0 0
#define DB1 1
#define DB2 14
#define DB3 17
#define DB4 6

/*************************************************************************
 *    Name:Bitbanding
 *
 *    Beschreibung: Berechnet die Bitband-Addresse für DCLK, HD und VD. 
 *
 *
 **************************************************************************/
#define CAM_SYNC   0x2009C055
#define DCLK    *((volatile unsigned int *) (BITBAND_SRAM(CAM_SYNC,3)))     //FIO2PIN_bit.P2_11
#define HD      *((volatile unsigned int *) (BITBAND_SRAM(CAM_SYNC,4)))     //FIO2PIN_bit.P2_12
#define VD      *((volatile unsigned int *) (BITBAND_SRAM(CAM_SYNC,5)))     //FIO2PIN_bit.P2_13


/****** Strukturen ******/

struct cam
{
  unsigned char sub_addr;
  unsigned char reg_value;
  unsigned char data_package[2];
// unsigned char Picture [19200]; 
  unsigned char Picture[20000];
  unsigned char bit_count;
  unsigned int  pixel_count;
  unsigned int  byte_address;
  unsigned int  DCLK_count;
  unsigned char picture_ready;
  unsigned char run; 
};


extern struct cam CAM_CNTRL;

/****** Header ******/



void cam_config(unsigned char SubAddr, unsigned char value);

unsigned char cam_read(unsigned char SubAddr);

void cam_init(void);

void start_cam(void);

void get_pic(void);

void save_bin_pic(void);

void save_grey_pic(unsigned char section);

void decrease_clock_output (void);

void AGC_ENABLE();
void AGC_DISABLE();
void AWB_ENABLE();
void AWB_DISABLE();
void AEC_ENABLE();
void AEC_DISABLE();

int read_exposure();

void write_exposure(int exposure);

int read_gain();

unsigned char read_r_gain();

unsigned char read_g_gain();

unsigned char read_b_gain();

void write_r_gain(unsigned char gain);

void write_g_gain(unsigned char gain);

void write_b_gain(unsigned char gain);

void write_gain(int gain);

void test_pic (void);

void clock_output_init(void);

void windowing(unsigned int HStart, unsigned int HStop, unsigned int VStart, unsigned int VStop);







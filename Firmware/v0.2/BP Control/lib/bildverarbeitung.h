/********************************************************************************
 *    Bibliothek: bildverarbeitung.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Enthält die Funktions-Header und Kontrollstrukturen für bildverarbeitung.c               
 *    
 ********************************************************************************/


/****** Strukturen ******/

struct result
{
  unsigned char Koordinate0[2];
  unsigned char Koordinate1[2];
  unsigned char Koordinate2[2];
  unsigned char Koordinate3[2];
  unsigned char Koordinate4[2];
  unsigned char Koordinate5[2];
};

extern struct result Koordinaten;


/****************************************************************************************
 *    Struktur: pic_tools
 *
 *    Beschreibung: enthält Buffer und Flags, die zur Bildbearbeitung verwendet werden.
 * 
 ****************************************************************************************/

struct pic_tools
{
  unsigned char row;
  unsigned char col;
  unsigned char row_old;
  unsigned char row_offset;
  unsigned char col_offset;
  unsigned char center_x;
  unsigned char center_y;
  unsigned char position_buffer [70][25];
};


extern struct pic_tools PIC_TOOL_CNTRL;

/****** Header ******/

void threshold (void);

unsigned int picture_position(unsigned int zeile, unsigned int spalte); 

void get_position(void);

void get_center(void);

void mark_center(void);

void Bildbearbeitung(void);
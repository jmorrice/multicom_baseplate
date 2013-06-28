/********************************************************************************
 *    Bibliothek: bildverarbeitung.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Multi.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: Entählt Funktionen um ein S/W Bild zu erstellen und den Mittelpunkt eines hellen Kreises zu ermitteln.   
 *
 *
 ********************************************************************************/

#include "incl.h"

struct pic_tools PIC_TOOL_CNTRL;

struct result Koordinaten;

/*************************************************************************
 *    Funktion: threshold()
 *
 *    Beschreibung: Prüft jedes Pixel, ob es über einem Schwellwert liegt.
 *                  Wenn ja, wird das Pixel weiß, falls nicht, wird es Schwarz.
 *  
 **************************************************************************/

void threshold(void)
{
  for (unsigned int a=0;a<12288;a++)
  {
    if( CAM_CNTRL.Picture[a]>160)
    {
      CAM_CNTRL.Picture[a]=0xFF;
    }
    else
    {
      CAM_CNTRL.Picture[a]=0x00;
    }
  }
}

/*************************************************************************
 *    Funktion: picture_position()
 *
 *    Beschreibung: Rechnet die Position eines Pixel im Bildbuffer aus.
 *               
 *  
 **************************************************************************/

unsigned int picture_position(unsigned int zeile, unsigned int spalte)
{
  unsigned int Position;
  Position=(zeile*128)+spalte;
  return Position;
}

/*************************************************************************
 *    Funktion: get_position()
 *
 *    Beschreibung: Speichert die Positionen alles weißen Pixel im position buffer.
 *               
 *  
 **************************************************************************/

void get_position(void)
{
    PIC_TOOL_CNTRL.row_offset=0;

    
   for( PIC_TOOL_CNTRL.row=0;PIC_TOOL_CNTRL.row<96;PIC_TOOL_CNTRL.row++)
    {
      PIC_TOOL_CNTRL.col_offset=1;
      
      for(PIC_TOOL_CNTRL.col=0;PIC_TOOL_CNTRL.col<128;PIC_TOOL_CNTRL.col++)
      {
        
        
        if (CAM_CNTRL.Picture[picture_position(PIC_TOOL_CNTRL.row,PIC_TOOL_CNTRL.col)]==0xFF)
        {
          
         
          PIC_TOOL_CNTRL.position_buffer[PIC_TOOL_CNTRL.row_offset][0]=PIC_TOOL_CNTRL.row;
          PIC_TOOL_CNTRL.position_buffer[PIC_TOOL_CNTRL.row_offset][PIC_TOOL_CNTRL.col_offset]=PIC_TOOL_CNTRL.col;
          PIC_TOOL_CNTRL.col_offset++;
          if(PIC_TOOL_CNTRL.row != PIC_TOOL_CNTRL.row_old)
          {
            PIC_TOOL_CNTRL.row_offset++;
          }
          PIC_TOOL_CNTRL.row_old=PIC_TOOL_CNTRL.row;
        }
        
      }
    }
}


/*************************************************************************
 *    Funktion: get_center()
 *
 *    Beschreibung: Ermittelt den Mittelpunkt der weißen Pixel aus dem position buffer.
 *               
 *  
 **************************************************************************/
void get_center(void)
{
  char ready=0;

  
  PIC_TOOL_CNTRL.center_x=1;
  PIC_TOOL_CNTRL.center_y=PIC_TOOL_CNTRL.row_offset>>1;
  
  while(!ready)
  {
    if(PIC_TOOL_CNTRL.position_buffer[PIC_TOOL_CNTRL.center_y][PIC_TOOL_CNTRL.center_x])
    {
      PIC_TOOL_CNTRL.center_x++;
    }
    else
    {
      ready=1;
    }
  }
  
  PIC_TOOL_CNTRL.center_x=PIC_TOOL_CNTRL.center_x>>1;
  
  
  PIC_TOOL_CNTRL.center_x=PIC_TOOL_CNTRL.position_buffer[PIC_TOOL_CNTRL.center_y][PIC_TOOL_CNTRL.center_x];
  PIC_TOOL_CNTRL.center_y=PIC_TOOL_CNTRL.position_buffer[PIC_TOOL_CNTRL.center_y][0] ;
  
  Koordinaten.Koordinate0[0]= PIC_TOOL_CNTRL.center_x;
  Koordinaten.Koordinate0[1]=PIC_TOOL_CNTRL.center_y;
 
}

/*************************************************************************
 *    Funktion: mark_center()
 *
 *    Beschreibung: Markiert den mit "get_center()" gefundenen Mittelpunkt mit einem grauen Kreuz.
 *               
 *  
 **************************************************************************/

void mark_center(void)
{ 
  unsigned char x=PIC_TOOL_CNTRL.center_x-5;
  unsigned char y=PIC_TOOL_CNTRL.center_y-5;
  
  for(x;x<PIC_TOOL_CNTRL.center_x+5;x++)
  {
    CAM_CNTRL.Picture[picture_position(PIC_TOOL_CNTRL.center_y,x)]=0x80;

  }
  
  for(y;y<PIC_TOOL_CNTRL.center_y+5;y++)
  {
    CAM_CNTRL.Picture[picture_position(y,PIC_TOOL_CNTRL.center_x)]=0x80;

  }
}

void Bildbearbeitung(void)
{
  threshold();
  get_position();
  get_center();
  mark_center();
  
}


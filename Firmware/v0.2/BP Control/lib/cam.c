/********************************************************************************
 *    Bibliothek: cam.c
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Jonathan Morrice Multi.Com GmbH
 *
 *    Datum: 18.09.2012   
 *
 *    Beschreibung: Entählt alle Funktionen um ein Kamerabild auszulesen    
 *
 *    Hardware: Omnivision OV7675 CMOS Camera Module
 *
 ********************************************************************************/

#include "incl.h"
#include <math.h>

struct cam CAM_CNTRL;
extern unsigned char CAM_OKAY;
unsigned char BW_thresh = 245, CAM_REG;

#define DBG 0x2009C136
#define DBG3  *((volatile unsigned int *) (BITBAND_SRAM(DBG,1))) // Bit 0
#define KERNEL_SIZE 7
#define EDGE_STRENGTH 40


/*************************************************************************
 *    Funktion: clock_output_init()
 *
 *    Beschreibung: - Initialisiert den CLKOUT-Pin (P1.27) auf 25 MHz.
 *  
 **************************************************************************/

void clock_output_init(void)                   
{
  PINSEL3_bit.P1_27=01;                       // Select Pinfunction CLKOUT
  CLKOUTCFG_bit.CLKOUT_EN =0;
  CLKOUTCFG_bit.CLKOUTSEL=0;                  //CCLK auswählen 
  CLKOUTCFG_bit.CLKOUTDIV=4-1;                // 100/4=25 MHz
  CLKOUTCFG_bit.CLKOUT_EN=1;
  
  
}

/*************************************************************************
 *    Funktion: cam_init()
 *
 *    Beschreibung: - Alle wichtigen I/Os und Flags.
 *  
 **************************************************************************/

void cam_init(void)
{
  CAM_CNTRL.DCLK_count=1;
  CAM_CNTRL.pixel_count=0;
  CAM_CNTRL.byte_address=0;
  CAM_CNTRL.bit_count = 0;
  for(int i=0; i<19200; i++)
    CAM_CNTRL.Picture[i] = 0;
  
// camera data ports set all to GPIO input not pull up/down restistors
  PINSEL2_bit.P1_0 = 0;         // mode GPIO
  PINMODE2_bit.P1_0 = 2;        // no restistors
  FIO1DIR_bit.P1_0 = 0;         // input
  
  FIO1DIR_bit.P1_1      = 0;
  FIO1DIR_bit.P1_4      = 0;
  FIO1DIR_bit.P1_8      = 0;
  FIO1DIR_bit.P1_9      = 0;
  FIO1DIR_bit.P1_10     = 0;
  FIO1DIR_bit.P1_14     = 0;
  FIO1DIR_bit.P1_15     = 0;

// camera PWDN port set to output low (camera activ)
  PINSEL3_bit.P1_26 = 0;        // mode GPIO
  PINMODE3_bit.P1_26 = 2;       // no restistors
  FIO1DIR_bit.P1_26 = 1;        // output
  FIO1PIN_bit.P1_26 = 0;
  
  FIO2DIR_bit.P2_11=0;    //DCLK  
  FIO2DIR_bit.P2_12=0;    //HD  
  FIO2DIR_bit.P2_13=0;    //VD
  
  PINMODE2_bit.P1_1 = 2;
  PINMODE2_bit.P1_4 = 2;
  PINMODE2_bit.P1_8=2;
  PINMODE2_bit.P1_9=2;
  PINMODE2_bit.P1_10=2;
  PINMODE2_bit.P1_14=2;
  PINMODE2_bit.P1_15=2;
  
  
  PINMODE4_bit.P2_11=2;
  PINMODE4_bit.P2_12=2;
  PINMODE4_bit.P2_13=2;
  
   PINSEL4_bit.P2_11=0; 
   PINSEL4_bit.P2_12=0;
   PINSEL4_bit.P2_13=0;
   
  
  PINSEL2_bit.P1_1 = 0;
  PINSEL2_bit.P1_4 = 0;
  PINSEL2_bit.P1_8 = 0;
  PINSEL2_bit.P1_9 = 0;
  PINSEL2_bit.P1_10 = 0;
  PINSEL2_bit.P1_14 = 0;
  PINSEL2_bit.P1_15 = 0;
   
   //Konfiguriert die internen Kamera Register
   cam_config(0x12,0x10);       //select qvga
   cam_config(0x3A,0x0D);       //select YVYU
   cam_config(0x11,0x0B);       //internal clock prescaler
   //cam_config(0x11,0x0E);       //internal clock prescaler
   AEC_DISABLE();
   AWB_DISABLE();
   AGC_DISABLE();
   write_exposure(500);
   write_gain(1);
   write_r_gain(1);
   write_g_gain(25);
   write_b_gain(1);
   cam_config(0x17,0x12);       //horizontal frame start
   cam_config(0x18,0x61);       //horizontal frame stop
   cam_config(0x32,0xBA);       //horizontal frame start/stop LSB
   
   //cam_config(0x41,0x30);       //bypass AWB gain   
   //cam_config(0x71,0xFF);       //test pattern
   
   CAM_CNTRL.run=1;   
}

/*************************************************************************
 *    Funktion: cam_config()
 *
 *    Beschreibung: - Schreibt "value" in das Kameraregister mit der Addr. "SubAddr".
 *                    und überprüft anschließend, ob der Wert geschrieben wurde.
 *  
 **************************************************************************/

void cam_config(unsigned char SubAddr, unsigned char value)
{
    unsigned char temp[1];   
    unsigned char add[1];
    add[0]= SubAddr;
   
    CAM_CNTRL.data_package[0]=SubAddr;
    CAM_CNTRL.data_package[1]= value;
  
    //3 versuche
    unsigned char done = 0, try = 0;
    while(done == 0)
    {
      i2c_send(0x21,CAM_CNTRL.data_package,2);
    
      //Wert auslesen
      i2c_send_receive(0x21,add,1,1);
      i2c_get_data(temp);    
  
      if((I2C2_CNTRL.Error_ID == ok && temp[0] == value) || try == 3)
        done = 1;
      else
        try++;
    }
  
    //Status Flag entsprechend setzen
    if(temp[0] == value && I2C2_CNTRL.Error_ID == ok && CAM_OKAY)
      CAM_OKAY = 1;
    else
      CAM_OKAY = 0;    
}

/*************************************************************************
 *    Funktion: cam_read(SubAddr)
 *
 *    Beschreibung: - Liest das Kameraregister mit der Addr. "SubAddr".
 *                    und sendet es an den Master.
 *  
 **************************************************************************/

unsigned char cam_read(unsigned char SubAddr)
{
    unsigned char temp[1];   
    unsigned char add[1];
    add[0]= SubAddr;
   
    CAM_CNTRL.data_package[0]=SubAddr;
  
    //3 versuche
    unsigned char done = 0, try = 0;
    while(done == 0)
    {   
      //Wert auslesen
      i2c_send_receive(0x21,add,1,1);
      i2c_get_data(temp);    
  
      if(I2C2_CNTRL.Error_ID == ok || try == 3)
        done = 1;
      else
        try++;
    }
    
    //Status Flag entsprechend setzen
    if(I2C2_CNTRL.Error_ID == ok)// && CAM_OKAY)
      CAM_OKAY = 1;
    else
      CAM_OKAY = 0;        
  
    return temp[0];
}

/*************************************************************************
 *    Funktion: save_bin_pic()
 *
 *    Beschreibung: - Nimmt ein Binärbild auf und normalisiert es anhand der vorher berechneten
 *                    Maxima des Bildes. Der Schwellwert wird knapp unter den Durchschnitt des
 *                    Graubildes gelegt, dieser wird ebenfalls normalisiert.
 *  
 **************************************************************************/

void save_bin_pic(void)
{ 
  //disable interrupts
  NVIC_IntDisable(NVIC_TIMER0);
  NVIC_IntDisable(NVIC_TIMER1);
  NVIC_IntDisable(NVIC_I2C0);
  NVIC_IntDisable(NVIC_I2C2);
  __disable_interrupt();  
  
  CAM_CNTRL.picture_ready=0;

  //um speicherplatz zu sparen, wurde auf den datentyp double verzichtet.
  //stattdessen wurden werte um den faktor 10 hochskaliert, um nachkommastellen zu vermeiden
  //formel zur normalisierung:
  // ((Nmax / (Omax - Omin)) * (pix - Omin)) + Nmin
  //Nmax,Nmin -  Maxima des normalisierten Bildes
  //Omax, Omin - Maxima des alten Bildes
  unsigned int a = 2550.0 / (CAM_CNTRL.histogram_max - CAM_CNTRL.histogram_min); //normalisierungskonstante hochskaliert
  volatile unsigned int bin_thresh = (a * (CAM_CNTRL.histogram_avg - CAM_CNTRL.histogram_min)) - 100; //schwellwert hochskaliert
  volatile unsigned char LCLK = 0, 
                         b =  255 - CAM_CNTRL.histogram_min; //255 - Omin, beeinhaltet umkehrung des ausgelesenen pixels (zur optimierung)

  //vorhandenes bild löschen
  for(int i=0; i<19200; i++)
    CAM_CNTRL.Picture[i] = 0;
  LED2(LED_ON);
  
  while(CAM_CNTRL.picture_ready==0)
  {
      // auf VD Flanke warten, pin wird durch bit bending schneller ausgelesen
      asm ("        mov    r0,#0x0AAC        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_vd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_vd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_vd             ");  
      
    
      // auf frame loop warten und initialisieren
      CAM_CNTRL.byte_address=0;
      LCLK = 0;
      do
      {
          asm ("    mov    r0,#0x0AB4        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_hd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_hd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_hd             ");

        // zeilen schleife
        CAM_CNTRL.pixel_count=0;
        CAM_CNTRL.DCLK_count=1;
        
        do      //auf pixel clock flanke warten
        { 
          asm ("      mov    r0,#0x0AB0        \n"
         "            movt   r0,#0x2338        \n"
         "            ldr   r2,[r0]            \n" 
         "start_dclk: ldr   r1,[r0]            \n"
         "            cmp   r1,r2              \n"
         "            beq.w start_dclk         \n"  
         "            movs  r2,r1              \n"
         "            beq.w start_dclk           ");
   
        CAM_CNTRL.DCLK_count++;
        
          if(CAM_CNTRL.DCLK_count==2)   //nur jeden zweiten Pixel auslesen, sprich nur den Y (Helligkeits) Wert
          {
            if(LCLK == 0)               //nur jede zweite Zeile auslesen
            {
              //daten pins auslesen
              unsigned char CAM_D7 = FIO1PIN_bit.P1_15;
              unsigned char CAM_D6 = FIO1PIN_bit.P1_14;
              unsigned char CAM_D5 = FIO1PIN_bit.P1_10;
              unsigned char CAM_D4 = FIO1PIN_bit.P1_9;
              unsigned char CAM_D3 = FIO1PIN_bit.P1_8;
              unsigned char CAM_D2 = FIO1PIN_bit.P1_4;
              unsigned char CAM_D1 = FIO1PIN_bit.P1_1;
              unsigned char CAM_D0 = FIO1PIN_bit.P1_0;

              //zu einem byte zusammenschieben und umkehren
              volatile unsigned char CAM_DATA = b - ((CAM_D0) | (CAM_D1 << 1) | (CAM_D2 << 2) | (CAM_D3 << 3) |(CAM_D4 << 4) | (CAM_D5 << 5) | (CAM_D6 << 6) | (CAM_D7 << 7));

              //wert normalisieren, mit schwellwert vergleichen und nächstes bit in den picture array schreiben
              if(a * CAM_DATA > bin_thresh);     //schwarz -> bleibt 0x00            
              else
                CAM_CNTRL.Picture[CAM_CNTRL.byte_address] = CAM_CNTRL.Picture[CAM_CNTRL.byte_address] | (1 << CAM_CNTRL.bit_count); //weiß -> 0xFF
                 
              CAM_CNTRL.bit_count++;            
              if(CAM_CNTRL.bit_count == 8)      //nach 8 bit zum nächsten byte im array wechseln
              {
                CAM_CNTRL.byte_address++;
                CAM_CNTRL.bit_count = 0;
              }
            }
            CAM_CNTRL.pixel_count++;
            CAM_CNTRL.DCLK_count=0;              
          }
        } while ( CAM_CNTRL.pixel_count<320 );

        //zeile zählen um nur jede zweite auszulesen
        if(LCLK == 1)
          LCLK = 0;
        else
          LCLK++;

      } while (CAM_CNTRL.byte_address<9600);// && I2C0_CNTRL.Busy==free); //wenn 9600 (240*320/8) bytes geschrieben wurden aufhören 

      CAM_CNTRL.picture_ready=1;
  }

  LED2(LED_OFF);
  //enable interrupts
  NVIC_IntEnable(NVIC_TIMER0);
  NVIC_IntEnable(NVIC_TIMER1);
  NVIC_IntEnable(NVIC_I2C0);
  NVIC_IntEnable(NVIC_I2C2);
  __enable_interrupt();    
}

/*************************************************************************
 *    Funktion: get_height()
 *
 *    Beschreibung: - Berechnet die Hoehe anhand des Binaerbildes. Gruppiert hierbei zusammenliegende schwarze Pixel
 *                    und misst ihre Größe. Punkte, welche die Durchschnittsgröße +- 60% haben werden als echte
 *                    Punkte gezählt. Die Anzahl der Punkte gibt die Höhe von 0%-100%.
 *  
 **************************************************************************/

void get_height(void)
{  
  //gruppierungsalgorithmus ab folie 6 http://www.intelligence.tuc.gr/~petrakis/courses/computervision/binary.pdf
  unsigned int labels_above[320], new_label = 0;
  
  for(unsigned int i = 0; i < 320; i++) //arrays initialisieren
    labels_above[i] = 0;
  for(unsigned int i = 0; i < 1000; i++)
    CAM_CNTRL.blobs[i] = 0;  
  
  for (unsigned int y = 1; y < 240; y++) //zeilen loop
  { 
    unsigned int label_left = 0;    
    for (unsigned int x = 1; x < 40; x++) //spalten loop
    {
        for (unsigned int bit = 0; bit < 8; bit++) //byte loop
        {
            if (((CAM_CNTRL.Picture[y * 40 + x] >> bit) & 1) == 1) //pixel schwarz
            {
              unsigned int label_above = labels_above[x * 8 + bit], label;
                  if (label_left > 0 && label_above == 0) //only left
                      label = label_left;
                  else if (label_above > 0 && label_left == 0) //only above
                      label = label_above;
                  else if (label_left == label_above)
                  {
                      if (label_left == 0) //none, new label
                      {
                          label = new_label;
                          new_label++;
                      }
                      else //same, copy label
                          label = label_left;
                  }
                  else //different and non-zero
                  {
                      label = label_left;
                      CAM_CNTRL.blobs[label] += CAM_CNTRL.blobs[label_above]; //labelgrößen zusammenführen und eine löschen
                      CAM_CNTRL.blobs[label_above] = 0;
                  }
                  CAM_CNTRL.blobs[label] += 1; //increment label size
                  labels_above[x * 8 + bit] = label; //save for row below
                  label_left = label; //save for next pixel
            }
            else
            {
                label_left = 0; //save for next pixel
                labels_above[x * 8 + bit] = 0; //save for row below
            }             
        }
     }
  }

            //durchschnitt bilden, punkte herausfiltern und zaehlen
            volatile unsigned int index = 0, sum = 0, count = 0, max_blob = 0;
            for(unsigned int i = 0; i < 1500; i++)
            {
              if(CAM_CNTRL.blobs[i] > 5) //nur punkte größer als 5 um rauschen zu filtern
              {
                unsigned int temp = CAM_CNTRL.blobs[i];
                CAM_CNTRL.blobs[i] = 0;
                CAM_CNTRL.blobs[index] = temp;
                if(CAM_CNTRL.blobs[index] > max_blob) //maximum finden
                  max_blob = CAM_CNTRL.blobs[index];
                sum += temp;
                index++;
              }                        
            }
            
            unsigned int avg = (sum - max_blob)/index;  //durchschnitt ohne max und punkte kleiner als 5
            //aus blob array herausfiltern +-60%
            for(unsigned int i = 0; i < index; i++)
              if(CAM_CNTRL.blobs[i] < avg + avg * 0.6 && CAM_CNTRL.blobs[i] > avg - avg * 0.6)
                count++;
            
            //unsigned int min = 20;
            //unsigned int max = 300;
            double a = 100.0 / (300 - 20);
            CAM_CNTRL.height = a * (count - 20);
}


/*************************************************************************
 *    Funktion: save_grey_pic(unsigned char section)
 *
 *    Beschreibung: - Nimmt einen Graubildabschnitt auf, der durch das parameter bestimmt wird.
 *  
 **************************************************************************/
void save_grey_pic(unsigned char section)
{ 
  //disable interrupts
  NVIC_IntDisable(NVIC_TIMER0);
  NVIC_IntDisable(NVIC_TIMER1);
  NVIC_IntDisable(NVIC_I2C0);
  NVIC_IntDisable(NVIC_I2C2);
  __disable_interrupt();    
  
  CAM_CNTRL.picture_ready=0;
  unsigned int linecount = 0, currentsection = 0, LCLK = 0;
  
  //vorhandenes bild löschen
  for(int i=0; i<19200; i++)
    CAM_CNTRL.Picture[i] = 0;  

  LED2(LED_ON);
  
  while(CAM_CNTRL.picture_ready==0)
  {
      // auf VD flanke warten
      asm ("        mov    r0,#0x0AAC        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_vd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_vd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_vd             ");  
       
      // auf frame schleife warten und initialisieren
      CAM_CNTRL.byte_address=0;
      do
      {
          asm ("    mov    r0,#0x0AB4        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_hd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_hd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_hd             ");

        // zeilen schleife 
        if(linecount == 60)     //alle 60 zeilen abschnitte trennen
        {
          currentsection++;
          linecount = 0;
        }
        
        CAM_CNTRL.pixel_count=0;
        CAM_CNTRL.DCLK_count=1;

        do      //auf pixel clock flanke warten
        {
          asm ("      mov    r0,#0x0AB0        \n"
         "            movt   r0,#0x2338        \n"
         "            ldr   r2,[r0]            \n" 
         "start_dclk: ldr   r1,[r0]            \n"
         "            cmp   r1,r2              \n"
         "            beq.w start_dclk         \n"  
         "            movs  r2,r1              \n"
         "            beq.w start_dclk           ");
 
        CAM_CNTRL.DCLK_count++;
        
          if(CAM_CNTRL.DCLK_count==2)   //nur jeden zweiten Pixel auslesen, sprich nur den Y (Helligkeits) Wert
          {
            if(section == currentsection && LCLK == 0)  //jede zweite zeile auslesen, aber nur im richtigen abschnitt
            {
              unsigned char CAM_D7 = FIO1PIN_bit.P1_15;
              unsigned char CAM_D6 = FIO1PIN_bit.P1_14;
              unsigned char CAM_D5 = FIO1PIN_bit.P1_10;
              unsigned char CAM_D4 = FIO1PIN_bit.P1_9;
              unsigned char CAM_D3 = FIO1PIN_bit.P1_8;
              unsigned char CAM_D2 = FIO1PIN_bit.P1_4;
              unsigned char CAM_D1 = FIO1PIN_bit.P1_1;
              unsigned char CAM_D0 = FIO1PIN_bit.P1_0;
              
              volatile unsigned char CAM_DATA = 255 - ((CAM_D0) | (CAM_D1 << 1) | (CAM_D2 << 2) | (CAM_D3 << 3) |(CAM_D4 << 4) | (CAM_D5 << 5) | (CAM_D6 << 6) | (CAM_D7 << 7));
              CAM_CNTRL.Picture[CAM_CNTRL.byte_address] = CAM_DATA;     //ganzes byte speichern
              CAM_CNTRL.byte_address++;              
            }    
            CAM_CNTRL.DCLK_count=0;
            CAM_CNTRL.pixel_count++;            
          }
        } while ( CAM_CNTRL.pixel_count<320 );
        
              //jede zweite zeile zählen
                if(LCLK == 0)
                  linecount++;
        
                if(LCLK == 1)
                  LCLK = 0;
                else
                  LCLK++;                
                
     
      } while (CAM_CNTRL.byte_address<19200 && I2C0_CNTRL.Busy==free); 
      
      CAM_CNTRL.picture_ready=1;
  }
  LED2(LED_OFF);
  PWM1_DISABLE();
  PWM1_ENABLE();
  //PWM2_DISABLE();
  //PWM3_DISABLE();
  
  //enable interrupts
  NVIC_IntEnable(NVIC_TIMER0);
  NVIC_IntEnable(NVIC_TIMER1);
  NVIC_IntEnable(NVIC_I2C0);
  NVIC_IntEnable(NVIC_I2C2);
  __enable_interrupt();   
}

/*************************************************************************
 *    Funktion: analyse_grey_pic(unsigned char section)
 *
 *    Beschreibung: - Berechnet Durchschnitt, Maxima und Minima des Graubildes
 *  
 **************************************************************************/
void analyse_grey_pic()
{ 
  //disable interrupts
  NVIC_IntDisable(NVIC_TIMER0);
  NVIC_IntDisable(NVIC_TIMER1);
  NVIC_IntDisable(NVIC_I2C0);
  NVIC_IntDisable(NVIC_I2C2);
  __disable_interrupt();    
  
  CAM_CNTRL.picture_ready=0;
  unsigned int linecount = 0, LCLK = 0;
  unsigned char row_avg[240];
  unsigned int row_sum = 0;
  CAM_CNTRL.histogram_max = 0;
  CAM_CNTRL.histogram_min = 255;

  LED2(LED_ON);
  
  while(CAM_CNTRL.picture_ready==0)
  {
      // auf VD flanke warten
      asm ("        mov    r0,#0x0AAC        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_vd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_vd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_vd             ");  
       
      // auf frame schleife warten und initialisieren
      CAM_CNTRL.byte_address=0;
      do
      {
          asm ("    mov    r0,#0x0AB4        \n"
         "          movt   r0,#0x2338        \n"
         "          ldr   r2,[r0]            \n"
         "start_hd: ldr   r1,[r0]            \n"
         "          cmp   r1,r2              \n"
         "          beq.w start_hd           \n"  
         "          movs  r2,r1              \n"
         "          beq.w start_hd             ");

        // zeilen schleife         
        CAM_CNTRL.pixel_count=0;
        CAM_CNTRL.DCLK_count=1;
        row_sum = 0;

        do      //auf pixel clock flanke warten
        {
          asm ("      mov    r0,#0x0AB0        \n"
         "            movt   r0,#0x2338        \n"
         "            ldr   r2,[r0]            \n" 
         "start_dclk: ldr   r1,[r0]            \n"
         "            cmp   r1,r2              \n"
         "            beq.w start_dclk         \n"  
         "            movs  r2,r1              \n"
         "            beq.w start_dclk           ");
 
        CAM_CNTRL.DCLK_count++;
        
          if(CAM_CNTRL.DCLK_count==2)   //nur jeden zweiten Pixel auslesen, sprich nur den Y (Helligkeits) Wert
          {
            if(LCLK == 0)  //jede zweite zeile auslesen, aber nur im richtigen abschnitt
            {
              unsigned char CAM_D7 = FIO1PIN_bit.P1_15;
              unsigned char CAM_D6 = FIO1PIN_bit.P1_14;
              unsigned char CAM_D5 = FIO1PIN_bit.P1_10;
              unsigned char CAM_D4 = FIO1PIN_bit.P1_9;
              unsigned char CAM_D3 = FIO1PIN_bit.P1_8;
              unsigned char CAM_D2 = FIO1PIN_bit.P1_4;
              unsigned char CAM_D1 = FIO1PIN_bit.P1_1;
              unsigned char CAM_D0 = FIO1PIN_bit.P1_0;
              
              volatile unsigned char CAM_DATA = 255 - ((CAM_D0) | (CAM_D1 << 1) | (CAM_D2 << 2) | (CAM_D3 << 3) |(CAM_D4 << 4) | (CAM_D5 << 5) | (CAM_D6 << 6) | (CAM_D7 << 7));
              row_sum += CAM_DATA;
              if(CAM_DATA > CAM_CNTRL.histogram_max)
                CAM_CNTRL.histogram_max = CAM_DATA;
              if(CAM_DATA < CAM_CNTRL.histogram_min)
                CAM_CNTRL.histogram_min = CAM_DATA;
              if(CAM_DATA < 2)
                CAM_CNTRL.histogram_min = CAM_DATA;              
              CAM_CNTRL.byte_address++;
            }    
            CAM_CNTRL.DCLK_count=0;
            CAM_CNTRL.pixel_count++;            
          }
        } while ( CAM_CNTRL.pixel_count<320 );
        
        //jede zweite zeile zählen
        if(LCLK == 0)
        {
          row_avg[linecount] = row_sum / 320.0;
          linecount++;        
        }
        if(LCLK == 1)
          LCLK = 0;
        else
          LCLK++;
                
      } while (CAM_CNTRL.byte_address<76800 && I2C0_CNTRL.Busy==free); 
      
      int sum = 0;
      for(int i = 0; i < 240; i++)
        sum += row_avg[i];
      CAM_CNTRL.histogram_avg = sum / 240.0;
      CAM_CNTRL.picture_ready=1;
  }
  LED2(LED_OFF);
  PWM1_DISABLE();
  PWM1_ENABLE();
  //PWM2_DISABLE();
  //PWM3_DISABLE();
  
  //enable interrupts
  NVIC_IntEnable(NVIC_TIMER0);
  NVIC_IntEnable(NVIC_TIMER1);
  NVIC_IntEnable(NVIC_I2C0);
  NVIC_IntEnable(NVIC_I2C2);
  __enable_interrupt();   
}

/*************************************************************************
 *    Funktion: AGC_ENABLE()/DISABLE()
 *
 *    Beschreibung: - Aktiviert oder Deaktiviert Automatic Gain Control
 *
 *  
 **************************************************************************/
void AGC_ENABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 | 0x04;
  cam_config(0x13, COM8);
}

void AGC_DISABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 & ~0x04;
  cam_config(0x13, COM8);
}

/*************************************************************************
 *    Funktion: AWB_ENABLE()/DISABLE()
 *
 *    Beschreibung: - Aktiviert oder Deaktiviert Automatic White Balance
 *
 *  
 **************************************************************************/
void AWB_ENABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 | 0x02;
  cam_config(0x13, COM8);
}

void AWB_DISABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 & ~0x02;
  cam_config(0x13, COM8);
}

/*************************************************************************
 *    Funktion: AEC_ENABLE()/DISABLE()
 *
 *    Beschreibung: - Aktiviert oder Deaktiviert Automatic Exposure Control
 *
 *  
 **************************************************************************/
void AEC_ENABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 | 0x01;
  cam_config(0x13, COM8);
}

void AEC_DISABLE()
{
  unsigned char COM8 = cam_read(0x13);
  COM8 = COM8 & ~0x01;
  cam_config(0x13, COM8);
}

/*************************************************************************
 *    Funktion: write/read_exposure()
 *
 *    Beschreibung: - Schreibt/Liest die aktuelle Belichtungszeit in millisekunden ein/aus
 *
 *  
 **************************************************************************/
int read_exposure()
{
  unsigned char COM1 = cam_read(0x04);
  unsigned char AEC = cam_read(0x10);
  unsigned char AECHH = cam_read(0x07);
  unsigned char COM1_bits = COM1 & 0x03;
  unsigned char AECHH_bits = AECHH & 0x3F;
  //shift for concatenation
  int AECHH2 = AECHH_bits << 10;
  int AEC2 = AEC << 2;
  
  //concatenate
  int exposure_reg = AECHH2 | AEC2 | COM1_bits;
  int exposure = (int)exposure_reg * 1.9;
  return exposure;
}

void write_exposure(int exposure)
{
  int exposure_reg = (int)exposure/1.9;
  unsigned char COM1 = exposure_reg & 0x03;
  unsigned char AEC = (exposure_reg >> 2) & 0xFF;
  unsigned char AECHH = (exposure_reg >> 10) & 0x3F;
  cam_config(0x04, COM1);
  cam_config(0x10, AEC);
  cam_config(0x07, AECHH);
}

/*************************************************************************
 *    Funktion: write/read_gain()
 *
 *    Beschreibung: - Schreibt/Liest den aktuellen gain ein/aus
 *
 *  
 **************************************************************************/
int read_gain()
{
  unsigned char gainreg = cam_read(0x00);
  unsigned char vref = cam_read(0x03);
  unsigned char a = (vref >> 7) & 0x01;
  unsigned char b = (vref >> 6) & 0x01;
  unsigned char c = (gainreg >> 7) & 0x01;
  unsigned char d = (gainreg >> 6) & 0x01;
  unsigned char e = (gainreg >> 5) & 0x01;
  unsigned char f = (gainreg >> 4) & 0x01;
  unsigned char g = gainreg & 0x0F;
  
  int gainval = (a + 1) * (b + 1) * (c + 1) * (d + 1) * (e + 1) * (f + 1) * (g / 15 + 1);
  return gainval;
}

/*************************************************************************
 *    Funktion: write/read_r/g/b_gain()
 *
 *    Beschreibung: - Schreibt/Liest den aktuellen rgb gain ein/aus
 *
 *  
 **************************************************************************/
unsigned char read_r_gain()
{
  return cam_read(0x02);
}

unsigned char read_g_gain()
{
  return cam_read(0x6A);
}

unsigned char read_b_gain()
{
  return cam_read(0x01);
}

void write_r_gain(unsigned char gain)
{
  cam_config(0x02, gain);
}

void write_g_gain(unsigned char gain)
{
  cam_config(0x6A, gain);
}

void write_b_gain(unsigned char gain)
{
  cam_config(0x01, gain);
}

/*************************************************************************
 *    Funktion: write/read_gain()
 *
 *    Beschreibung: - Schreibt/Liest den aktuellen gain ein/aus
 *
 *  
 **************************************************************************/
void write_gain(int gain)
{
  int n = floor(log(gain)/log(2));
  unsigned char gainreg = 0, vref = 0, a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0;
  
  if (n > 0)
    a = 0x01 << 7;
  if (n > 1)
    b = 0x01 << 6;
  if (n > 2)
    c = 0x01 << 7;
  if (n > 3)
    d = 0x01 << 6;
  if (n > 4)
    e = 0x01 << 5;
  if (n > 5)
    f = 0x01 << 4;
  if (n > 6)
    g = 0x0F;
  
  vref = a | b;
  gainreg = c | d | e | f | g;
  cam_config(0x00, gainreg);
  cam_config(0x03, vref);
}

/*************************************************************************
 *    Funktion: test_pic()
 *
 *    Beschreibung: - Erstellt ein Testbild mit verschiedenen Graustreifen
 *                   zum testen der Graubildübertragung.
 *
 *  
 **************************************************************************/

void test_pic (void)
{
  unsigned int spalte;
  //vorhandenes bild löschen
  for(int i=0; i<19200; i++)
    CAM_CNTRL.Picture[i] = 0;   
  CAM_CNTRL.byte_address = 0;
 
  do{
   for(spalte=0;spalte<40;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0xFF;  //weiß     
     CAM_CNTRL.byte_address++;
   }
   
   for(spalte=40;spalte<80;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0xDB;       
     CAM_CNTRL.byte_address++;
   }   
   
   for(spalte=80;spalte<120;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0xB7;       
     CAM_CNTRL.byte_address++;
   }

   for(spalte=120;spalte<160;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0x93;       
     CAM_CNTRL.byte_address++;
   }   
   
   for(spalte=160;spalte<200;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0x6F;       
     CAM_CNTRL.byte_address++;
   }   
   
   for(spalte=200;spalte<240;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0x4B;       
     CAM_CNTRL.byte_address++;
   }   
   
   for(spalte=240;spalte<280;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0x27;       
     CAM_CNTRL.byte_address++;
   }   
   
   for(spalte=280;spalte<320;spalte++)
   {
     CAM_CNTRL.Picture[CAM_CNTRL.byte_address]=0x00;    //schwarz      
     CAM_CNTRL.byte_address++;
   }   

}while(CAM_CNTRL.byte_address<19200);
 
}



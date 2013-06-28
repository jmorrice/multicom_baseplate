/********************************************************************************
 *    Bibliothek: incl.h
 *
 *    Ziel-Device: NXP LPC 1768 ARM Cortex M3    
 *
 *    Autor: Erik Schröder, Mult.Com GmbH
 *
 *    Datum: 08.07.2011   
 *
 *    Beschreibung: - Enthält alle Header-Files des Projektes.
 *                  - Enthält allgemeine defines.
 *    
 ********************************************************************************/

/****** Defines *******/
#define BOOT_START     0x0000
#define BOOT_SIZE      0x5000
#define USER_PROGRAM_START (BOOT_START + BOOT_SIZE)

#define BITBAND_SRAM_REF   0x20000000
#define BITBAND_SRAM_BASE  0x22000000
#define BITBAND_SRAM(a,b) ((BITBAND_SRAM_BASE + (a-BITBAND_SRAM_REF)*32 + (b*4)))  // Convert SRAM address

/****** Includes *******/

#include "iolpc1766.h"
#include "default.h"
#include "fsm.h"
#include "nvic.h"
#include "pwm.h"
#include "user_code.h"
#include "bootloader.h"

#include "i2c_slave.h"
#include "i2c_master.h"
#include "i2c.h"

#include "sm5822.h"

#include "cam.h"
#include "bildverarbeitung.h"

#include "flash.h"



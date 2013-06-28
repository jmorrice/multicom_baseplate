
#define FIRMWARE_MAJOR 00
#define FIRMWARE_MINOR 02

enum CELL_STAT { INIT_ARM, INIT_EXT , WAIT_ADDR , WAIT_OP, OP };

extern int firmware_major, firmware_minor;

void fsm();
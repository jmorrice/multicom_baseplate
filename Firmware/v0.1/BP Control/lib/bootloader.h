

#define SECTOR_SIZE  4096
#define PACKAGE_SIZE  45

#define BOOTLOADER_MAJOR 00
#define BOOTLOADER_MINOR 05
enum status {
  
  OK=0,
  PREP_ERR,
  WRITE_ERR,
  CHECKSUM_ERR,
  COUNT_ERR,
  LOCKED_SECTOR,
  PACKAGE_ID_ERR,
  READY_TO_FLASH,
  WRITE_SUCCESS,
  COMPARE_ERR
};


  
  
struct boot
{
  unsigned int byte_count;
  unsigned int sector_count;
  unsigned int package_count;
  enum status flash_status;
  unsigned long start_address;
  unsigned char sector;
  unsigned int checksum_received;
  unsigned int checksum_calculated;
  unsigned int bytes_per_sector[5];
  unsigned int bytes_per_package[250];
  unsigned char finished_sectors;
  unsigned int array_pos;
  unsigned char received_packages;
  unsigned int  package_id;
  unsigned int package_id_old;
  unsigned long start_address_const;
};

extern int bootloader_minor;
extern int bootloader_major; 

void boot_core(void);

unsigned int sector(unsigned long addr);

void Prepare_Flash(void);

void receive_firmware(void);

void update_firmware(void);

unsigned long calculate_rom_address(unsigned int);

void send_boot_status(void);

int i2c_test_connection(void);

 unsigned int calculate_array_pos(unsigned int sector);
 
 void send_bootloader_version(void);
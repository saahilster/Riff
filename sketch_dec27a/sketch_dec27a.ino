#include <SPI.h>
#include <Wire.h>
#include <MFRC522.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

// For RFID Scanner
#define SS_PIN 10
#define RST_PIN 9

MFRC522 reader(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;

byte sector = 1;
byte blockAddress = 4;
byte trailerBlock = 7;
byte buffer[18];
byte size = sizeof(buffer);
MFRC522::StatusCode status;

byte mageBlock[] = {
  1, 1, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0
};

byte knightBlock[] = {
  0, 1, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0
};

byte medicBlock[] = {
  2, 1, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0,
  0, 0, 0, 0
};

void setup() {
  Serial.begin(9600);
  SPI.begin();
  reader.PCD_Init();

  for (byte i = 0; i < 6; i++){
    key.keyByte[i] = 0xFF;
  }

  DumpByte(key.keyByte, MFRC522::MF_KEY_SIZE);
  Serial.println();

  Serial.println("Welcome to Fig Flasher this will be used to configure your figures meta data!");
}

void loop() {
  if(!reader.PICC_IsNewCardPresent()) return;

  if(!reader.PICC_ReadCardSerial()) return;

  Serial.print(F("Card UID:"));
  DumpByte(buffer, size);
  Serial.println();
  Serial.print(F("PICC type: "));
  MFRC522::PICC_Type piccType = reader.PICC_GetType(reader.uid.sak);
  Serial.println(reader.PICC_GetTypeName(piccType));

  Serial.println(F("Authenticating A"));
  status = (MFRC522::StatusCode) reader.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, trailerBlock, &key, &(reader.uid));
  if (status != MFRC522::STATUS_OK){
    Serial.println(F("PCD Authenticate A failed: "));
    Serial.println(reader.GetStatusCodeName(status));
    return;
  }

  Serial.println(F("Current data in sector: "));
  reader.PICC_DumpMifareClassicSectorToSerial(&(reader.uid), &key, sector);
  Serial.println();

  Serial.print(F("Reading data from block")); Serial.print(blockAddress);
  Serial.println(F("...."));
  status = (MFRC522::StatusCode) reader.MIFARE_Read(blockAddress, buffer, &size);
  if (status != MFRC522::STATUS_OK){
    Serial.print(F("Mifare read failed"));
    Serial.println(reader.GetStatusCodeName(status));
  }

  Serial.print(F("Data in block: ")); Serial.print(blockAddress); Serial.println(F(":"));
  DumpByte(buffer, 16); Serial.println();
  Serial.println();

  //Authenticate B
  Serial.println(F("Authenticating B"));
  status = (MFRC522::StatusCode) reader.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_B, trailerBlock, &key, &(reader.uid));
  if (status != MFRC522::STATUS_OK){
    Serial.print("Authentication B failed");
    return;
  }

  Serial.print("Writing Data"); 
  Serial.print(F("..."));
  DumpByte(mageBlock, 16); Serial.println();
  status = (MFRC522::StatusCode) reader.MIFARE_Write(blockAddress, medicBlock, 16);
  if (status != MFRC522::STATUS_OK){
    Serial.print("Writing failed");
    return;
  }
  Serial.print("Writing Success!");
  Serial.println("Reading new data");

  status = (MFRC522::StatusCode) reader.MIFARE_Read(blockAddress, buffer, &size);
  if(status != MFRC522::STATUS_OK){
    Serial.println("Read after data writing failed");
    return;
  }

  Serial.print(F("Data in block: ")); Serial.print(blockAddress); Serial.println(F(":"));
  DumpByte(buffer, 16); Serial.println();

  reader.PICC_HaltA();
  reader.PCD_StopCrypto1();
}

 void DumpByte(byte *buffer, byte bufferSize){
  for (byte i = 0; i < bufferSize; i++){
    Serial.print(buffer[i] < 0x10 ? "0" : " ");
    Serial.print(buffer[i], HEX);
  }
}






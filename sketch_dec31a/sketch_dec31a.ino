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
MFRC522::StatusCode status;

byte dataBlock[] = {};
byte blockAddress = 4;
byte trailerBlock = 7;
byte buffer[18];
byte size = sizeof(buffer);

//Only commands now is needing the arduino to summon and save data.
String unityCommands[] = {
  "SUMMON",
  "SAVE"
};

String order;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  SPI.begin();
  reader.PCD_Init();

  for (byte i = 0; i < 6; i++) {
    key.keyByte[i] = 0xFF;
  }

  DumpByte(key.keyByte, MFRC522::MF_KEY_SIZE);
}

void loop() {
  String order = UnityOrder();  // read ONCE
  if (order.length() == 0) return;

  if (order == "SUMMON") {
    if (!reader.PICC_IsNewCardPresent() || !reader.PICC_ReadCardSerial()) {
      Serial.println("NO_CARD");
      reader.PICC_HaltA();
      reader.PCD_StopCrypto1();
      return;
    }

    Authenticate(blockAddress);
    status = (MFRC522::StatusCode)reader.MIFARE_Read(blockAddress, buffer, &size);
    if (status != MFRC522::STATUS_OK) {
      Serial.print("READ_FAIL:");
      Serial.println(reader.GetStatusCodeName(status));
      reader.PICC_HaltA();
      reader.PCD_StopCrypto1();
      return;
    }

    DumpByte(buffer, 16);
    Serial.println();
  } else if (order == unityCommands[1]) {
    Serial.println("Unity Request Save data");
    reader.PICC_HaltA();
    reader.PCD_StopCrypto1();
  }

  reader.PICC_HaltA();
  reader.PCD_StopCrypto1();
}

void DumpByte(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? "0" : " ");
    Serial.print(buffer[i], HEX);
  }
}

void Authenticate(byte block) {
  status = (MFRC522::StatusCode)reader.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_A, block, &key, &(reader.uid));
  if (status != MFRC522::STATUS_OK) {
    // Serial.println(F("Authenticate A failed"));
  }

  status = (MFRC522::StatusCode)reader.PCD_Authenticate(MFRC522::PICC_CMD_MF_AUTH_KEY_B, block, &key, &(reader.uid));
  if (status != MFRC522::STATUS_OK) {
    // Serial.println(F("Authenticate B failed"));
  }
}

//This function reads data block and will send it to unity.
void ReadCharacter(byte blockAddr) {
  Authenticate(blockAddress);
  status = (MFRC522::StatusCode)reader.MIFARE_Read(blockAddr, buffer, &size);
  if (status != MFRC522::STATUS_OK) {
    Serial.print("READ_FAIL:");
    Serial.println(reader.GetStatusCodeName(status));
    return;
  }
  DumpByte(buffer, 16);
  Serial.println();
}

String UnityOrder() {
  String order = Serial.readStringUntil('\n');
  order.trim();
  return order;
}

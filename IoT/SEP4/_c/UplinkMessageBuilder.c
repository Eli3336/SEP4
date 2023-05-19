#include "UplinkMessageBuilder.h"
#include "DataHolder.h"

static uint16_t _humidity;
static uint16_t _temperature;
static uint16_t _co2;

static uint8_t _validationBits;

lora_driver_payload_t
uplinkMessageBuilder_buildUplinkMessage(uint8_t port) {
	lora_driver_payload_t payload;
	
	payload.portNo = port;
	payload.len = 7;
	
	payload.bytes[0] = _humidity >> 8;
	payload.bytes[1] = _humidity & 0xFF;
	payload.bytes[2] = _temperature >> 8;
	payload.bytes[3] = _temperature & 0xFF;
	payload.bytes[4] = _co2 >> 8;
	payload.bytes[5] = _co2 & 0xFF;
	payload.bytes[6] = _validationBits;

	
	return payload;
}

void uplinkMessageBuilder_setHumidityData(uint16_t data) {
	_humidity = data;
	printf("the Hum is : %d \n",data);
}

void uplinkMessageBuilder_setTemperatureData(uint16_t data) {
	_temperature = data;
	printf("the temp value is: %d \n",data);
}

void uplinkMessageBuilder_setCO2Data(uint16_t data) {
	_co2 = data;
	printf("the Co2 value is: %d \n ",data);
}

void uplinkMessageBuilder_setSystemErrorState() {
	_validationBits = 0;
}
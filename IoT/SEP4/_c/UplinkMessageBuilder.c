#include "UplinkMessageBuilder.h"
#include "Config.h"

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
	
	if (data == CONFIG_INVALID_HUMIDITY_VALUE) {
		_validationBits |= 0 << 3;
	} else {
		_validationBits |=1 << 3;
	}
}

void uplinkMessageBuilder_setTemperatureData(uint16_t data) {
	_temperature = data;
	
	if (data == CONFIG_INVALID_TEMPERATURE_VALUE) {
		_validationBits |= 0 << 2;
		} else {
		_validationBits |=1 << 2;
	}
}

void uplinkMessageBuilder_setCO2Data(uint16_t data) {
	_co2 = data;
	
	if (data == CONFIG_INVALID_CO2_VALUE) {
		_validationBits |= 0 << 1;
		} else {
		_validationBits |=1 << 1;
	}
}

void uplinkMessageBuilder_setSystemErrorState() {
	_validationBits = 0;
}
#include <Configuration.h>
#include <semphr.h>

#define CHECK_BIT(variable, position) variable & (1 << position)

static SemaphoreHandle_t _mutex;

static uint16_t _humidityLOW;
static uint16_t _humidityHIGH;
static int16_t _temperatureLOW;
static int16_t _temperatureHIGH;
static uint16_t _ppmLOW;
static uint16_t _ppmHIGH;

void configuration_create(SemaphoreHandle_t mutex) {
	_mutex = mutex;
	
	_humidityLOW = CONFIG_INVALID_HUMIDITY_VALUE;
	_humidityHIGH = CONFIG_INVALID_HUMIDITY_VALUE;
	_temperatureLOW = CONFIG_INVALID_TEMPERATURE_VALUE;
	_temperatureHIGH = CONFIG_INVALID_TEMPERATURE_VALUE;
	_ppmLOW = CONFIG_INVALID_CO2_VALUE;
}

void configuration_setThresholds(lora_driver_payload_t payload) {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		
		if (CHECK_BIT(payload.bytes[14], 1)) {
			_ppmLOW = (payload.bytes[10] << 8) + payload.bytes[11];
		}
		
		if (CHECK_BIT(payload.bytes[14], 2)) {
			_ppmHIGH = (payload.bytes[8] << 8) + payload.bytes[9];
		}
		
		if (CHECK_BIT(payload.bytes[14], 3)) {
			_temperatureLOW = (payload.bytes[6] << 8) + payload.bytes[7];
		}
		
		if(CHECK_BIT(payload.bytes[14], 4)) {
			_temperatureHIGH = (payload.bytes[4] << 8) + payload.bytes[5];
		}
		
		if(CHECK_BIT(payload.bytes[14], 5)) {
			_humidityLOW = (payload.bytes[2] << 8) + payload.bytes[3];
		}
		
		if(CHECK_BIT(payload.bytes[14], 6)) {
			_humidityHIGH = (payload.bytes[0] << 8) + payload.bytes[1];
		}
		
		xSemaphoreGive(_mutex);
	}
}

uint16_t configuration_getLowHumidityThreshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _humidityLOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

int16_t configuration_getLowTemperatureThreshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _temperatureLOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t configuration_getLowCO2Threshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _ppmLOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t configuration_getHighHumidityThreshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _humidityHIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

int16_t configuration_getHighTemperatureThreshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _temperatureHIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t configuration_getHighCO2Threshold() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _ppmHIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

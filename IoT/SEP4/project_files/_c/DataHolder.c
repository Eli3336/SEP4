#include <DataHolder.h>
#include <semphr.h>

#define CHECK_BIT(variable, position) variable & (1 << position)

static SemaphoreHandle_t _mutex;
static SemaphoreHandle_t mutexAvgValues;

static uint16_t _humidityLOW;
static uint16_t _humidityHIGH;
static int16_t _temperatureLOW;
static int16_t _temperatureHIGH;
static uint16_t _co2LOW;
static uint16_t _co2HIGH;
static int16_t avgTemp;
static uint16_t avgHum;
static uint16_t avgCo2;

void dataHolder_create(SemaphoreHandle_t mutex,SemaphoreHandle_t _mutexAvgValues) {
	_mutex = mutex;
	
	_mutexAvgValues = mutexAvgValues;
	_humidityLOW = INVALID_HUMIDITY_VALUE;
	_humidityHIGH = INVALID_HUMIDITY_VALUE;
	_temperatureLOW = INVALID_TEMPERATURE_VALUE;
	_temperatureHIGH = INVALID_TEMPERATURE_VALUE;
	_co2LOW = INVALID_CO2_VALUE;
	_co2HIGH = INVALID_CO2_VALUE;
}

void dataHolder_setBreakpoints(lora_driver_payload_t payload) {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		
		// Checks for breakpoints to be set in byte 0. (flag)
		// HumiLow
		if(CHECK_BIT(payload.bytes[12], 0)) {
			_humidityLOW = (payload.bytes[1] << 8) + payload.bytes[0];
		}
		// HumiHigh
		if(CHECK_BIT(payload.bytes[12], 1)) {
			_humidityHIGH = (payload.bytes[3] << 8) + payload.bytes[2];
		}
		// TempLow
		if(CHECK_BIT(payload.bytes[12], 2)) {
			_temperatureLOW = (payload.bytes[5] << 8) + payload.bytes[4];
		}
		// TempHigh
		if(CHECK_BIT(payload.bytes[12], 3)) {
			_temperatureHIGH = (payload.bytes[7] << 8) + payload.bytes[6];
		}
		// Co2Low
		if(CHECK_BIT(payload.bytes[12], 4)) {
			_co2LOW = (payload.bytes[9] << 8) + payload.bytes[8];
		}
		// Co2High
		if(CHECK_BIT(payload.bytes[12], 5)) {
			_co2HIGH = (payload.bytes[11] << 8) + payload.bytes[10];
		}
		
		xSemaphoreGive(_mutex);
	}
}

uint16_t getHumidityBreakpointLow() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _humidityLOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

int16_t getTemperatureBreakpointLow() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _temperatureLOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t getCo2BreakpointLow() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _co2LOW;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t getHumidityBreakpointHigh() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _humidityHIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

int16_t getTemperatureBreakpointHigh() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _temperatureHIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

uint16_t getCo2BreakpointHigh() {
	if (xSemaphoreTake(_mutex, pdMS_TO_TICKS(3000)) == pdTRUE) {
		int16_t temp = _co2HIGH;
		xSemaphoreGive(_mutex);
		return temp;
	}
}

void addPPM(uint16_t ppm)
{
	co2Pool+=ppm;
	co2Count++;
}


void addTemperture(int16_t temperature)
{
	tempPool+=temperature;
	tempCount++;
}


void addHumidity(uint16_t humidity)
{
	humPool+=humidity;
	humCount++;
}

void resetAllCounterValues(){
	tempCount=0;
	tempPool=0;
	humCount=0;
	humPool=0;
	co2Count=0;
	co2Pool=0;
	
}

uint16_t getHumAvg() {
	if(xSemaphoreTake(mutexAvgValues, pdMS_TO_TICKS(100)) == pdTRUE)
	{
		uint16_t tempHum = avgHum;
		xSemaphoreGive(mutexAvgValues);
		return tempHum;
	}
}

uint16_t getCo2Avg() {
	if(xSemaphoreTake(mutexAvgValues, pdMS_TO_TICKS(2000)) == pdTRUE)
	{
		uint16_t tempCo2 = avgCo2;
		xSemaphoreGive(mutexAvgValues);
		return tempCo2;
	}
}

int16_t getTempAvg() {
	if(xSemaphoreTake(mutexAvgValues, pdMS_TO_TICKS(10000)) == pdTRUE)
	{
		int16_t tempTemp = avgTemp;
		xSemaphoreGive(mutexAvgValues);
		return tempTemp;
	}
}

void calculateAvg() {
	if(xSemaphoreTake(mutexAvgValues, pdMS_TO_TICKS(100)) == pdTRUE)
	{
		if(humCount != 0)
		{
			avgHum = humPool / humCount;
		}
		else
		{
			avgHum = INVALID_HUMIDITY_VALUE;
		}
		if(tempCount != 0)
		{
			avgTemp = tempPool / tempCount;
		}
		else
		{
			avgTemp = INVALID_TEMPERATURE_VALUE;
		}
		if(co2Count != 0)
		{
			avgCo2 = co2Pool / co2Count;
		}
		else
		{
			avgCo2 = INVALID_CO2_VALUE;
		}
		xSemaphoreGive(mutexAvgValues);
	}
}
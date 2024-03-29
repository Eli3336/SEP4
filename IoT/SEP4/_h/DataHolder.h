#include <ATMEGA_FreeRTOS.h>
#include <semphr.h>
#include <lora_driver.h>
#include <stdint.h>

#define INVALID_HUMIDITY_VALUE 0
#define INVALID_TEMPERATURE_VALUE -100
#define INVALID_CO2_VALUE 0
#define BIT_SERVOS_ACT 1 << 3
#define BIT_DISPLAY_ACT 1 << 4

int tempPool;
int tempCount;
int humPool;
int humCount;
int co2Pool;
int co2Count;

int16_t avgTemp;
uint16_t avgHum;
uint16_t avgCo2;

void dataHolder_create(SemaphoreHandle_t mutex);
void dataHolder_setBreakpoints(lora_driver_payload_t payload);
void addPPM(uint16_t ppm);
void addTemperture(uint16_t temperature);
void addHumidity(uint16_t humidity);
void resetAllCounterValues();
uint16_t getHumAvg();
uint16_t getCo2Avg();
int16_t getTempAvg();
void calculateAvg();

uint16_t getHumidityBreakpointLow();
uint16_t getHumidityBreakpointHigh();
int16_t getTemperatureBreakpointLow();
int16_t getTemperatureBreakpointHigh();
uint16_t getCo2BreakpointLow();
uint16_t getCo2BreakpointHigh();

#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <semphr.h>
#include <lora_driver.h>
#include <stdint.h>

#define INVALID_HUMIDITY_VALUE 0
#define INVALID_TEMPERATURE_VALUE -100
#define INVALID_CO2_VALUE 0
#define BIT_WINDOW_ACT 1 << 3

int TempPool;
int TempCount;
int HumPool;
int HumCount;
int PpmPool;
int PpmCount;
int AvgTemp;
int AvgHum;
int AvgPpm;

void dataHolder_create(SemaphoreHandle_t mutex);
void dataHolder_setBreakpoints(lora_driver_payload_t payload);

uint16_t getHumidityBreakpointLow();
uint16_t getHumidityBreakpointHigh();
int16_t getTemperatureBreakpointLow();
int16_t getTemperatureBreakpointHigh();
uint16_t getCo2BreakpointLow();
uint16_t getCo2BreakpointHigh();

#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <semphr.h>
#include <lora_driver.h>
#include <stdint.h>

#define CONFIG_INVALID_HUMIDITY_VALUE 2000
#define CONFIG_INVALID_TEMPERATURE_VALUE -1000
#define CONFIG_INVALID_CO2_VALUE 0


void config_create(SemaphoreHandle_t mutex);
void config_setThresholds(lora_driver_payload_t payload);
uint16_t config_getLowHumidityThreshold();
int16_t config_getLowTemperatureThreshold();
uint16_t config_getLowCO2Threshold();
uint16_t config_getHighHumidityThreshold();
int16_t config_getHighTemperatureThreshold();
uint16_t config_getHighCO2Threshold();

#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <semphr.h>
#include <lora_driver.h>
#include <stdint.h>

#define CONFIG_INVALID_HUMIDITY_VALUE 2000
#define CONFIG_INVALID_TEMPERATURE_VALUE -1000
#define CONFIG_INVALID_CO2_VALUE 0
#define CONFIG_INVALID_SOUND_VALUE 0

void configuration_create(SemaphoreHandle_t mutex);
void configuration_setThresholds(lora_driver_payload_t payload);
uint16_t configuration_getLowHumidityThreshold();
int16_t configuration_getLowTemperatureThreshold();
uint16_t configuration_getLowCO2Threshold();
uint16_t configuration_getHighHumidityThreshold();
int16_t configuration_getHighTemperatureThreshold();
uint16_t configuration_getHighCO2Threshold();
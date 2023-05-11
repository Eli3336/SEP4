#pragma once

#include <lora_driver.h>
#include <stdint.h>

lora_driver_payload_t uplinkMessageBuilder_buildUplinkMessage(uint8_t port);
void uplinkMessageBuilder_setHumidityData(uint16_t data);
void uplinkMessageBuilder_setTemperatureData(uint16_t data);
void uplinkMessageBuilder_setCO2Data(uint16_t data);
void uplinkMessageBuilder_setSystemErrorState();
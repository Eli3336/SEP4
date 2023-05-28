#include <VibeController.h>
#include <UplinkMessageBuilder.h>
#include <lora_driver.h>
#include <task.h>
#include <stdbool.h>
#include <DataHolder.h>
#include <stdio.h>


#define TASK_NAME "VibeController"
#define TASK_INTERVAL 300000UL //3 seconds
#define TASK_PRIORITY 4
#define PORT 2

static void _run(void* params);

static QueueHandle_t _senderQueue;
static EventGroupHandle_t _actEventGroup;

void VibeController_create(QueueHandle_t senderQueue, EventGroupHandle_t actEventGroup){
	_senderQueue = senderQueue;
	_actEventGroup = actEventGroup;
	xTaskCreate(_run, 
			    TASK_NAME, 
				configMINIMAL_STACK_SIZE, 
				NULL, 
				TASK_PRIORITY, 
				NULL
	);
	
	
}

void VibeController_initTask(void* params) {
	
	// Test if display is working, and show the start of the program
	display_7seg_displayHex("0000");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("9999");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("7777");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("1111");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("....");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("9999");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("7777");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("1111");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_displayHex("....");
	vTaskDelay(pdMS_TO_TICKS(100));
	display_7seg_powerDown();
	
}

void VibeController_runTask(void) {	
	
	// Calculate Averages before sending
	calculateAvg();
	
	// Take calculated Averages from DataHolder
	uint16_t tempHumidity = getHumAvg();
	int16_t tempTemperature = getTempAvg();
	uint16_t tempCo2 = getCo2Avg();
	
	uint8_t errorFlagToSend[] = {0, 0, 0, 0, 0, 0, 0, 0};
	
	// Check if we have Invalid Data
	if (tempHumidity == INVALID_HUMIDITY_VALUE)
	{
		errorFlagToSend[0] = 1;
	}
	if (tempTemperature == INVALID_TEMPERATURE_VALUE)
	{
		errorFlagToSend[1] = 1;
	}
	if (tempCo2 == INVALID_CO2_VALUE)
	{
		errorFlagToSend[2] = 1;
	}
	
	// Build the uplink message
	uplinkMessageBuilder_setHumidityData(&tempHumidity);
	uplinkMessageBuilder_setTemperatureData(&tempTemperature);
	uplinkMessageBuilder_setCO2Data(&tempCo2);
	uplinkMessageBuilder_setSystemErrorState(&errorFlagToSend);
	
	// Activate servos and display according to calculated averages
	xEventGroupSetBits(_actEventGroup, BIT_SERVOS_ACT | BIT_DISPLAY_ACT);
	
	// Sent build uplink message to SenderTask
	lora_driver_payload_t message = uplinkMessageBuilder_buildUplinkMessage(PORT);
	if (message.len > 0) {
		xQueueSendToBack(_senderQueue, &message, pdMS_TO_TICKS(10000));
	}
	
	// Reset Counters for calculating averages
	resetAllCounterValues();
	TickType_t lastWakeTime = xTaskGetTickCount();
	xTaskDelayUntil(&lastWakeTime, pdMS_TO_TICKS(TASK_INTERVAL));
}

static void _run(void* params) {
	VibeController_initTask(params);
	
	while (1) {
		VibeController_runTask();
	}
}
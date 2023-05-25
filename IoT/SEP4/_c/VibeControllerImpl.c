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
	vTaskDelay(50UL);
	printf("Controller is initialiazed");
	vTaskDelay(100UL);
	
}

void VibeController_runTask(void) {	
	
	calculateAvg();
	
	uint16_t tempHumidity = getHumAvg();
	int16_t tempTemperature = getTempAvg();
	uint16_t tempCo2 = getCo2Avg();
	
	if (tempHumidity == INVALID_HUMIDITY_VALUE)
	{
		uplinkMessageBuilder_setSystemErrorState();
	}
	if (tempTemperature == INVALID_TEMPERATURE_VALUE)
	{
		uplinkMessageBuilder_setSystemErrorState();
	}
	if (tempCo2 == INVALID_CO2_VALUE)
	{
		uplinkMessageBuilder_setSystemErrorState();
	}
	
	uplinkMessageBuilder_setHumidityData(tempHumidity);
	uplinkMessageBuilder_setTemperatureData(tempHumidity);
	uplinkMessageBuilder_setCO2Data(tempCo2);
			
	xEventGroupSetBits(_actEventGroup, BIT_WINDOW_ACT);
	
	lora_driver_payload_t message = uplinkMessageBuilder_buildUplinkMessage(PORT);
	if (message.len > 0) {
		xQueueSendToBack(_senderQueue, &message, pdMS_TO_TICKS(10000));
	}

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
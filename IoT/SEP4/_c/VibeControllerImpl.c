#include <VibeController.h>
#include <UplinkMessageBuilder.h>
#include <lora_driver.h>
#include <task.h>
#include <stdbool.h>
#include <DataHolder.h>
#include <stdio.h>


#define TASK_NAME "VibeController"
#define TASK_INTERVAL 300000UL //3 seconds
#define TASK_PRIORITY 6
#define PORT 2

static void _run(void* params);

static QueueHandle_t _senderQueue;


void VibeController_create(QueueHandle_t senderQueue){
	_senderQueue = senderQueue;
	
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
	
	if (TempCount == 0)
	{
		uplinkMessageBuilder_setTemperatureData(INVALID_TEMPERATURE_VALUE);
		uplinkMessageBuilder_setSystemErrorState();
		
	}
	if (HumCount == 0)
	{
		uplinkMessageBuilder_setHumidityData(INVALID_HUMIDITY_VALUE);
		uplinkMessageBuilder_setSystemErrorState();
	}
	if (PpmCount == 0)
	{
		uplinkMessageBuilder_setCO2Data(INVALID_CO2_VALUE);
		uplinkMessageBuilder_setSystemErrorState();
	}
	else{
		
		
	uplinkMessageBuilder_setHumidityData(HumPool/HumCount);
	uplinkMessageBuilder_setTemperatureData(TempPool/TempCount);
	uplinkMessageBuilder_setCO2Data(PpmPool/PpmCount);
	
	}
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
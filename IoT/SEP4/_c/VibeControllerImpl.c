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
			AvgHum =HumPool/HumCount;
			AvgTemp=TempPool/TempCount;
			AvgPpm=PpmPool/TempCount;
			uplinkMessageBuilder_setHumidityData(AvgHum);
			uplinkMessageBuilder_setTemperatureData(AvgTemp);
			uplinkMessageBuilder_setCO2Data(AvgPpm);
			
			xEventGroupSetBits(_actEventGroup,BIT_WINDOW_ACT);
	
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
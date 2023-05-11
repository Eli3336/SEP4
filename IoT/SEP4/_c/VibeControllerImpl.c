#include <VibeController.h>
#include <CO2Task.h>
#include <HumiTempTask.h>
#include <UplinkMessageBuilder.h>
#include <lora_driver.h>
#include <task.h>
#include <stdbool.h>
#include <Config.h>

#define TASK_NAME "VibeController"
#define TASK_INTERVAL 300000UL //5´Minutes
#define TASK_PRIORITY configMAX_PRIORITIES - 1
#define PORT 1

static void _run(void* params);

static QueueHandle_t _senderQueue;
static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static QueueHandle_t _co2Queue;
static EventGroupHandle_t _actEventGroup;
static EventGroupHandle_t _doneEventGroup;
static bool _error = false;

void VibeController_create(QueueHandle_t senderQueue, 
				      QueueHandle_t humidityQueue, 
					  QueueHandle_t temperatureQueue, 
					  QueueHandle_t co2Queue,
					  EventGroupHandle_t actEventGroup, 
					  EventGroupHandle_t doneEventGroup){
	_senderQueue = senderQueue;
	_humidityQueue = humidityQueue;
	_temperatureQueue = temperatureQueue;
	_co2Queue = co2Queue;
	_actEventGroup = actEventGroup;
	_doneEventGroup = doneEventGroup;
	
	xTaskCreate(_run, 
			    TASK_NAME, 
				configMINIMAL_STACK_SIZE, 
				NULL, 
				TASK_PRIORITY, 
				NULL
	);
}

void VibeController_initTask(void* params) {
	
}

void VibeController_runTask(void) {	
	EventBits_t uxBits = xEventGroupSetBits(_actEventGroup, BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT);
	
	if ((uxBits & (BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT)) == (BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT))
	{
		/* If both bits remain set, then there is a big chance that the measurements are incorrect.
		Set all the validation bits to "Invalid"*/
		_error = true;
	}

	EventBits_t waitBits = xEventGroupWaitBits(_doneEventGroup,
						BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE,
						pdTRUE, 
						pdTRUE, 
						pdMS_TO_TICKS(TASK_INTERVAL)
	);
	
	if ((waitBits & (BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE)) != (BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE)) {
		/*
			The function has executed due to timeout and the measurements are invalid
			Set the errorState flag to true
		*/
		_error = true;
	}

	uint16_t humidity;
	int16_t temperature;
	uint16_t ppm;
	if (xQueueReceive(_humidityQueue, &humidity, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		humidity = CONFIG_INVALID_HUMIDITY_VALUE;
	};
	if (xQueueReceive(_temperatureQueue, &temperature, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		temperature = CONFIG_INVALID_TEMPERATURE_VALUE;
	}
	if (xQueueReceive(_co2Queue, &ppm, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		ppm = CONFIG_INVALID_CO2_VALUE;
	}
		
	uplinkMessageBuilder_setHumidityData(humidity);
	uplinkMessageBuilder_setTemperatureData(temperature);
	uplinkMessageBuilder_setCO2Data(ppm);
		
	if (_error == true)
	{
		uplinkMessageBuilder_setSystemErrorState();
	}

	lora_driver_payload_t message = uplinkMessageBuilder_buildUplinkMessage(PORT);
	if (message.len > 0) {
		xQueueSendToBack(_senderQueue, &message, pdMS_TO_TICKS(10000));
	}
	
	/*
	if (_error == false)
	{
		xQueueSendToBack(_servoQueue, &humidity, pdMS_TO_TICKS(10000));
		xQueueSendToBack(_servoQueue, &temperature, pdMS_TO_TICKS(10000));
		xQueueSendToBack(_servoQueue, &ppm, pdMS_TO_TICKS(10000));
		xQueueSendToBack(_servoQueue, &sound, pdMS_TO_TICKS(10000));
	}
	*/

	_error = false;
	TickType_t lastWakeTime = xTaskGetTickCount();
	xTaskDelayUntil(&lastWakeTime, pdMS_TO_TICKS(TASK_INTERVAL));
}

static void _run(void* params) {
	VibeController_initTask(params);
	
	while (1) {
		VibeController_runTask();
	}
}
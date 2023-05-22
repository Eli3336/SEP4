#include <VibeController.h>
#include <CO2Task.h>
#include <HumiTempTask.h>
#include <UplinkMessageBuilder.h>
#include <lora_driver.h>
#include <task.h>
#include <stdbool.h>
#include <DataHolder.h>

#define TASK_NAME "VibeController"
#define TASK_INTERVAL 300000UL // 3 seconds
#define TASK_PRIORITY 6
#define PORT 2

static void _run(void *params);

static QueueHandle_t _senderQueue;
static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static QueueHandle_t _co2Queue;
static EventGroupHandle_t _doEventGroup;
static EventGroupHandle_t _doneEventGroup;
static bool _error = false;

void VibeController_create(QueueHandle_t senderQueue,
						   QueueHandle_t humidityQueue,
						   QueueHandle_t temperatureQueue,
						   QueueHandle_t co2Queue,
						   EventGroupHandle_t actEventGroup,
						   EventGroupHandle_t doneEventGroup)
{
	_senderQueue = senderQueue;
	_humidityQueue = humidityQueue;
	_temperatureQueue = temperatureQueue;
	_co2Queue = co2Queue;
	_doEventGroup = actEventGroup;
	_doneEventGroup = doneEventGroup;

	xTaskCreate(_run,
				TASK_NAME,
				configMINIMAL_STACK_SIZE,
				NULL,
				TASK_PRIORITY,
				NULL);
}

void VibeController_initTask(void *params)
{
	vTaskDelay(50UL);
	puts("Controller is initialiazed");
	vTaskDelay(100UL);
}

void VibeController_runTask(void)
{

	xEventGroupSetBits(_doEventGroup, BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT | BIT_CO2_ACT);

	uint16_t humidity;
	int16_t temperature;
	uint16_t ppm;
	if (xQueueReceive(_humidityQueue, &humidity, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		humidity = INVALID_HUMIDITY_VALUE;
	};
	if (xQueueReceive(_temperatureQueue, &temperature, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		temperature = INVALID_TEMPERATURE_VALUE;
	}
	if (xQueueReceive(_co2Queue, &ppm, pdMS_TO_TICKS(10000)) != pdTRUE)
	{
		ppm = INVALID_CO2_VALUE;
	}

	uplinkMessageBuilder_setHumidityData(humidity);
	uplinkMessageBuilder_setTemperatureData(temperature);
	uplinkMessageBuilder_setCO2Data(ppm);

	if (_error == true)
	{
		puts("Error detected");
		uplinkMessageBuilder_setSystemErrorState();
	}

	lora_driver_payload_t message = uplinkMessageBuilder_buildUplinkMessage(PORT);
	if (message.len > 0)
	{
		xQueueSendToBack(_senderQueue, &message, pdMS_TO_TICKS(10000));
	}

	_error = false;
	TickType_t lastWakeTime = xTaskGetTickCount();
	xTaskDelayUntil(&lastWakeTime, pdMS_TO_TICKS(TASK_INTERVAL));
}

static void _run(void *params)
{
	VibeController_initTask(params);

	while (1)
	{
		VibeController_runTask();
	}
}
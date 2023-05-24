#include <HumiTempTask.h>
#include <DataHolder.h>
#include <Hal.h>

#define TASK_NAME "HumiTempTask"
#define TASK_PRIORITY 3

static void _run(void *params);

static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static EventGroupHandle_t _doEventGroup;
static EventGroupHandle_t _doneEventGroup;
static uint16_t _latestHumidity;
static int16_t _latestTemperature;

void humiTempTask_create(QueueHandle_t humidityQueue,
						 QueueHandle_t temperatureQueue,
						 EventGroupHandle_t doEventGroup,
						 EventGroupHandle_t doneEventGroup)
{
	_humidityQueue = humidityQueue;
	_temperatureQueue = temperatureQueue;
	_doEventGroup = doEventGroup;
	_doneEventGroup = doneEventGroup;

	xTaskCreate(_run,
				TASK_NAME,
				configMINIMAL_STACK_SIZE,
				NULL,
				TASK_PRIORITY,
				NULL);
}

void humiTempTask_runTask()
{
	xEventGroupWaitBits(_doEventGroup,
						BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT,
						pdTRUE,
						pdTRUE,
						portMAX_DELAY);

	validateHumTemp();

	xQueueSendToBack(_humidityQueue, &_latestHumidity, portMAX_DELAY);
	xQueueSendToBack(_temperatureQueue, &_latestTemperature, portMAX_DELAY);
}

static void _run(void *params)
{
	humiTempTask_initTask(params);

	while (1)
	{
		humiTempTask_runTask();
	}
}

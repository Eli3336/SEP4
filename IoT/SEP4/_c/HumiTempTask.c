#include <HumiTempTask.h>
#include <stdint.h>
#include <task.h>
#include <hih8120.h>
#include <DataHolder.h>

#define TASK_NAME "HumiTempTask"
#define TASK_PRIORITY 1

static void _run(void* params);

static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static EventGroupHandle_t _doEventGroup;
static uint16_t _latestHumidity;
static int16_t _latestTemperature;

void humiTempTask_create(QueueHandle_t humidityQueue, 
									QueueHandle_t temperatureQueue, 
									EventGroupHandle_t doEventGroup) {
	_humidityQueue = humidityQueue;
	_temperatureQueue = temperatureQueue;
	_doEventGroup = doEventGroup;
	
	xTaskCreate(_run, 
				TASK_NAME, 
				configMINIMAL_STACK_SIZE, 
				NULL, 
				TASK_PRIORITY, 
				NULL
	);
}

void humiTempTask_initTask(void* params) {
	hih8120_wakeup();
	vTaskDelay(pdMS_TO_TICKS(100));
}

void humiTempTask_runTask() {
	 xEventGroupWaitBits(_doEventGroup, 
					    BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT,
						pdTRUE,	
						pdTRUE, 
						portMAX_DELAY
	);
	
	if (hih8120_wakeup() == HIH8120_OK) {
		vTaskDelay(pdMS_TO_TICKS(100));
		
		if (hih8120_measure() == HIH8120_OK) {
			vTaskDelay(pdMS_TO_TICKS(50));
			_latestHumidity = hih8120_getHumidityPercent_x10();
			_latestTemperature = hih8120_getTemperature_x10();
		} else {
			_latestHumidity = INVALID_HUMIDITY_VALUE;
			_latestTemperature = INVALID_TEMPERATURE_VALUE;			
		}
	} else {
		_latestHumidity = INVALID_HUMIDITY_VALUE;
		_latestTemperature = INVALID_TEMPERATURE_VALUE;
	}
	
	xQueueSendToBack(_humidityQueue, &_latestHumidity, portMAX_DELAY);
	xQueueSendToBack(_temperatureQueue, &_latestTemperature, portMAX_DELAY);
	
}

static void _run(void* params) {
	humiTempTask_initTask(params);
	
	while (1) {
		humiTempTask_runTask();
	}
}


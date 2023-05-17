#include <CO2Task.h>
#include <task.h>
#include <stdint.h>
#include <mh_z19.h>
#include <Config.h>
#include <HumiTempTask.h>
#include <stdio.h>

#define TASK_NAME "CO2Task"
#define TASK_PRIORITY configMAX_PRIORITIES - 3

static void _co2CallBack(uint16_t ppm);
static void _run(void* params);

static QueueHandle_t _co2Queue;
static EventGroupHandle_t _doEventGroup;
static EventGroupHandle_t _doneEventGroup;
static uint16_t ppm;

void co2Task_create(QueueHandle_t co2Queue, EventGroupHandle_t doEventGroup, EventGroupHandle_t doneEventGroup) {
	_co2Queue = co2Queue;
	_doEventGroup = doEventGroup;
	_doneEventGroup = doneEventGroup;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL);
}

void co2Task_initTask(void* params) {
	mh_z19_injectCallBack(_co2CallBack);
}

void co2Task_runTask() {
/*	xEventGroupWaitBits(_doneEventGroup,
	BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE,
	pdFALSE,
	pdTRUE,
	portMAX_DELAY
	);
	*/
	if ((mh_z19_takeMeassuring()) != MHZ19_OK) {
		ppm = CONFIG_INVALID_CO2_VALUE;
		_co2CallBack(ppm);
	}
		vTaskDelay(pdMS_TO_TICKS(30000UL));
	
	xEventGroupSetBits(_doneEventGroup, BIT_CO2_DONE);
}

static void _co2CallBack(uint16_t ppm){
	
	xQueueSendToBack(_co2Queue, &ppm, portMAX_DELAY);
}

static void _run(void* params) {
	
	co2Task_initTask(params);
	
	while (1) {
		co2Task_runTask();
	}
}
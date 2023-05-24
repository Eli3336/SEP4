#include <CO2Task.h>
#include <DataHolder.h>
#include <HumiTempTask.h>
#include <Hal.h>

#define TASK_NAME "CO2Task"
#define TASK_PRIORITY 3

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



void co2Task_runTask() {
	xEventGroupWaitBits(_doEventGroup,
	BIT_CO2_ACT,
	pdTRUE,
	pdTRUE,
	portMAX_DELAY
	);
	
	if ((mh_z19_takeMeassuring()) != MHZ19_OK) {
		ppm = INVALID_CO2_VALUE;
		_co2CallBack(ppm);
	}
}



static void _run(void* params) {
	
	co2Task_initTask(params);
	
	while (1) {
		co2Task_runTask();
	}
}
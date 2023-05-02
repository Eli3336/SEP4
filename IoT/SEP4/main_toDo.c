#include <stdio.h>
#include <avr/io.h>
#include <stdio_driver.h>
#include <serial.h>
#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>
#include <mh_z19.h>
#include <lora_driver.h>
#include <semphr.h>
#include <message_buffer.h>

//#include <VibeHealth.h>
#include <CO2Task.h>
#include <Configuration.h>
#include <HumiTemp.h>

// humidity + temperature
static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static QueueHandle_t _co2Queue;
// servo
// sender queues
// breakpoint queues?
// also receiver task

static EventGroupHandle_t _actEventGroup;
static EventGroupHandle_t _doneEventGroup;

static SemaphoreHandle_t _mutex;

static MessageBufferHandle_t _messageBuffer;

static void _createQueues(void) {
	_co2Queue = xQueueCreate(10, sizeof(uint16_t));
	_humidityQueue = xQueueCreate(10, sizeof(uint16_t));
	_temperatureQueue = xQueueCreate(10, sizeof(int16_t));
	_messageBuffer = xMessageBufferCreate(sizeof(lora_driver_payload_t)*5);
}

static void _createEventGroups(void) {
	_actEventGroup = xEventGroupCreate();
	_doneEventGroup = xEventGroupCreate();
}

static void _createMutexes(void){
	_mutex = xSemaphoreCreateMutex();
}

static void _initDrivers(void) {
	puts("Initializing drivers...");
	mh_z19_initialise(ser_USART3);
	hih8120_initialise();
	lora_driver_initialise(ser_USART1, _messageBuffer);
}

static void _createTasks(void) {
	co2Task_create(_co2Queue, _actEventGroup, _doneEventGroup);
	humidityTemperatureTask_create(_humidityQueue, _temperatureQueue, _actEventGroup, _doneEventGroup);
}

int main(void) {
	stdio_initialise(ser_USART0);
	
	_createQueues();
	_initDrivers();
	_createEventGroups();
	_createTasks();
	_createMutexes();
	configuration_create(_mutex);
	
	puts("Starting...");
	vTaskStartScheduler();
}
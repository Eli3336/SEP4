#include <stdio.h>
#include <avr/io.h>
#include <ATMEGA_FreeRTOS.h>
#include <task.h>
#include <semphr.h>
#include <stdio_driver.h>
#include <serial.h>

// Needed for LoRaWAN
#include <lora_driver.h>
#include <status_leds.h>

//added sensors
#include <queue.h>
#include <event_groups.h>
#include <mh_z19.h>
#include <message_buffer.h>

#include <VibeController.h>
#include <CO2Task.h>
#include <Config.h>
#include <HumiTempTask.h>


// Queues
static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static QueueHandle_t _co2Queue;
static QueueHandle_t _senderQueue;
// also receiver task

static EventGroupHandle_t _actEventGroup = NULL;
static EventGroupHandle_t _doneEventGroup = NULL;



static SemaphoreHandle_t _mutex;

static MessageBufferHandle_t _messageBuffer;

static void _createQueues(void) {
	_co2Queue = xQueueCreate(10, sizeof(uint16_t));
	_humidityQueue = xQueueCreate(10, sizeof(uint16_t));
	_temperatureQueue = xQueueCreate(10, sizeof(int16_t));
	_senderQueue= xQueueCreate(10,sizeof(lora_driver_payload_t));
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
	 lora_driver_initialise(ser_USART1, _messageBuffer);
	puts("Initializing drivers...");
	// + servo drivers

	mh_z19_initialise(ser_USART3);
	hih8120_initialise();
}

static void _createTasks(void) {
	senderTask_create(_senderQueue);
	puts("Created Sender task");
	VibeController_create(_senderQueue, _humidityQueue, _temperatureQueue, _co2Queue, _actEventGroup, _doneEventGroup);
	co2Task_create(_co2Queue, _actEventGroup, _doneEventGroup);
	humiTempTask_create(_humidityQueue, _temperatureQueue, _actEventGroup, _doneEventGroup);
}



int main(void)
{

	stdio_initialise(ser_USART0);
	_initDrivers();
	puts("Start initiated");
	
	_createQueues();
	
	_createEventGroups();
	_createTasks();
	_createMutexes();
	config_create(_mutex);

	puts("Launching IoT device...");
	vTaskStartScheduler();
	while (1)
	{
		
	}
}

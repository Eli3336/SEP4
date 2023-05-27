#include <stdio.h>
#include <ATMEGA_FreeRTOS.h>
#include <task.h>
#include <semphr.h>
#include <stdio_driver.h>
#include <serial.h>
#include <queue.h>
#include <event_groups.h>
#include <message_buffer.h>

// Needed for LoRaWAN
#include <lora_driver.h>
#include <status_leds.h>

//added sensors
#include <mh_z19.h>
#include <hih8120.h>
#include <display_7seg.h>

#include <VibeController.h>
#include <CO2Task.h>
#include <DataHolder.h>
#include <HumiTempTask.h>
#include <ReceiverTask.h>
#include <Counter.h>
#include <ServoTask.h>
#include <SenderTask.h>
#include <DisplayTask.h>


// Queues
static QueueHandle_t _humidityQueue;
static QueueHandle_t _temperatureQueue;
static QueueHandle_t _co2Queue;
static QueueHandle_t _senderQueue;

static EventGroupHandle_t _actEventGroup = NULL;

static SemaphoreHandle_t _mutex;
SemaphoreHandle_t mutexAvgValues;
MessageBufferHandle_t messageBuffer;

static void _createQueues(void) {
	_co2Queue = xQueueCreate(10, sizeof(uint16_t));
	_humidityQueue = xQueueCreate(10, sizeof(uint16_t));
	_temperatureQueue = xQueueCreate(10, sizeof(int16_t));
	_senderQueue= xQueueCreate(10,sizeof(lora_driver_payload_t));
}

static void _createEventGroups(void) {
	_actEventGroup = xEventGroupCreate();
}
static void _createMessageBuffer(void){
	messageBuffer = xMessageBufferCreate(sizeof(lora_driver_payload_t)*2);
}

static void _createMutexes(void){
	_mutex = xSemaphoreCreateMutex();
	mutexAvgValues = xSemaphoreCreateMutex();
}

static void _initDrivers(void) {
	lora_driver_initialise(ser_USART1, messageBuffer);
	mh_z19_initialise(ser_USART3);
	hih8120_initialise();
	rc_servo_initialise();
	display_7seg_initialise(NULL);
	display_7seg_powerUp();
}

static void _createTasks(void) {
	senderTask_create(_senderQueue);
	VibeController_create(_senderQueue,_actEventGroup);
	counter_create( _humidityQueue, _temperatureQueue, _co2Queue, _actEventGroup);
	co2Task_create(_co2Queue, _actEventGroup);
	humiTempTask_create(_humidityQueue, _temperatureQueue, _actEventGroup);
	receiverTask_create();
	servoTask_create(_actEventGroup);
	displayTask_create(_actEventGroup);
}



int main(void)
{
	
	stdio_initialise(ser_USART0);	
	_createMessageBuffer();
	_initDrivers();
	_createQueues();
	_createEventGroups();
	_createMutexes();
	_createTasks();
	dataHolder_create(_mutex);
	puts("Launching IoT device...");
	vTaskStartScheduler();
  
	while (1)
	{
		
	}
}

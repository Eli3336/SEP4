/*
* main.c
* Author : IHA
*
* Edited example main file
*/

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

// define two Tasks
void task1( void *pvParameters );

// define semaphore handle
SemaphoreHandle_t xTestSemaphore;

// Prototype for LoRaWAN handler
void lora_handler_initialise(UBaseType_t lora_handler_task_priority);

//added sensors
#include <queue.h>
#include <event_groups.h>
#include <mh_z19.h>
#include <message_buffer.h>

//#include <VibeHealth.h>
#include <CO2Task.h>
#include <Config.h>
#include <HumiTempTask.h>

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
	humiTempTask_create(_humidityQueue, _temperatureQueue, _actEventGroup, _doneEventGroup);
}

//added sensors end

/*-----------------------------------------------------------*/
void create_tasks_and_semaphores(void)
{
	// Semaphores are useful to stop a Task proceeding, where it should be paused to wait,
	// because it is sharing a resource, such as the Serial port.
	// Semaphores should only be used whilst the scheduler is running, but we can set it up here.
	if ( xTestSemaphore == NULL )  // Check to confirm that the Semaphore has not already been created.
	{
		xTestSemaphore = xSemaphoreCreateMutex();  // Create a mutex semaphore.
		if ( ( xTestSemaphore ) != NULL )
		{
			xSemaphoreGive( ( xTestSemaphore ) );  // Make the mutex available for use, by initially "Giving" the Semaphore.
		}
	}


}


/*-----------------------------------------------------------*/


/*-----------------------------------------------------------*/
void initialiseSystem()
{
	// Set output ports for leds used in the example
	DDRA |= _BV(DDA0) | _BV(DDA7);

	// Make it possible to use stdio on COM port 0 (USB) on Arduino board - Setting 57600,8,N,1
	stdio_initialise(ser_USART0);
	// Let's create some tasks
	create_tasks_and_semaphores();

	// vvvvvvvvvvvvvvvvv BELOW IS LoRaWAN initialisation vvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
	// Status Leds driver
	status_leds_initialise(5); // Priority 5 for internal task
	// Initialise the LoRaWAN driver without down-link buffer
	lora_driver_initialise(1, NULL);
	// Create LoRaWAN task and start it up with priority 3
	lora_handler_initialise(3);
}

/*-----------------------------------------------------------*/
int main(void)
{
	initialiseSystem(); // Must be done as the very first thing!!
	printf("Start initiated\n");
	stdio_initialise(ser_USART0);

	_createQueues();
	_initDrivers();
	_createEventGroups();
	_createTasks();
	_createMutexes();
	config_create(_mutex);

	puts("Launching IoT device...");
	vTaskStartScheduler();
	/* Replace with your application code */
	while (1)
	{
	}
}

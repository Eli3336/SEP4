#include "SenderTask.h"
#include <ReceiverTask.h>
#include <Hal.h>

#define TASK_NAME "SenderTask"
#define TASK_PRIORITY 4
#define LORA_appEUI "6BE1FDCE7E214CF9"
#define LORA_appKEY "EECCD39BD2AB6C6BD107A08E0DBE9DB9"

static void _run(void* params);
static void _connectToLoRaWAN();

static EventGroupHandle_t _receiveEventGroup;

static QueueHandle_t _senderQueue;

void senderTask_create(QueueHandle_t senderQueue, EventGroupHandle_t receiveEventGroup) {
	_senderQueue = senderQueue;
	_receiveEventGroup = receiveEventGroup;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
}

void senderTask_runTask() {
	lora_driver_payload_t uplinkPayload;
	xQueueReceive(_senderQueue, &uplinkPayload, portMAX_DELAY);
	int i;
		
	printf("Payload to send: \n");
	for(i=0;i <uplinkPayload.len;i++)
	{
		printf("%02X ",uplinkPayload.bytes[i]);
			
	}
	printf("\n");
	lora_driver_sendUploadMessage(false, &uplinkPayload);
	vTaskDelay(pdMS_TO_TICKS(100));
	xEventGroupSetBits(_receiveEventGroup, BIT_RECEIVER_ACT);
}

static void _run(void* params) {
	senderTask_initTask(params);
	
	while (1) {
	senderTask_runTask();
	}
}

void _connectToLoRaWAN();

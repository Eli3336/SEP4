#include "ReceiverTask.h"
#include <task.h>
#include <message_buffer.h>
#include <stdint.h>
#include <stdio.h>
#include <stddef.h>
#include <lora_driver.h>
#include <DataHolder.h>
#include <event_groups.h>

#define TASK_NAME "ReceiverTask"
#define TASK_PRIORITY 5
#define EXPECTED_PAYLOAD_LENGTH 15

static EventGroupHandle_t _receiveEventGroup;
static MessageBufferHandle_t _receiverBuffer;

static void _run(void* params);

void receiverTask_create(MessageBufferHandle_t receiverBuffer, EventGroupHandle_t receiveEventGroup) {
	_receiverBuffer = receiverBuffer;
	_receiveEventGroup = receiveEventGroup;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
	puts("receiver task created");
}

void receiverTask_initTask(void* params) {
}

void receiverTask_runTask(void) {
	
	xEventGroupWaitBits(_receiveEventGroup,
	BIT_RECEIVER_ACT,
	pdTRUE,
	pdFALSE,
	portMAX_DELAY
	);
	printf("ReceiverTask run\n");
	
	lora_driver_payload_t payload;
	xMessageBufferReceive(_receiverBuffer,
	&payload,
	sizeof(lora_driver_payload_t),
	portMAX_DELAY
	);

	if (payload.len == EXPECTED_PAYLOAD_LENGTH) {
		dataHolder_setBreakpoints(payload);
	}
	
	printf("Payload received: \n");
	for(int i=0; i <payload.len; i++)
	{
		printf("%02X ", payload.bytes[i]);
	}
	printf("\n");
}

static void _run(void* params) {
	printf("Receiver Task is running\n");
	receiverTask_initTask(params);
	
	while (1) {
		receiverTask_runTask();
	}
}
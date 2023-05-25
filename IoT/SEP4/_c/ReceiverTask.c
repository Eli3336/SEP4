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
#define TASK_PRIORITY 1
#define EXPECTED_PAYLOAD_LENGTH 15

extern MessageBufferHandle_t messageBuffer;

static void _run(void* params);

void receiverTask_create(void) {
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
}

void receiverTask_initTask(void* params) {
}

void receiverTask_runTask(void) {
	
	lora_driver_payload_t payload;
	xMessageBufferReceive(messageBuffer,
	&payload,
	sizeof(lora_driver_payload_t),
	portMAX_DELAY
	);
	
	payload.portNo = 2;
	printf("DOWN LINK: from port: %d with %d bytes received!", payload.portNo, payload.len);

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
	receiverTask_initTask(params);
	
	while (1) {
		receiverTask_runTask();
	}
}
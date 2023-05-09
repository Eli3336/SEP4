#include "ReceiverTask.h"
#include <task.h>
#include <message_buffer.h>
#include <stdint.h>
#include <stdio.h>
#include <stddef.h>
#include <lora_driver.h>
#include <task.h>
#include <Config.h>

#define TASK_NAME "ReceiverTask"
#define TASK_PRIORITY configMAX_PRIORITIES - 2
#define EXPECTED_PAYLOAD_LENGTH 15

static void _run(void* params);

static MessageBufferHandle_t _receiverBuffer;

void receiverTask_create(MessageBufferHandle_t receiverBuffer) {
	_receiverBuffer = receiverBuffer;
	
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
	xMessageBufferReceive(_receiverBuffer,
	&payload,
	sizeof(lora_driver_payload_t),
	portMAX_DELAY
	);

	if (payload.len == EXPECTED_PAYLOAD_LENGTH) {
		configuration_setThresholds(payload);
	}
}

static void _run(void* params) {
	receiverTask_initTask(params);
	
	while (1) {
		receiverTask_runTask();
	}
}
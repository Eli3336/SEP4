#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <message_buffer.h>
#include <event_groups.h>

#define BIT_RECEIVER_ACT 1 << 3

void receiverTask_create(MessageBufferHandle_t receiverBuffer, EventGroupHandle_t receiveEventGroup);
void receiverTask_initTask(void* params);
void receiverTask_runTask(void);
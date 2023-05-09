#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <message_buffer.h>

void receiverTask_create(MessageBufferHandle_t receiverBuffer);
void receiverTask_initTask(void* params);
void receiverTask_runTask(void);
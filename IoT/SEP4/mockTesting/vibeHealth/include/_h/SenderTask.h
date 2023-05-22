#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>


void senderTask_create(QueueHandle_t senderQueue, EventGroupHandle_t doEventGroup);
void senderTask_initTask(void* params);
void senderTask_runTask(void);
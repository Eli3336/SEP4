
/*
 * ServoTask.h
 *
 * Created: 16/05/2023 14:05:45
 *  Author: ancab
 */ 
#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>

void servoTask_create(EventGroupHandle_t actEventGroup);
void servoTask_initTask(void* params);
void servoTask_runTask(void);

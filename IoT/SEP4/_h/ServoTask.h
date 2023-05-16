
/*
 * ServoTask.h
 *
 * Created: 16/05/2023 14:05:45
 *  Author: ancab
 */ 
#pragma once

#include <ATMEGA_FreeRTOS.h>
#include <queue.h>

void servoTask_create(QueueHandle_t servoQueue);
void servoTask_initTask(void* params);
void servoTask_runTask(void);

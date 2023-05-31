#pragma once
#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>
#include <rc_servo.h>
#define BIT_SERVOS_ACT 1 << 3
void servoTask_create(EventGroupHandle_t actEventGroup);
void servoTask_initTask(void* params);
void servoTask_runTask(void);

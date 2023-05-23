
/*
 * ServoTask.c
 *
 * Created: 16/05/2023 14:06:04
 *  Author: ancab
 */ 
#include <ServoTask.h>
#include <DataHolder.h>
#include <rc_servo.h>
#include <stdio.h>
#include <stdint.h>
#include <task.h>
#include <event_groups.h>

#define TASK_NAME "ServoTask"
#define TASK_PRIORITY configMAX_PRIORITIES - 2
#define SERVO_PORT 1
#define SERVO_POS_OPEN 100
#define SERVO_POS_CLOSED -100
#define SERVO_POS_MIDDLE 0

static void _run(void* params);

static EventGroupHandle_t _actEventGroup;

void servoTask_create(EventGroupHandle_t actEventGroup) {
	_actEventGroup = actEventGroup;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
}

void servoTask_initTask(void* params) {
	rc_servo_initialise();
	// Default the starting window position to be between open and closed.
	rc_servo_setPosition(SERVO_PORT, SERVO_POS_MIDDLE);
}

void servoTask_runTask() {

	

	xEventGroupWaitBits(_actEventGroup,
	BIT_WINDOW_ACT,
	pdTRUE,
	pdTRUE,
	portMAX_DELAY
	);
	
	//rc_servo_setPosition(SERVO_PORT,SERVO_POS_CLOSED);
	
	
	//uint16_t lowPpm = getCo2BreakpointLow();
	//uint16_t HighPpm = getCo2BreakpointHigh();
	//tests
	uint16_t LowPpm = 800;
	uint16_t HighPpm= 1000;
	
	
	
	if (LowPpm != INVALID_CO2_VALUE && AvgPpm < LowPpm) {
		rc_servo_setPosition(SERVO_PORT, SERVO_POS_CLOSED);
		} else if (HighPpm != INVALID_CO2_VALUE && AvgPpm > HighPpm) {
		rc_servo_setPosition(SERVO_PORT, SERVO_POS_OPEN);
		} else {
		rc_servo_setPosition(SERVO_PORT, SERVO_POS_MIDDLE);
	}
}

static void _run(void* params) {
	servoTask_initTask(params);
	
	while (1) {
		servoTask_runTask();
	}
}

/*
 * ServoTask.c
 *
 * Created: 16/05/2023 14:06:04
 *  Author: ancab
 */ 
#include <ServoTask.h>
#include <Config.h>
#include <rc_servo.h>
#include <Hal.h>

#define TASK_NAME "ServoTask"
#define TASK_PRIORITY configMAX_PRIORITIES - 2
#define SERVO_PORT 1
#define SERVO_POS_OPEN 100
#define SERVO_POS_CLOSED -100
#define SERVO_POS_MIDDLE 0

static void _run(void* params);

static QueueHandle_t _servoQueue;

void servoTask_create(QueueHandle_t servoQueue) {
	_servoQueue = servoQueue;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
}



void servoTask_runTask() {
	uint16_t humidity;
	int16_t temperature;
	uint16_t co2;
	uint16_t sound;
	xQueueReceive(_servoQueue, &humidity, portMAX_DELAY);
	xQueueReceive(_servoQueue, &temperature, portMAX_DELAY);
	xQueueReceive(_servoQueue, &co2, portMAX_DELAY);
	
	// Delay introduced such that the thresholds are updated before reading them.
	vTaskDelay(pdMS_TO_TICKS(5000));
	
	int16_t lowThreshold = config_getLowTemperatureThreshold();
	int16_t highThreshold = config_getHighTemperatureThreshold();
	
	// Only open or close the window if the stored thresholds are not set to
	// the default temperature threshold values - the invalid temperature value.
	if (lowThreshold != CONFIG_INVALID_TEMPERATURE_VALUE && temperature < lowThreshold) {
		rc_servo_setPosition(SERVO_PORT, SERVO_POS_CLOSED);
		} else if (highThreshold != CONFIG_INVALID_TEMPERATURE_VALUE && temperature > highThreshold) {
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
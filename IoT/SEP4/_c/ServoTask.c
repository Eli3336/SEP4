#include <ServoTask.h>
#include <DataHolder.h>
#include <rc_servo.h>
#include <stdio.h>
#include <stdint.h>
#include <task.h>
#include <event_groups.h>

#define TASK_NAME "ServoTask"
#define TASK_PRIORITY 5
#define WINDOW_SERVO_PORT 1
#define CONDITIONER_SERVO_PORT 0

#define WINDOW_POS_OPEN 100
#define WINDOW_POS_CLOSED -100
#define WINDOW_POS_ON_DRY 30			// Slightly open window for regulating humidity levels
static bool windowOpen = false;			// true -> window is open | false -> window is closed

#define CONDITIONER_POS_ON 100
#define CONDITIONER_POS_OFF -100

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
	
	// Default the starting window position to be between open and closed.
	rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_CLOSED);
	rc_servo_setPosition(CONDITIONER_SERVO_PORT, CONDITIONER_POS_OFF);
}

void servoTask_runTask() {
	xEventGroupWaitBits(_actEventGroup,
	BIT_SERVOS_ACT,
	pdTRUE,
	pdTRUE,
	portMAX_DELAY
	);
	
	uint16_t tempHumidityAvg = getHumAvg();
	int16_t tempTemperatureAvg = getTempAvg();
	uint16_t tempCo2Avg = getCo2Avg();
	
	uint16_t tempHumidityBreakpointL = getHumidityBreakpointLow();
	uint16_t tempHumidityBreakpointH = getHumidityBreakpointHigh();
	int16_t tempTemperatureBreakpointL = getTemperatureBreakpointLow();
	int16_t tempTemperatureBreakpointH = getTemperatureBreakpointHigh();
	uint16_t tempCo2BreakpointL = getCo2BreakpointLow();
	uint16_t tempCo2BreakpointH = getHumidityBreakpointHigh();
	
	tempCo2BreakpointL = 400;
	tempCo2BreakpointH = 600;
	
	tempTemperatureBreakpointH = 190;
	tempTemperatureBreakpointL = 160;
	
	
	
	// Check if the window needs to be open.
	// If Co2 levels are above required values -> open window; Ignore the Low breakpoint if measurements are above High breakpoint.
	// We cannot regulate just humidity values, so it has lower priority. If window is closed (co2 is OK), open the window on DRY.
	if (tempCo2BreakpointH != INVALID_CO2_VALUE && tempCo2Avg > tempCo2BreakpointH)
	{
		rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_OPEN);
		windowOpen = true;
	} else if (tempCo2BreakpointL != INVALID_CO2_VALUE && tempCo2Avg < tempCo2BreakpointL)
			{
				rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_CLOSED);
			}
	vTaskDelay(pdMS_TO_TICKS(3000));
	if (!windowOpen && tempHumidityBreakpointH != INVALID_HUMIDITY_VALUE && tempHumidityAvg > tempHumidityBreakpointH)
	{
		rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_ON_DRY);
	}
	
	// Check if the Air-Conditioner needs to be turned on.
	// If Temperature is below required values -> turn on; Ignore the High breakpoint if measuerements are below the Low breakpoint.
	if (tempTemperatureBreakpointL != INVALID_TEMPERATURE_VALUE && tempTemperatureAvg < tempTemperatureBreakpointL)
		{
			rc_servo_setPosition(CONDITIONER_SERVO_PORT, CONDITIONER_POS_OFF);
		}
		else if (tempTemperatureBreakpointH != INVALID_TEMPERATURE_VALUE && tempTemperatureAvg > tempTemperatureBreakpointH)
				{
					rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_OPEN);
				}
	
}

static void _run(void* params) {
	servoTask_initTask(params);
	
	while (1) {
		servoTask_runTask();
	}
}
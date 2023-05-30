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

#define CONDITIONER_POS_COOL 100
#define CONDITIONER_POS_OFF 0
#define CONDITIONER_POS_HEAT -100

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
	// Default window position defined to be closed/turned off.
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
	
	// Take calculated Averages from DataHolder
	uint16_t tempHumidityAvg = getHumAvg();
	int16_t tempTemperatureAvg = getTempAvg();
	uint16_t tempCo2Avg = getCo2Avg();
	
	// Take set Breakpoints from DataHolder
	uint16_t tempHumidityBreakpointL = getHumidityBreakpointLow();
	uint16_t tempHumidityBreakpointH = getHumidityBreakpointHigh();
	int16_t tempTemperatureBreakpointL = getTemperatureBreakpointLow();
	int16_t tempTemperatureBreakpointH = getTemperatureBreakpointHigh();
	uint16_t tempCo2BreakpointL = getCo2BreakpointLow();
	uint16_t tempCo2BreakpointH = getCo2BreakpointHigh();
	
	
	// -------------------TESTS-------------------------
	
// 	tempCo2BreakpointL = 600;		//window is closed at 1500
// 	tempCo2BreakpointH = 2000;
// 	tempCo2BreakpointL = 100;		//window is open at 1500
// 	tempCo2BreakpointH = 300;

// 	tempHumidityBreakpointH = 200;	// goes to dry mode
// 	tempHumidityBreakpointL = 100;
	
// 	tempHumidityBreakpointH = 90;	// doesn't go to dry mode
// 	tempHumidityBreakpointL = 80;

// 	tempTemperatureBreakpointH = 190;		// air conditioner is on COOL at 27
// 	tempTemperatureBreakpointL = 160;
//  tempTemperatureBreakpointH = 350;		// air conditioner is OFF at 27
//  tempTemperatureBreakpointL = 160;
//  tempTemperatureBreakpointH = 400;		// air conditioner is ON HEAT at 27
//  tempTemperatureBreakpointL = 350;

// All tests passed. Uncomment to simulate the required

	// -------------------TESTS END-------------------------
	
	// In addition to comparing average values we also check if the averages are valid before executing anything
	// Check if the window needs to be open.
	// First check if breakpoints are set.
	// If Co2 levels are above required values -> open window; Ignore the Low breakpoint if measurements are above High breakpoint.
	// We cannot regulate just humidity values, so it has lower priority. If window is closed (co2 is OK), open the window on DRY.
	
	if(tempCo2BreakpointL != INVALID_CO2_VALUE || tempCo2BreakpointH != INVALID_CO2_VALUE)
	{
		if (tempCo2Avg != INVALID_CO2_VALUE && tempCo2Avg > tempCo2BreakpointH)
		{
			rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_OPEN);
			windowOpen = true;
		} else if (tempCo2Avg != INVALID_CO2_VALUE && tempCo2Avg < tempCo2BreakpointL)
		{
			rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_CLOSED);
			windowOpen = false;
		}
		else if (!windowOpen && tempHumidityAvg != INVALID_HUMIDITY_VALUE && tempHumidityAvg > tempHumidityBreakpointH && (tempHumidityBreakpointL != INVALID_HUMIDITY_VALUE || tempHumidityBreakpointH != INVALID_HUMIDITY_VALUE))
		{
			rc_servo_setPosition(WINDOW_SERVO_PORT, WINDOW_POS_ON_DRY);
		}
		
	}
	
	// Check if the Air-Conditioner needs to be turned on.
	// First check if breakpoints are set.
	// If Temperature is below required values -> turn on; Ignore the High breakpoint if measuerements are below the Low breakpoint.
	if(tempTemperatureBreakpointL != INVALID_TEMPERATURE_VALUE || tempTemperatureBreakpointH != INVALID_TEMPERATURE_VALUE)
	{
		if (tempTemperatureAvg != INVALID_TEMPERATURE_VALUE && tempTemperatureAvg < tempTemperatureBreakpointL)
		{
			rc_servo_setPosition(CONDITIONER_SERVO_PORT, CONDITIONER_POS_HEAT);
		}
		else if (tempTemperatureAvg != INVALID_TEMPERATURE_VALUE && tempTemperatureAvg > tempTemperatureBreakpointH)
		{
			rc_servo_setPosition(CONDITIONER_SERVO_PORT, CONDITIONER_POS_COOL);
		}
		else rc_servo_setPosition(CONDITIONER_SERVO_PORT, CONDITIONER_POS_OFF);
	}	
	
}

static void _run(void* params) {
	servoTask_initTask(params);
	
	while (1) {
		servoTask_runTask();
	}
}
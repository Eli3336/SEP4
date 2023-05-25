
/*
 * DisplayTask.c
 *
 * Created: 5/24/2023 5:17:04 PM
 *  Author: ssem5
 */ 

#include <DisplayTask.h>
#include <DataHolder.h>
#include <stdio.h>
#include <stdint.h>
#include <task.h>
#include <event_groups.h>


#define TASK_NAME "DisplayTask"
#define TASK_PRIORITY 1
static EventGroupHandle_t _actEventGrop;
static void _run(void* params);

void displayTask_create(EventGroupHandle_t actEventGroup){
	_actEventGrop = actEventGroup;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
	
}

void displayTask_initTask(void* params){

display_7seg_powerUp();
}

void displayTask_runTask(void){
	xEventGroupWaitBits(_actEventGrop,BIT_WINDOW_ACT,
	pdFALSE,
	pdFALSE,
	portMAX_DELAY);
		uint16_t tempCo2Avg = getCo2Avg();
		uint16_t tempTempAvg = getTempAvg();
		uint16_t tempHumAvg = getHumAvg();
	
		
		
			display_7seg_powerUp();
			float disCo2 = tempCo2Avg;
			float disTemp = tempTempAvg;
			float disHum = tempHumAvg;
			display_7seg_displayHex("A");
			vTaskDelay(pdMS_TO_TICKS(2000));
			display_7seg_display(disHum,0);
			vTaskDelay(pdMS_TO_TICKS(2000));
			display_7seg_displayHex("B");
			vTaskDelay(pdMS_TO_TICKS(2000));
			display_7seg_display(disTemp,0);
			vTaskDelay(pdMS_TO_TICKS(2000));
			display_7seg_displayHex("C");
			vTaskDelay(pdMS_TO_TICKS(2000));
			display_7seg_display(disCo2,0);
			vTaskDelay(pdMS_TO_TICKS(2000));


	
	
}

static void _run(void* params){
	displayTask_initTask(params);
	while(1)
	{
		displayTask_runTask();
	}

}
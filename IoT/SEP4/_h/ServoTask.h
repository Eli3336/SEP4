#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>

void servoTask_create(EventGroupHandle_t actEventGroup);
void servoTask_initTask(void* params);
void servoTask_runTask(void);

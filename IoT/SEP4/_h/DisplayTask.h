#include <atmega_freertos.h>
#include <event_groups.h>


void displayTask_create(EventGroupHandle_t actEventGroup);
void displayTask_initTask(void* params);
void displayTask_runTask(void);

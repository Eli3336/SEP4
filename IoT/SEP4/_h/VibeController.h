#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>

void VibeController_create(QueueHandle_t senderQueue);

void VibeController_initTask(void* params);
void VibeController_runTask(void);
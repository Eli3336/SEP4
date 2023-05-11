#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>

void VibeController_create(QueueHandle_t senderQueue,
QueueHandle_t humidityQueue,
QueueHandle_t temperatureQueue,
QueueHandle_t co2Queue,
EventGroupHandle_t actEventGroup,
EventGroupHandle_t doneEventGroup);

void VibeController_initTask(void* params);
void VibeController_runTask(void);
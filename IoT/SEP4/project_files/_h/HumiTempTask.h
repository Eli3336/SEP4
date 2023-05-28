#include <ATMEGA_FreeRTOS.h>
#include <queue.h>
#include <event_groups.h>

#define BIT_HUMIDITY_ACT 1 << 0
#define BIT_TEMPERATURE_ACT 1 << 1

void humiTempTask_create(QueueHandle_t humidityQueue, 
									QueueHandle_t temperatureQueue, 
									EventGroupHandle_t doEventGroup);
void humiTempTask_initTask(void* params);
void humiTempTask_runTask(void);
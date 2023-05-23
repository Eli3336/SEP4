
/*
 * Counter.h'
 *
 * Created: 5/22/2023 3:42:07 PM
 *  Author: ssem5
 */ 
 #include <atmega_freertos.h>
  #include <queue.h>
  #include <event_groups.h>
  
 int TempPool;
 int TempCount;
 int HumPool;
 int HumCount;
 int PpmPool;
 int PpmCount;
 
 void Counter_initTask(void* params);
 void counter_create(QueueHandle_t humidityQueue,
 QueueHandle_t temperatureQueue,
 QueueHandle_t co2Queue,
 EventGroupHandle_t actEventGroup);
 void Counter_runTask(void);
 void resetAllCounterValues(void);
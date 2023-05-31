 #pragma once
 #include <ATMEGA_FreeRTOS.h>
  #include <queue.h>
  #include <event_groups.h>
  

 
 void Counter_initTask(void* params);
 void counter_create(QueueHandle_t humidityQueue,
 QueueHandle_t temperatureQueue,
 QueueHandle_t co2Queue,
 EventGroupHandle_t actEventGroup);
 void Counter_runTask(void);
 void resetAllCounterValues(void);

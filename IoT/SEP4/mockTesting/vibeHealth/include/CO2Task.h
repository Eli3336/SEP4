 #ifndef co2task_h_
 #define co2task_h_
 
 #include <ATMEGA_FreeRTOS.h>
 #include <queue.h>
 #include <event_groups.h>
 
 #define BIT_CO2_ACT 1 << 2
 #define BIT_CO2_DONE 1 << 2
 
 void co2Task_create(QueueHandle_t co2queue, EventGroupHandle_t doEventGroup, EventGroupHandle_t doneEventGroup);
 void co2Task_initTask(void* params);
 void co2Task_runTask(void);
 
 
 
 #endif /* co2task_h_ */
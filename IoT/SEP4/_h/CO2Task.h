 #ifndef co2task_h_
 #define co2task_h_
 
 #include <atmega_freertos.h>
 #include <queue.h>
 #include <event_groups.h>
 
 #define BIT_CO2_ACT 1 << 2
 #define BIT_CO2_DONE 1 << 2
 
 void co2task_create(QueueHandle_t co2queue, EventGroupHandle_t doEventGroup, EventGroupHandle_t doneEventGroup);
 void co2task_inittask(void* params);
 void co2task_runtask(void);
 
 
 
 #endif /* co2task_h_ */
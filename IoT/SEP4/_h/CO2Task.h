 #ifndef co2task_h_
 #define co2task_h_
 
 #include <atmega_freertos.h>
 #include <queue.h>
 #include <event_groups.h>
 
 #define bit_co2_act 1 << 2
 #define bit_co2_done 1 << 2
 
 void co2task_create(QueueHandle_t co2queue, EventGroupHandle_t acteventgroup, EventGroupHandle_t doneeventgroup);
 void co2task_inittask(void* params);
 void co2task_runtask(void);
 
 
 
 #endif /* co2task_h_ */
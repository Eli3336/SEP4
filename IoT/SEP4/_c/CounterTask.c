
/*
 * C0ounterTask.S
 *
 * Created: 5/22/2023 3:24:47 PM
 *  Author: ssem5
 */ 
 #include <Counter.h>
 #include <stdio.h>
 #include <stdint.h>
 #include <task.h>
 #include <DataHolder.h>
 #include <CO2Task.h>
 #include <event_groups.h>
 #include <HumiTempTask.h>




 #define TASKNAME "DATA_TASK"
 #define TASK_INTERVAL 60000UL // 1 Minute delay
 #define TASK_PRIORITY 4


 static void _run(void* params);


 static QueueHandle_t _humidityQueue;
 static QueueHandle_t _temperatureQueue;
 static QueueHandle_t _co2Queue;
 static EventGroupHandle_t _doEventGroup;
 
  
 



 void counter_create(QueueHandle_t humidityQueue,
 QueueHandle_t temperatureQueue,
 QueueHandle_t co2Queue,
 EventGroupHandle_t actEventGroup){
	 
	 _humidityQueue = humidityQueue;
	 _temperatureQueue = temperatureQueue;
	 _co2Queue = co2Queue;
	 _doEventGroup = actEventGroup;




	 xTaskCreate(_run,
	 TASKNAME,
	 configMINIMAL_STACK_SIZE,
	 NULL,
	 TASK_PRIORITY,
	 NULL
	 );
	 
 }
 
 void Counter_initTask(void* params){
	 resetAllCounterValues();
	 
	 
 }


 
 void Counter_runTask(void){
	 
	 
	 xEventGroupSetBits(_doEventGroup,BIT_HUMIDITY_ACT |BIT_TEMPERATURE_ACT|BIT_CO2_ACT);
	 
	 uint16_t humidity;
	 uint16_t temperature;
	 uint16_t ppm;
	 if (xQueueReceive(_humidityQueue, &humidity, pdMS_TO_TICKS(10000)) != pdTRUE)
	 {
		 
		 humidity = INVALID_HUMIDITY_VALUE;
	 }
	 if (xQueueReceive(_temperatureQueue, &temperature, pdMS_TO_TICKS(10000)) != pdTRUE)
	 {
		 temperature = INVALID_TEMPERATURE_VALUE;
	 }
	 if (xQueueReceive(_co2Queue, &ppm, pdMS_TO_TICKS(10000)) != pdTRUE)
	 {
		 ppm = INVALID_CO2_VALUE;
	 }
	 printf("CO2 Value is : %d\n",ppm);
	 printf("Humidity is : %d \n",humidity);
	 printf("Temperature is : %d \n",temperature);
	 
	 
	 //place the values in the average and rise the counter.
	if (temperature != INVALID_TEMPERATURE_VALUE)
	{
		addTemperture(temperature);
	}
	if (humidity != INVALID_HUMIDITY_VALUE)
	{
		addHumidity(humidity);
	}
	if (ppm != INVALID_CO2_VALUE)
	{
		addPPM(ppm);
	}
	 
	 TickType_t lastWakeTime = xTaskGetTickCount();
	 xTaskDelayUntil(&lastWakeTime, pdMS_TO_TICKS(TASK_INTERVAL));
	 
	 
 }
 
 static void _run(void* params){
	 Counter_initTask(params);
	 
	 
	 while (1)
	 {
		Counter_runTask();
	 }
 }
 
 
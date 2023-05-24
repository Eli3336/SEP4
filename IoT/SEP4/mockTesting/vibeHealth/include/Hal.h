
#include <stdint.h>
#include "ATMEGA_FreeRTOS.h"
#include "task.h"
#include "queue.h"
#include "event_groups.h"
#include <mh_z19.h>
#include <hih8120.h>
#include <stddef.h>
#include <status_leds.h>
#include <lora_driver.h>
void co2Task_initTask(void *params);
void humiTempTask_initTask(void *params);
void validateHumTemp();
void senderTask_initTask(void *params);
void receiverTask_initTask(void *params);
void servoTask_initTask(void *params);

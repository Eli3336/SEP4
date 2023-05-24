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

// Hardware Abstraction Layer (HAL) functions.

//CO2Task
static void _co2CallBack(uint16_t ppm){
	xQueueSendToBack(_co2Queue, &ppm, portMAX_DELAY);
}

void co2Task_initTask(void* params) {
	mh_z19_injectCallBack(_co2CallBack);
}


//HumiTempTask
void humiTempTask_initTask(void* params) {
	hih8120_wakeup();
}

void validateHumTemp(){
if (hih8120_wakeup() == HIH8120_OK) {
		vTaskDelay(pdMS_TO_TICKS(100));
		
		if (hih8120_measure() == HIH8120_OK) {
			vTaskDelay(pdMS_TO_TICKS(50));
			_latestHumidity = hih8120_getHumidityPercent_x10();
			_latestTemperature = hih8120_getTemperature_x10();
		} else {
			_latestHumidity = INVALID_HUMIDITY_VALUE;
			_latestTemperature = INVALID_TEMPERATURE_VALUE;			
		}
	} else {
		_latestHumidity = INVALID_HUMIDITY_VALUE;
		_latestTemperature = INVALID_TEMPERATURE_VALUE;
	}
}


//SenderTask
void senderTask_initTask(void* params) {
		vTaskDelay(50UL);
		lora_driver_resetRn2483(1);
		vTaskDelay(100UL);
		lora_driver_resetRn2483(0);
		lora_driver_flushBuffers();
		//_connectToLoRaWAN();

	puts("Sender Task initialized");
}

static void _connectToLoRaWAN() {
	puts("Start Connect To Lorawan");
	char _out_buf[20];
	lora_driver_returnCode_t rc;
	status_leds_slowBlink(led_ST2); // OPTIONAL: Led the green led blink slowly while we are setting up LoRa

	// Factory reset the transceiver
	printf("FactoryReset >%s<\n", lora_driver_mapReturnCodeToText(lora_driver_rn2483FactoryReset()));
	
	// Configure to EU868 LoRaWAN standards
	printf("Configure to EU868 >%s<\n", lora_driver_mapReturnCodeToText(lora_driver_configureToEu868()));

	// Get the transceivers HW EUI
	rc = lora_driver_getRn2483Hweui(_out_buf);
	printf("Get HWEUI >%s<: %s\n",lora_driver_mapReturnCodeToText(rc), _out_buf);

	// Set the HWEUI as DevEUI in the LoRaWAN software stack in the transceiver
	printf("Set DevEUI: %s >%s<\n", _out_buf, lora_driver_mapReturnCodeToText(lora_driver_setDeviceIdentifier(_out_buf)));

	// Set Over The Air Activation parameters to be ready to join the LoRaWAN
	printf("Set OTAA Identity appEUI:%s appKEY:%s devEUI:%s >%s<\n", LORA_appEUI, LORA_appKEY, _out_buf, lora_driver_mapReturnCodeToText(lora_driver_setOtaaIdentity(LORA_appEUI,LORA_appKEY,_out_buf)));

	// Save all the MAC settings in the transceiver
	printf("Save mac >%s<\n",lora_driver_mapReturnCodeToText(lora_driver_saveMac()));

	// Enable Adaptive Data Rate
	printf("Set Adaptive Data Rate: ON >%s<\n", lora_driver_mapReturnCodeToText(lora_driver_setAdaptiveDataRate(LORA_ON)));

	// Set receiver window1 delay to 500 ms - this is needed if down-link messages will be used
	printf("Set Receiver Delay: %d ms >%s<\n", 500, lora_driver_mapReturnCodeToText(lora_driver_setReceiveDelay(500)));

	// Join the LoRaWAN
	uint8_t maxJoinTriesLeft = 10;
	
	do {
		rc = lora_driver_join(LORA_OTAA);
		
		printf("Join Network TriesLeft:%d >%s<\n", maxJoinTriesLeft, lora_driver_mapReturnCodeToText(rc));
		status_leds_ledOn(led_ST2); // OPTIONAL

		if ( rc != LORA_ACCEPTED)
		{
			// Make the red led pulse to tell something went wrong
			status_leds_longPuls(led_ST1); // OPTIONAL
			// Wait 5 sec and lets try again
			vTaskDelay(pdMS_TO_TICKS(5000UL));
			
		}
		else
		{
			break;
		}
	} while (--maxJoinTriesLeft);
}


//ReceiverTask
void receiverTask_initTask(void* params) {
}


//ServoTask
void servoTask_initTask(void* params) {
	// Default the starting window position to be between open and closed.
	rc_servo_setPosition(SERVO_PORT, SERVO_POS_MIDDLE);
}

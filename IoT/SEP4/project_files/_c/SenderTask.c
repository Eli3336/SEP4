#include <SenderTask.h>
#include <stdio.h>
#include <status_leds.h>
#include <lora_driver.h>
#include <stdint.h>
#include <task.h>


#define TASK_NAME "SenderTask"
#define TASK_PRIORITY 3
#define LORA_appEUI "6BE1FDCE7E214CF9"
#define LORA_appKEY "EECCD39BD2AB6C6BD107A08E0DBE9DB9"

static void _run(void* params);
 void _connectToLoRaWAN();

static bool connected; // check if the connection to LoRaWAN exists

static QueueHandle_t _senderQueue;

void senderTask_create(QueueHandle_t senderQueue) {
	_senderQueue = senderQueue;
	
	xTaskCreate(_run,
	TASK_NAME,
	configMINIMAL_STACK_SIZE,
	NULL,
	TASK_PRIORITY,
	NULL
	);
}

void senderTask_initTask(void* params) {
	vTaskDelay(50UL);
	lora_driver_resetRn2483(1);
	vTaskDelay(100UL);
	lora_driver_resetRn2483(0);
	lora_driver_flushBuffers();
	_connectToLoRaWAN();
}

void senderTask_runTask() {
	lora_driver_payload_t uplinkPayload;
		xQueueReceive(_senderQueue, &uplinkPayload, portMAX_DELAY);
		
	printf("Payload to send: \n");
	for(int i=0; i <uplinkPayload.len; i++)
	{
		printf("%02X ",uplinkPayload.bytes[i]);
			
	}
	printf("\n");
	
	if (lora_driver_join == LORA_ACCEPTED)
	{
		lora_driver_sendUploadMessage(false, &uplinkPayload);
	}else{
	 printf("No connection to LoRaWAN detected, message not sent\n");
	}
}

static void _run(void* params) {
	senderTask_initTask(params);
	
	while (1) {
	senderTask_runTask();
	}
}

 void _connectToLoRaWAN() {
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

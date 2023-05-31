#include "gtest/gtest.h"
#include "FreeRTOS_defs/FreeRTOS_FFF_MocksDeclaration.h"

extern "C"
{
	#include <lora_driver.h>
	#include "ReceiverTask.h"
}

MessageBufferHandle_t messageBuffer;

FAKE_VOID_FUNC(dataHolder_setBreakpoints, lora_driver_payload_t);

class Sendertest : public ::testing::Test
{
protected:
	void SetUp() override
	{
        RESET_FAKE(xMessageBufferReceive);
        RESET_FAKE(xTaskCreate);
        RESET_FAKE(vTaskDelay);
        RESET_FAKE(dataHolder_setBreakpoints);
		RESET_FAKE(xSemaphoreCreateMutex);
		RESET_FAKE(xSemaphoreGive);
		RESET_FAKE(xSemaphoreTake);
		FFF_RESET_HISTORY();
	}
	void TearDown() override
	{}
};
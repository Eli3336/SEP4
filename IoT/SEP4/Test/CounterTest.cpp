#include "gtest/gtest.h"
#include "FreeRTOS_defs/FreeRTOS_FFF_MocksDeclaration.h"

extern "C"
{
	#include <Counter.h>
    #include <DataHolder.h>

}


FAKE_VOID_FUNC(addHumidity,uint16_t);
FAKE_VOID_FUNC(addTemperture,uint16_t);
FAKE_VOID_FUNC(addPPM,uint16_t);

class CounterTest : public ::testing::Test
{
protected:
	void SetUp() override
	{
		RESET_FAKE(xSemaphoreCreateMutex);
		RESET_FAKE(xTaskCreate);
		RESET_FAKE(xEventGroupSetBits);
		RESET_FAKE(xEventGroupWaitBits);
		RESET_FAKE(xQueueReceive);
		RESET_FAKE(xQueueSendToBack);
		RESET_FAKE(xTaskGetTickCount);
		RESET_FAKE(xTaskDelayUntil);
		FFF_RESET_HISTORY();
	}
	void TearDown() override
	{}
};

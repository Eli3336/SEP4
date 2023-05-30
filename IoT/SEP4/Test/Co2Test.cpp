#include "gtest/gtest.h"
#include "FreeRTOS_defs/FreeRTOS_FFF_MocksDeclaration.h"

extern "C"
{
	#include <mh_z19.h>
	#include "CO2Task.h"
}

// Create Fake Driver functions
FAKE_VOID_FUNC( _co2CallBack,uint16_t);
FAKE_VALUE_FUNC(mh_z19_returnCode_t, mh_z19_takeMeassuring);

typedef void (*mh_z19_callback)(uint16_t);
FAKE_VOID_FUNC(mh_z19_injectCallBack, mh_z19_callback);




class Co2test : public ::testing::Test
{
protected:
	void SetUp() override
	{
		RESET_FAKE(xEventGroupSetBits);
		RESET_FAKE(xEventGroupWaitBits);
		RESET_FAKE(xQueueReceive);
		RESET_FAKE(xQueueSendToBack);
		RESET_FAKE(xTaskGetTickCount);
		RESET_FAKE(xTaskDelayUntil);
        RESET_FAKE(mh_z19_takeMeassuring);
        RESET_FAKE(_co2CallBack);
		RESET_FAKE(mh_z19_injectCallBack);
		FFF_RESET_HISTORY();
	}
	void TearDown() override
	{}
};

TEST_F(Co2test, callBackCalledAfterRun)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_CO2_ACT);
	mh_z19_takeMeassuring_fake.return_val = MHZ19_NO_SERIAL;

	// Act
	co2task_runtask();
	

	// Assert/Expect
	ASSERT_EQ(_co2CallBack_fake.call_count,1);
}
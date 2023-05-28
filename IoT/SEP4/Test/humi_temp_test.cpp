#include "gtest/gtest.h"
#include "FreeRTOS_defs/FreeRTOS_FFF_MocksDeclaration.h"

// Include interfaces and define global variables
// defined by the production code
extern "C"
{
	#include <hih8120.h>
	#include "HumiTempTask.h"
}

// Create Fake Driver functions
FAKE_VALUE_FUNC(hih8120_driverReturnCode_t, hih8120_wakeup);
FAKE_VALUE_FUNC(hih8120_driverReturnCode_t, hih8120_measure);
FAKE_VALUE_FUNC(uint16_t, hih8120_getHumidityPercent_x10);
FAKE_VALUE_FUNC(int16_t, hih8120_getTemperature_x10);

// Create Test fixture and Reset all Mocks before each test
class HumiTemp : public ::testing::Test
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

TEST_F(HumiTemp, HumidityReturnAfterEventGroupIsSet)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_HUMIDITY_ACT);

	uint16_t humidity_return = 120;
	uint16_t _humidity;
	hih8120_getHumidityPercent_x10_fake.return_val = humidity_return;

	// Act
	humiTempTask_runTask();
	_humidity = hih8120_getHumidityPercent_x10();

	// Assert/Expect
	ASSERT_EQ(_humidity, humidity_return);
}

TEST_F(HumiTemp, TemperatureReturnAfterEventGroupIsSet)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_TEMPERATURE_ACT);

	uint16_t temperature_return = 200;
	uint16_t _temperature;
	hih8120_getTemperature_x10_fake.return_val = temperature_return;

	// Act
	humiTempTask_runTask();
	_temperature = hih8120_getTemperature_x10();

	// Assert/Expect
	ASSERT_EQ(_temperature, temperature_return);
}

TEST_F(HumiTemp, TemperatureReturnAfterEventGroupIsNotSet)
{
	// Arrange
	xEventGroupWaitBits_fake.return_val = (BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT);

	// Act
	humiTempTask_runTask();
	
	// Assert/Expect
	EXPECT_EQ(1, xEventGroupWaitBits_fake.call_count);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg2_val);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg3_val);
	EXPECT_EQ(portMAX_DELAY, xEventGroupWaitBits_fake.arg4_val);
}

TEST_F(HumiTemp, TemperatureAndHumidityMeasuredArePutInTheQueue)
{
	// Arrange
	humiTempTask_runTask();

	EXPECT_EQ(2, xQueueSendToBack_fake.call_count);
}

TEST_F(HumiTemp, TemperatureAndHumidityIsPutToInvalidValueWhenWakeUpIsNotOK)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	hih8120_getHumidityPercent_x10_fake.return_val = 100;
	hih8120_driverReturnCode_t returnMessage = HIH8120_DRIVER_NOT_INITIALISED;
	hih8120_wakeup_fake.return_val = returnMessage;


	// Act
	humiTempTask_runTask();

	EXPECT_EQ(0, *(uint16_t*)xQueueSendToBack_fake.arg1_history[0]);
	EXPECT_EQ(-100, *(int16_t*)xQueueSendToBack_fake.arg1_history[1]);
}

TEST_F(HumiTemp, TemperatureAndHumidityIsPutToInvalidValueWhenTheMeasurementIsNotOK)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	hih8120_getHumidityPercent_x10_fake.return_val = 100;
	hih8120_driverReturnCode_t returnMessage = HIH8120_DRIVER_NOT_INITIALISED;
	hih8120_measure_fake.return_val = returnMessage;


	// Act
	humiTempTask_runTask();

	EXPECT_EQ(0, *(uint16_t*)xQueueSendToBack_fake.arg1_history[0]);
	EXPECT_EQ(-100, *(int16_t*)xQueueSendToBack_fake.arg1_history[1]);
}
TEST_F(HumiTemp, TemperatureAndHumidityIsPutToValidValueWhenTheMeasurementIsOK)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	hih8120_getHumidityPercent_x10_fake.return_val = 100;
	hih8120_getTemperature_x10_fake.return_val = 240;
	hih8120_driverReturnCode_t returnMessage = HIH8120_OK;
	hih8120_measure_fake.return_val = returnMessage;
	hih8120_wakeup_fake.return_val= returnMessage;


	// Act
	humiTempTask_runTask();

	EXPECT_EQ(100, *(uint16_t*)xQueueSendToBack_fake.arg1_history[0]);
	EXPECT_EQ(240, *(int16_t*)xQueueSendToBack_fake.arg1_history[1]);
}


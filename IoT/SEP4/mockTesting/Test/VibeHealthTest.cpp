#include "gtest/gtest.h"
#include "../FakeFreeRTOS/FreeRTOS_FFF_MocksDeclaration.h"
#include "../fff/fff.h"

extern "C"
{
	// include header files
}
/*
FAKE_VALUE_FUNC(lora_driver_payload_t, uplinkMessageBuilder_buildUplinkMessage, uint8_t);
FAKE_VOID_FUNC(uplinkMessageBuilder_setHumidityData, uint16_t);
FAKE_VOID_FUNC(uplinkMessageBuilder_setTemperatureData, uint16_t);
FAKE_VOID_FUNC(uplinkMessageBuilder_setCO2Data, uint16_t);
FAKE_VOID_FUNC(uplinkMessageBuilder_setSystemErrorState);

class VibeHealthTest : public ::testing::Test {
	protected:
		void SetUp() override {
				RESET_FAKE(xTaskCreate);
				RESET_FAKE(xEventGroupSetBits);
				RESET_FAKE(xEventGroupWaitBits);
				RESET_FAKE(xQueueReceive);
				RESET_FAKE(xQueueSendToBack);
				RESET_FAKE(xTaskGetTickCount);
				RESET_FAKE(xTaskDelayUntil);
				RESET_FAKE(uplinkMessageBuilder_buildUplinkMessage);
				RESET_FAKE(uplinkMessageBuilder_setHumidityData);
				RESET_FAKE(uplinkMessageBuilder_setTemperatureData);
				RESET_FAKE(uplinkMessageBuilder_setCO2Data);
				RESET_FAKE(uplinkMessageBuilder_setSystemErrorState);

				FFF_RESET_HISTORY();
			}

		void TearDown() override {}

};

TEST_F(VibeHealthTest, Create_CallsTaskCreate) {
	VibeController_create(nullptr, nullptr, nullptr, nullptr, nullptr, nullptr);

	EXPECT_EQ(1, xTaskCreate_fake.call_count);

	EXPECT_EQ(configMINIMAL_STACK_SIZE, xTaskCreate_fake.arg2_val);
	EXPECT_EQ(NULL, xTaskCreate_fake.arg3_val);
	EXPECT_EQ(configMAX_PRIORITIES - 1, xTaskCreate_fake.arg4_val);
	EXPECT_EQ(NULL, xTaskCreate_fake.arg5_val);
}


TEST_F(VibeHealthTest, Task_SetsEventGroupBitsToStartHumidityAndTemperature) {
	//Arrange
	xEventGroupSetBits_fake.return_val = 0;
	xEventGroupWaitBits_fake.return_val = (BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE);

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xEventGroupSetBits_fake.call_count);

	EXPECT_EQ(BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT, xEventGroupSetBits_fake.arg1_val);
	EXPECT_EQ(0, uplinkMessageBuilder_setSystemErrorState_fake.call_count);
}

TEST_F(VibeHealthTest, Task_SetsEventGroupBitsToStartHumidityAndTemperatureReturnBotsNotCleared) {
	//Arrange
	xEventGroupSetBits_fake.return_val = (BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT);

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xEventGroupSetBits_fake.call_count);
	EXPECT_EQ(BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT, xEventGroupSetBits_fake.arg1_val);
	EXPECT_EQ(1, uplinkMessageBuilder_setSystemErrorState_fake.call_count);
}

TEST_F(VibeHealthTest, Task_WaitsForAllEventGroupBitsToBeSet) {
	//Arrange
	xEventGroupWaitBits_fake.return_val = (BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE);

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xEventGroupWaitBits_fake.call_count);

	EXPECT_EQ(BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE, xEventGroupWaitBits_fake.arg1_val);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg2_val);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg3_val);
	EXPECT_EQ(pdMS_TO_TICKS(300000UL), xEventGroupWaitBits_fake.arg4_val);
	EXPECT_EQ(0, uplinkMessageBuilder_setSystemErrorState_fake.call_count);
}

TEST_F(VibeHealthTest, Task_WaitsForAllEventGroupBitsToBeSetNotAllBitsSet) {
	//Arrange
	xEventGroupWaitBits_fake.return_val = 0;

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xEventGroupWaitBits_fake.call_count);

	EXPECT_EQ(BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE, xEventGroupWaitBits_fake.arg1_val);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg2_val);
	EXPECT_EQ(pdTRUE, xEventGroupWaitBits_fake.arg3_val);
	EXPECT_EQ(pdMS_TO_TICKS(300000UL), xEventGroupWaitBits_fake.arg4_val);
	EXPECT_EQ(1, uplinkMessageBuilder_setSystemErrorState_fake.call_count);
}


TEST_F(VibeHealthTest, Task_ReceivesFourMeasurementsFromTheQueue) {
	// Arrange
	// Act
	VibeController_runTask();

	// Assert
	EXPECT_EQ(4, xQueueReceive_fake.call_count);

	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueReceive_fake.arg2_history[0]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueReceive_fake.arg2_history[1]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueReceive_fake.arg2_history[2]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueReceive_fake.arg2_history[3]);
}


TEST_F(VibeHealthTest, Task_FailToReceivesFourMeasurementsFromTheQueue) {
	// Arrange
	xQueueReceive_fake.return_val = pdFALSE;
	// Act
	VibeController_runTask();

	// Assert
	EXPECT_EQ(4, xQueueReceive_fake.call_count);

	EXPECT_EQ(CONFIG_INVALID_HUMIDITY_VALUE, uplinkMessageBuilder_setHumidityData_fake.arg0_val);
	EXPECT_EQ(CONFIG_INVALID_TEMPERATURE_VALUE, uplinkMessageBuilder_setTemperatureData_fake.arg0_val);
	EXPECT_EQ(CONFIG_INVALID_CO2_VALUE, uplinkMessageBuilder_setCO2Data_fake.arg0_val);
}

TEST_F(VibeHealthTest, Task_SendsMeasurementsToServoQueueWhenNoErrorState) {
	//Arrange
	xEventGroupWaitBits_fake.return_val = (BIT_HUMIDITY_DONE | BIT_TEMPERATURE_DONE | BIT_CO2_DONE);
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(4, xQueueSendToBack_fake.call_count);

	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueSendToBack_fake.arg2_history[0]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueSendToBack_fake.arg2_history[1]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueSendToBack_fake.arg2_history[2]);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueSendToBack_fake.arg2_history[3]);
}

TEST_F(VibeHealthTest, Task_DoesNotSendMeasurementsToServoQueueWhenErrorStateHasOccured) {
	//Arrange
	xEventGroupSetBits_fake.return_val = (BIT_HUMIDITY_ACT | BIT_TEMPERATURE_ACT);

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(0, xQueueSendToBack_fake.call_count);
}


TEST_F(VibeHealthTest, Task_CallsUplinkMessageBuilderSetHumidityDataOneTime) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, uplinkMessageBuilder_setHumidityData_fake.call_count);
}

TEST_F(VibeHealthTest, Task_CallsUplinkMessageBuilderSetTemperatureDataOneTime) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, uplinkMessageBuilder_setTemperatureData_fake.call_count);
}

TEST_F(VibeHealthTest, Task_CallsUplinkMessageBuilderSetCo2DataOneTime) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, uplinkMessageBuilder_setCO2Data_fake.call_count);
}

TEST_F(VibeHealthTest, Task_CallsBuildUplinkMessageWithPortNumberOneTime) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, uplinkMessageBuilder_buildUplinkMessage_fake.call_count);
	EXPECT_EQ(1, uplinkMessageBuilder_buildUplinkMessage_fake.arg0_val);
}

TEST_F(VibeHealthTest, Task_SendsToServerTaskIfUplinkMessageBuilderReturnsFilledPayload) {
	//Arrange
	uplinkMessageBuilder_buildUplinkMessage_fake.return_val.len = 8;

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xQueueSendToBack_fake.call_count);
	EXPECT_EQ(pdMS_TO_TICKS(10000), xQueueSendToBack_fake.arg2_val);
}

TEST_F(VibeHealthTest, Task_DoesNotSendToServerTaskIfUplinkMessageBuilderReturnsEmptyPayload) {
	//Arrange
	uplinkMessageBuilder_buildUplinkMessage_fake.return_val.len = 0;

	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(0, xQueueSendToBack_fake.call_count);
}

TEST_F(VibeHealthTest, Task_CallsTaskGetTickCount) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xTaskGetTickCount_fake.call_count);
}

TEST_F(VibeHealthTest, Task_CallsDelayUntilWithFiveMinuteDelay) {
	//Arrange
	//Act
	VibeController_runTask();

	//Assert
	EXPECT_EQ(1, xTaskDelayUntil_fake.call_count);
	EXPECT_EQ(pdMS_TO_TICKS(300000UL), xTaskDelayUntil_fake.arg1_val);
}*/
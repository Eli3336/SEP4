#include "gtest/gtest.h"
#include "FreeRTOS_defs/FreeRTOS_FFF_MocksDeclaration.h"

// Include interfaces and define global variables
// defined by the production code

extern "C"
{
	#include <rc_servo.h>
	#include "ServoTask.h"
	#include <Counter.h>
	#include <semphr.h>
	#include <DataHolder.h>
	#include <lora_driver.h>
	#include <message_buffer.h>
	#include <ReceiverTask.h>


	


	
}

extern SemaphoreHandle_t mutexAvgValues;
extern MessageBufferHandle_t messageBuffer;


// Create Fake Driver functions.
FAKE_VOID_FUNC(rc_servo_setPosition,uint8_t,int8_t);

FAKE_VALUE_FUNC(uint16_t, getHumAvg);
FAKE_VALUE_FUNC(uint16_t, getCo2Avg);
FAKE_VALUE_FUNC(int16_t, getTempAvg);

FAKE_VOID_FUNC(dataHolder_setBreakpoints,lora_driver_payload_t);

FAKE_VOID_FUNC(addHumidity,uint16_t);
FAKE_VOID_FUNC(addTemperture,int16_t);
FAKE_VOID_FUNC(addPPM,uint16_t);
FAKE_VOID_FUNC(resetAllCounterValues);

FAKE_VALUE_FUNC(uint16_t, getHumidityBreakpointLow);
FAKE_VALUE_FUNC(uint16_t, getHumidityBreakpointHigh);

FAKE_VALUE_FUNC(uint16_t, getCo2BreakpointLow);
FAKE_VALUE_FUNC(uint16_t, getCo2BreakpointHigh);

FAKE_VALUE_FUNC(int16_t, getTemperatureBreakpointLow);
FAKE_VALUE_FUNC(int16_t, getTemperatureBreakpointHigh);


// Create Test fixture and Reset all Mocks before each test
class Servo : public ::testing::Test
{
protected:
	void SetUp() override
	{
		RESET_FAKE(xSemaphoreCreateMutex);
		RESET_FAKE(xSemaphoreGive);
		RESET_FAKE(xSemaphoreTake);
		RESET_FAKE(xTaskCreate);
		RESET_FAKE(xEventGroupSetBits);
		RESET_FAKE(xEventGroupWaitBits);
		RESET_FAKE(xTaskGetTickCount);
		RESET_FAKE(xTaskDelayUntil);
		RESET_FAKE(rc_servo_setPosition);
        RESET_FAKE(addHumidity);
        RESET_FAKE(addTemperture);
        RESET_FAKE(getTempAvg);
		RESET_FAKE(addPPM);
        RESET_FAKE(getCo2Avg);
        RESET_FAKE(getTempAvg);
        RESET_FAKE(getHumidityBreakpointLow);
        RESET_FAKE(getHumidityBreakpointHigh);
        RESET_FAKE(getCo2BreakpointLow);
        RESET_FAKE(getCo2BreakpointHigh);
        RESET_FAKE(getTemperatureBreakpointLow);
        RESET_FAKE(getTemperatureBreakpointHigh);
		RESET_FAKE(resetAllCounterValues);
		RESET_FAKE(xQueueReceive);
		RESET_FAKE(xQueueSendToBack);
		RESET_FAKE(dataHolder_setBreakpoints);
		RESET_FAKE(xMessageBufferReceive);
        RESET_FAKE(xMessageBufferSend);
		FFF_RESET_HISTORY();
	}
	void TearDown() override
	{}
};


TEST_F(Servo,noBreakpointsHasBeenExceeded)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 600;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =50;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 140;;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}
TEST_F(Servo,BreakpointsontheLowLimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 500;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =20;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 100;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}
TEST_F(Servo,BreakpointsontheHighLimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 800;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =80;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 300;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}

TEST_F(Servo,HumidityUnderthelimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 600;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =10;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 200;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}
TEST_F(Servo,HumidityOverthelimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 600;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =100;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 200;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(2,rc_servo_setPosition_fake.call_count);	
}

TEST_F(Servo,TemperatureUnderTheimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 600;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =60;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 80;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}
TEST_F(Servo,TemperatureOverTheimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 600;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =60;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 1000;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(1,rc_servo_setPosition_fake.call_count);	
}

TEST_F(Servo,CO2LevelUnderTheimit)
{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 400;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =60;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 200;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(2,rc_servo_setPosition_fake.call_count);	
}
TEST_F(Servo,CO2LevelOverTheimit)


{
	// Arrange
	xEventGroupSetBits_fake.return_val = 1;
	xEventGroupWaitBits_fake.return_val = (BIT_SERVOS_ACT);

	uint16_t humidityLow = 20;
	uint16_t humidityHigh = 80;
	uint16_t Co2Low = 500;
	uint16_t Co2High = 800;
	int16_t temperatureLow = 100;
	int16_t temperatureHigh = 300;

	getCo2BreakpointHigh_fake.return_val = Co2High;
	getCo2BreakpointLow_fake.return_val = Co2Low;
	getCo2Avg_fake.return_val = 2000;

	getHumidityBreakpointLow_fake.return_val = humidityLow;
	getHumidityBreakpointHigh_fake.return_val = humidityHigh;
	getHumAvg_fake.return_val =60;

	getTemperatureBreakpointLow_fake.return_val = temperatureLow;
	getTemperatureBreakpointHigh_fake.return_val = temperatureHigh;
	getTempAvg_fake.return_val = 200;


	// Act
	servoTask_runTask();

	// Assert/Expect
	ASSERT_EQ(2,rc_servo_setPosition_fake.call_count);	
}

TEST_F(Servo,CounterDontAddValuesIfTheyAreInvalid){
	// Arr  	

	// Act
	Counter_runTask();

	// Assert/Expect
	ASSERT_EQ(addPPM_fake.call_count,0);
	ASSERT_EQ(addTemperture_fake.call_count,0);
	ASSERT_EQ(addHumidity_fake.call_count,0);
	ASSERT_EQ(xEventGroupSetBits_fake.call_count,1);

}
TEST_F(Servo,Rreceive){
	lora_driver_payload_t fake;
	// Arr  
	xMessageBufferSend_fake.arg1_history[0]=&fake;

	// Act
	receiverTask_runTask();

	// Assert/Expect
	EXPECT_EQ(xMessageBufferReceive_fake.call_count,1);

}
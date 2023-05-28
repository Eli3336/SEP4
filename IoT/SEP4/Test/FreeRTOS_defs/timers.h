#pragma once
#include "FreeRTOS.h"
#include "task.h"

typedef struct TimerDef_t* TimerHandle_t;
typedef void (*TimerCallbackFunction_t)(TimerHandle_t xTimer);

TimerHandle_t xTimerCreate(const char* const pcTimerName, const TickType_t xTimerPeriod, const UBaseType_t uxAutoReload, void* const pvTimerID, TimerCallbackFunction_t pxCallbackFunction);
BaseType_t xTimerIsTimerActive(TimerHandle_t xTimer);
BaseType_t xTimerStart(TimerHandle_t xTimer, TickType_t xBlockTime);
BaseType_t xTimerStop(TimerHandle_t xTimer, TickType_t xBlockTime);
BaseType_t xTimerChangePeriod(TimerHandle_t xTimer, TickType_t xNewPeriod, TickType_t xBlockTime);
BaseType_t xTimerDelete(TimerHandle_t xTimer, TickType_t xBlockTime);
BaseType_t xTimerReset(TimerHandle_t xTimer, TickType_t xBlockTime);
void* pvTimerGetTimerID(TimerHandle_t xTimer);
void vTimerSetReloadMode(TimerHandle_t xTimer, const UBaseType_t uxAutoReload);
void vTimerSetTimerID(TimerHandle_t xTimer, void* pvNewID);
TaskHandle_t xTimerGetTimerDaemonTaskHandle(void);
const char* pcTimerGetName(TimerHandle_t xTimer);
TickType_t xTimerGetPeriod(TimerHandle_t xTimer);
TickType_t xTimerGetExpiryTime(TimerHandle_t xTimer);
UBaseType_t uxTimerGetReloadMode(TimerHandle_t xTimer);
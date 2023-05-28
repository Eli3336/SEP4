#pragma once
#include "FreeRTOS.h"

typedef void* TaskHandle_t;
typedef void (*TaskFunction_t)(void*);
typedef size_t TaskStatus_t;

// Task Creation
BaseType_t xTaskCreate(TaskFunction_t pvTaskCode,
    const char* const pcName,
    configSTACK_DEPTH_TYPE usStackDepth,
    void* pvParameters,
    UBaseType_t uxPriority,
    TaskHandle_t* pxCreatedTask
);
TaskHandle_t xTaskCreateStatic(TaskFunction_t pxTaskCode,
    const char* const pcName,
    const uint32_t ulStackDepth,
    void* const pvParameters,
    UBaseType_t uxPriority,
    StackType_t* const puxStackBuffer,
    StaticTask_t* const pxTaskBuffer);
void vTaskDelete(TaskHandle_t xTask);

// Task Control
void vTaskDelay(const TickType_t xTicksToDelay);
void vTaskDelayUntil(TickType_t* pxPreviousWakeTime, const TickType_t xTimeIncrement);
BaseType_t xTaskDelayUntil( TickType_t *pxPreviousWakeTime, const TickType_t xTimeIncrement );
UBaseType_t uxTaskPriorityGet(TaskHandle_t xTask);
void vTaskPrioritySet(TaskHandle_t xTask,
    UBaseType_t uxNewPriority);
void vTaskSuspend(TaskHandle_t xTaskToSuspend);
void vTaskResume(TaskHandle_t xTaskToResume);
BaseType_t xTaskResumeFromISR(TaskHandle_t xTaskToResume);
BaseType_t xTaskAbortDelay(TaskHandle_t xTask);

// Task Utilities
TickType_t xTaskGetTickCount(void);

UBaseType_t uxTaskGetSystemState(
    TaskStatus_t* const pxTaskStatusArray,
    const UBaseType_t uxArraySize,
    unsigned long* const pulTotalRunTime);
void vTaskGetInfo(TaskHandle_t xTask,
    TaskStatus_t* pxTaskStatus,
    BaseType_t xGetFreeStackSpace,
    eTaskState eState);

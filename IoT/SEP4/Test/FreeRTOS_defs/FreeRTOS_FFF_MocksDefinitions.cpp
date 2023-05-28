#include "FreeRTOS_FFF_MocksDeclaration.h"
DEFINE_FFF_GLOBALS

// --- Define mocks for FreeRTOS functions ---
// task.h
// BaseType_t xTaskCreate(TaskFunction_t pvTaskCode, const char* const pcName, configSTACK_DEPTH_TYPE usStackDepth, void* pvParameters, UBaseType_t uxPriority, TaskHandle_t* pxCreatedTask);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTaskCreate, TaskFunction_t, const char*, configSTACK_DEPTH_TYPE, void*, UBaseType_t, TaskHandle_t*);
//TaskHandle_t xTaskCreateStatic(TaskFunction_t pxTaskCode, const char* const pcName, const uint32_t ulStackDepth, void* const pvParameters, UBaseType_t uxPriority, StackType_t* const puxStackBuffer, StaticTask_t* const pxTaskBuffer);
DEFINE_FAKE_VALUE_FUNC(TaskHandle_t, xTaskCreateStatic, TaskFunction_t, const char*, uint32_t, void*, UBaseType_t, StackType_t*, StaticTask_t*);
// void vTaskDelete(TaskHandle_t xTask);
DEFINE_FAKE_VOID_FUNC(vTaskDelete, TaskHandle_t);
// void vTaskDelay( const TickType_t xTicksToDelay );
DEFINE_FAKE_VOID_FUNC(vTaskDelay, TickType_t);
//void vTaskDelayUntil(TickType_t* pxPreviousWakeTime, const TickType_t xTimeIncrement);
DEFINE_FAKE_VOID_FUNC(vTaskDelayUntil, TickType_t*, TickType_t);
// BaseType_t xTaskDelayUntil( TickType_t *pxPreviousWakeTime, const TickType_t xTimeIncrement );
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTaskDelayUntil, TickType_t*, TickType_t);
// TickType_t xTaskGetTickCount(void);
DEFINE_FAKE_VALUE_FUNC(TickType_t, xTaskGetTickCount);
// UBaseType_t uxTaskPriorityGet(TaskHandle_t xTask);
DEFINE_FAKE_VALUE_FUNC(UBaseType_t, uxTaskPriorityGet, TaskHandle_t);
// void vTaskPrioritySet(TaskHandle_t xTask, UBaseType_t uxNewPriority);
DEFINE_FAKE_VOID_FUNC(vTaskPrioritySet, TaskHandle_t, UBaseType_t);
// void vTaskSuspend(TaskHandle_t xTaskToSuspend);
DEFINE_FAKE_VOID_FUNC(vTaskSuspend, TaskHandle_t);
// void vTaskResume(TaskHandle_t xTaskToResume);
DEFINE_FAKE_VOID_FUNC(vTaskResume, TaskHandle_t);
// BaseType_t xTaskResumeFromISR(TaskHandle_t xTaskToResume);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTaskResumeFromISR, TaskHandle_t);
// BaseType_t xTaskAbortDelay(TaskHandle_t xTask);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTaskAbortDelay, TaskHandle_t);
// UBaseType_t uxTaskGetSystemState(TaskStatus_t* const pxTaskStatusArray, const UBaseType_t uxArraySize, unsigned long* const pulTotalRunTime);
DEFINE_FAKE_VALUE_FUNC(UBaseType_t, uxTaskGetSystemState, TaskStatus_t*, UBaseType_t, unsigned long*);
// void vTaskGetInfo(TaskHandle_t xTask, TaskStatus_t* pxTaskStatus, BaseType_t xGetFreeStackSpace, eTaskState eState);
DEFINE_FAKE_VOID_FUNC(vTaskGetInfo, TaskHandle_t, TaskStatus_t*, BaseType_t, eTaskState);

// semphr.h
// SemaphoreHandle_t xSemaphoreCreateBinary(void);
DEFINE_FAKE_VALUE_FUNC(SemaphoreHandle_t, xSemaphoreCreateBinary);
// SemaphoreHandle_t xSemaphoreCreateCounting(UBaseType_t uxMaxCount, UBaseType_t uxInitialCount);
DEFINE_FAKE_VALUE_FUNC(SemaphoreHandle_t, xSemaphoreCreateCounting, UBaseType_t, UBaseType_t);
// SemaphoreHandle_t xSemaphoreCreateMutex(void);
DEFINE_FAKE_VALUE_FUNC(SemaphoreHandle_t, xSemaphoreCreateMutex);
// void vSemaphoreDelete(SemaphoreHandle_t xSemaphore);
DEFINE_FAKE_VOID_FUNC(vSemaphoreDelete, SemaphoreHandle_t);
// BasetType_t xSemaphoreGive( SemaphoreHandle_t xSemaphore );
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xSemaphoreGive, SemaphoreHandle_t);
// BaseType_t xSemaphoreTake(SemaphoreHandle_t xSemaphore, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xSemaphoreTake, SemaphoreHandle_t, TickType_t);

// queue.h
// QueueHandle_t xQueueCreate( UBaseType_t uxQueueLength, UBaseType_t uxItemSize );
DEFINE_FAKE_VALUE_FUNC(QueueHandle_t, xQueueCreate, UBaseType_t, UBaseType_t);
// BaseType_t xQueueSend(QueueHandle_t xQueue, const void* pvItemToQueue, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueSend, QueueHandle_t, const void*, TickType_t);
// BaseType_t xQueueReceive(QueueHandle_t xQueue, void* pvBuffer, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueReceive, QueueHandle_t, void*, TickType_t);
// BaseType_t xQueueSendFromISR(QueueHandle_t xQueue, const void* pvItemToQueue, BaseType_t* pxHigherPriorityTaskWoken);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueSendFromISR, QueueHandle_t, const void*, BaseType_t*);
// BaseType_t xQueueSendToBack(QueueHandle_t xQueue, const void* pvItemToQueue, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueSendToBack, QueueHandle_t, const void*, TickType_t);
// BaseType_t xQueueSendToFront(QueueHandle_t xQueue, const void* pvItemToQueue, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueSendToFront, QueueHandle_t, const void*, TickType_t);
// UBaseType_t uxQueueMessagesWaiting(QueueHandle_t xQueue);
DEFINE_FAKE_VALUE_FUNC(UBaseType_t, uxQueueMessagesWaiting, QueueHandle_t);
// UBaseType_t uxQueueSpacesAvailable(QueueHandle_t xQueue);
DEFINE_FAKE_VALUE_FUNC(UBaseType_t, uxQueueSpacesAvailable, QueueHandle_t);
// BaseType_t xQueueReset(QueueHandle_t xQueue);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueReset, QueueHandle_t);
// BaseType_t xQueueOverwrite(QueueHandle_t xQueue, const void* pvItemToQueue);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueOverwrite, QueueHandle_t, const void*);
// BaseType_t xQueuePeek(QueueHandle_t xQueue, void* pvBuffer, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueuePeek, QueueHandle_t, void*, TickType_t);
// QueueSetHandle_t xQueueCreateSet(const UBaseType_t uxEventQueueLength);
DEFINE_FAKE_VALUE_FUNC(QueueSetHandle_t, xQueueCreateSet, UBaseType_t);
// BaseType_t xQueueAddToSet(QueueSetMemberHandle_t xQueueOrSemaphore, QueueSetHandle_t xQueueSet);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueAddToSet, QueueSetMemberHandle_t, QueueSetHandle_t);
// BaseType_t xQueueRemoveFromSet(QueueSetMemberHandle_t xQueueOrSemaphore, QueueSetHandle_t xQueueSet);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xQueueRemoveFromSet, QueueSetMemberHandle_t, QueueSetHandle_t);
// QueueSetMemberHandle_t xQueueSelectFromSet(QueueSetHandle_t xQueueSet, const TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(QueueSetMemberHandle_t, xQueueSelectFromSet, QueueSetHandle_t, TickType_t);

// message_buffer.h
// MessageBufferHandle_t xMessageBufferCreate(size_t xBufferSizeBytes);
DEFINE_FAKE_VALUE_FUNC(MessageBufferHandle_t, xMessageBufferCreate, size_t);
// size_t xMessageBufferSend(MessageBufferHandle_t xMessageBuffer, const void* pvTxData, size_t xDataLengthBytes, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(size_t, xMessageBufferSend, MessageBufferHandle_t, const void*, size_t, TickType_t);
// size_t xMessageBufferReceive(MessageBufferHandle_t xMessageBuffer, void* pvRxData, size_t xBufferLengthBytes, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(size_t, xMessageBufferReceive, MessageBufferHandle_t, void*, size_t, TickType_t);
// void vMessageBufferDelete(MessageBufferHandle_t xMessageBuffer);
DEFINE_FAKE_VOID_FUNC(vMessageBufferDelete, MessageBufferHandle_t);
// size_t xMessageBufferSpacesAvailable(MessageBufferHandle_t xMessageBuffer);
DEFINE_FAKE_VALUE_FUNC(size_t, xMessageBufferSpacesAvailable, MessageBufferHandle_t);
// BaseType_t xMessageBufferReset(MessageBufferHandle_t xMessageBuffer);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xMessageBufferReset, MessageBufferHandle_t);
// BaseType_t xMessageBufferIsEmpty(MessageBufferHandle_t xMessageBuffer);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xMessageBufferIsEmpty, MessageBufferHandle_t);
// BaseType_t xMessageBufferIsFull(MessageBufferHandle_t xMessageBuffer);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xMessageBufferIsFull, MessageBufferHandle_t);

// event_groups.h
// EventGroupHandle_t xEventGroupCreate(void);
DEFINE_FAKE_VALUE_FUNC(EventGroupHandle_t, xEventGroupCreate);
// void vEventGroupDelete(EventGroupHandle_t xEventGroup);
DEFINE_FAKE_VOID_FUNC(vEventGroupDelete, EventGroupHandle_t);
// EventBits_t xEventGroupWaitBits(const EventGroupHandle_t xEventGroup, const EventBits_t uxBitsToWaitFor, const BaseType_t xClearOnExit, const BaseType_t xWaitForAllBits, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(EventBits_t, xEventGroupWaitBits, EventGroupHandle_t, EventBits_t, BaseType_t, BaseType_t, TickType_t);
// EventBits_t xEventGroupSetBits(EventGroupHandle_t xEventGroup, const EventBits_t uxBitsToSet);
DEFINE_FAKE_VALUE_FUNC(EventBits_t, xEventGroupSetBits, EventGroupHandle_t, EventBits_t);
// EventBits_t xEventGroupClearBits(EventGroupHandle_t xEventGroup, const EventBits_t uxBitsToClear);
DEFINE_FAKE_VALUE_FUNC(EventBits_t, xEventGroupClearBits, EventGroupHandle_t, EventBits_t);
// EventBits_t xEventGroupGetBits(EventGroupHandle_t xEventGroup);
DEFINE_FAKE_VALUE_FUNC(EventBits_t, xEventGroupGetBits, EventGroupHandle_t);
// EventBits_t xEventGroupSync(EventGroupHandle_t xEventGroup, const EventBits_t uxBitsToSet, const EventBits_t uxBitsToWaitFor, TickType_t xTicksToWait);
DEFINE_FAKE_VALUE_FUNC(EventBits_t, xEventGroupSync, EventGroupHandle_t, EventBits_t, EventBits_t, TickType_t);

// timers.h
// TimerHandle_t xTimerCreate(const char* const pcTimerName, const TickType_t xTimerPeriod, const UBaseType_t uxAutoReload, void* const pvTimerID, TimerCallbackFunction_t pxCallbackFunction);
DEFINE_FAKE_VALUE_FUNC(TimerHandle_t, xTimerCreate, const char*, TickType_t, UBaseType_t, void*, TimerCallbackFunction_t);
// BaseType_t xTimerIsTimerActive(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerIsTimerActive, TimerHandle_t);
// BaseType_t xTimerStart(TimerHandle_t xTimer, TickType_t xBlockTime);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerStart, TimerHandle_t, TickType_t);
// BaseType_t xTimerStop(TimerHandle_t xTimer, TickType_t xBlockTime);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerStop, TimerHandle_t, TickType_t);
// BaseType_t xTimerChangePeriod(TimerHandle_t xTimer, TickType_t xNewPeriod, TickType_t xBlockTime);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerChangePeriod, TimerHandle_t, TickType_t, TickType_t);
// BaseType_t xTimerDelete(TimerHandle_t xTimer, TickType_t xBlockTime);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerDelete, TimerHandle_t, TickType_t);
// BaseType_t xTimerReset(TimerHandle_t xTimer, TickType_t xBlockTime);
DEFINE_FAKE_VALUE_FUNC(BaseType_t, xTimerReset, TimerHandle_t, TickType_t);
// void* pvTimerGetTimerID(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(void*, pvTimerGetTimerID, TimerHandle_t);
// void vTimerSetReloadMode(TimerHandle_t xTimer, const UBaseType_t uxAutoReload);
DEFINE_FAKE_VOID_FUNC(vTimerSetReloadMode, TimerHandle_t, UBaseType_t);
// void vTimerSetTimerID(TimerHandle_t xTimer, void* pvNewID);
DEFINE_FAKE_VOID_FUNC(vTimerSetTimerID, TimerHandle_t, void*);
// TaskHandle_t xTimerGetTimerDaemonTaskHandle(void);
DEFINE_FAKE_VALUE_FUNC(TaskHandle_t, xTimerGetTimerDaemonTaskHandle);
// const char* pcTimerGetName(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(const char*, pcTimerGetName, TimerHandle_t);
// TickType_t xTimerGetPeriod(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(TickType_t, xTimerGetPeriod, TimerHandle_t);
// TickType_t xTimerGetExpiryTime(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(TickType_t, xTimerGetExpiryTime, TimerHandle_t);
// UBaseType_t uxTimerGetReloadMode(TimerHandle_t xTimer);
DEFINE_FAKE_VALUE_FUNC(UBaseType_t, uxTimerGetReloadMode, TimerHandle_t);

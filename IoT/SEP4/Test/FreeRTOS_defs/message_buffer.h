#pragma once
#include "FreeRTOS.h"

typedef void* MessageBufferHandle_t;

MessageBufferHandle_t xMessageBufferCreate(size_t xBufferSizeBytes);
size_t xMessageBufferSend(MessageBufferHandle_t xMessageBuffer, const void* pvTxData, size_t xDataLengthBytes, TickType_t xTicksToWait);
size_t xMessageBufferReceive(MessageBufferHandle_t xMessageBuffer, void* pvRxData, size_t xBufferLengthBytes, TickType_t xTicksToWait);
void vMessageBufferDelete(MessageBufferHandle_t xMessageBuffer);
size_t xMessageBufferSpacesAvailable(MessageBufferHandle_t xMessageBuffer);
BaseType_t xMessageBufferReset(MessageBufferHandle_t xMessageBuffer);
BaseType_t xMessageBufferIsEmpty(MessageBufferHandle_t xMessageBuffer);
BaseType_t xMessageBufferIsFull(MessageBufferHandle_t xMessageBuffer);


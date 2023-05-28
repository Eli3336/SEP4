#pragma once
#include <stdint.h>

#define pdMS_TO_TICKS(ms) (ms)
#define portMAX_DELAY 0xFFFF
#define pdTRUE 1
#define pdFALSE 0

#define configMINIMAL_STACK_SIZE 192
#define configSTACK_DEPTH_TYPE int16_t

typedef enum { running } eTaskState;

typedef size_t TickType_t;

typedef uint8_t UBaseType_t;
typedef int8_t  BaseType_t;

typedef size_t StackType_t;
typedef size_t StaticTask_t;

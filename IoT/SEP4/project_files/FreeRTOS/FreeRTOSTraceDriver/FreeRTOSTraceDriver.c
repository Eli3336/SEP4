/*
 * FreeRTOSTraceDriver.c
 *
 * Created: 28/02/2019 11:09:13
 *  Author: IHA
 */ 
 #include <avr/io.h>
 #include <FreeRTOSConfig.h>
 #include "FreeRTOSTraceDriver.h"

 void trace_init(void)
 {
	 #if (configUseR2RTrace == 1)
	 #ifdef HAL_DEFS_H_

	 #else
		// Used on VIA MEGA Shield rev. 2.0.0
		DDRK |= _BV(DDK0) | _BV(DDK1) | _BV(DDK2) | _BV(DDK3);
	 #endif
	 #endif
 }

 #if (configUseR2RTrace == 1)
	/**********************************************************************//**
	 @ingroup trace
	 @brief Set PORTB bit 2-5, to the task switched into running by the operating system.
	
	 Called by the the traceTASK_SWITCHED_IN() macro in FreeRTOS.
	 Are enabled in FreeRTOSConfig.h
	 **********************************************************************/
	void task_switch_in(uint8_t task_no) {
		 #ifdef HAL_DEFS_H_

		 #else
		 // Used on VIA MEGA Shield rev. 2.0.0
		 PORTK &= 0b11110000;
		 PORTK |= (task_no & 0b00001111);
		 #endif
	}
	/**********************************************************************//**
	 @ingroup trace
	 @brief Set PORTB bit 2-5, to zero when a task is switched out of running by the operating system.
	
	 Called by the the traceTASK_SWITCHED_OUT() macro in FreeRTOS.
	 Are enabled in FreeRTOSConfig.h
	 **********************************************************************/
	void task_switch_out(uint8_t task_no) {
		#ifdef HAL_DEFS_H_

		#else
		// Used on VIA MEGA Shield rev. 2.0.0
		PORTK &= 0b11110000;
		#endif
	}
#endif


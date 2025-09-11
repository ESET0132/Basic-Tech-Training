#ifndef HAL_GPIO_H
#define HAL_GPIO_H

#include <avr/io.h>
#include <util/delay.h>
#include "pin_config.h"

// Function Prototypes
void GPIO_Init(void);
void LED_On(void);
void LED_Off(void);
uint8_t Button_Read(void);

#endif // HAL_GPIO_H
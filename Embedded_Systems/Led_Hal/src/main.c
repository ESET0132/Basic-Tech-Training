#include "hal/hal_gpio.h"

int main(void) {
    GPIO_Init();  

    while (1) {
        if (Button_Read()) {
            // Turn LED on
            LED_On();
            _delay_ms(50);  
            // Turn LED off
            LED_Off();
            _delay_ms(50);  
        }
    }

    return 0;
}
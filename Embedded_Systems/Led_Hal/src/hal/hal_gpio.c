#include "hal_gpio.h"

void GPIO_Init(void) {

    DDRD |= (1 << LED_PIN);

    DDRD &= ~(1 << BUTTON_PIN);

    PORTD |= (1 << BUTTON_PIN);
}

void LED_On(void) {
    PORTD |= (1 << LED_PIN);
}

void LED_Off(void) {
    PORTD &= ~(1 << LED_PIN);
}

uint8_t Button_Read(void) {
    return !(PIND & (1 << BUTTON_PIN));
}
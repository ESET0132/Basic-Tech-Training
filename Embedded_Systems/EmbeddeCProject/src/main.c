#include <avr/io.h>
#include <util/delay.h>

#define LED_PIN PD4

int main(void) {
    // Set LED_PIN as output
    DDRD |= (1 << LED_PIN);

    while (1) {
        // Turn LED on
        PORTD |= (1 << LED_PIN);
        _delay_ms(500);

        // Turn LED off
        PORTD &= ~(1 << LED_PIN);
        _delay_ms(500);
    }

    return 0;
}
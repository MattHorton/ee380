#include "horton_stm32l432.h"

unsigned char key_map[4][3] = {
	{'1', '2', '3'},//1st row
	{'4', '5', '6'},//2nd row
	{'7', '8', '9'},//3rd row
	{'*', '0', '#'},//4th row
};
void USART_Init(USART_Typedef * USARTx);
unsigned char keypad_scan();


int main(){
	RCC_AHB2ENR |= (1 << 0); //Bit 0 is GPIOA clock enable bit
	//gpio a 0 and 1 MODE are already initialized to analog
	//GPIOA_PUPDR &=~(1 << 1); //set bit 1 in ASCR to close analog switch
	RCC_AHB2ENR |= (1 << 13); //enable adc clock bit

	//----------------------------------------------------------------------
	//pwm stuff
	GPIOA_MODER |= (1 << 17);//set bit 17 to 1 16 to 0 for pwm gpio (PA8)
	GPIOA_MODER |= (1 << 16);//alternate function mode
	GPIOA_AFRH |= 0x00000001;//set to TIM1 (PWM)
	//enable clock of gpio
	//enable clock of timer 1
	//set gpio mode as alternate function
	//set the pin as pugh pull mode with no pull up pull down
	//select counting direction as up counting
	//program prescalar tim1->psc
	//program auto-reload value tim1->arr
	//select pwm mode 1 on channel 1 tim1->ccmr1
	//enable tim1_arr register preload tim1-cr1
	//enable all outputs moe bit in tim1-bdtr
	//enable complementary output of channel 1 ccn1 bit tim1-ccer
	//clear polarity bit to 0 cc1np in tim1-ccmr1
	//tim1-ccr1 = 500 duty cycle of oc1n = 50%
	//enable the counter of channel 1 cen bit in tim1-cr1

	int i;
	RCC_AHB2ENR |= (1 << 1); //Bit 1 is GPIOB clock enable bit
	GPIOB_MODER &= ~(3<<(2*3)); //clear PB3
	GPIOB_MODER |= 1<<(2*3); //PB3 output
	while(1) {
		GPIOB_BSRR |= LED_ON; //Turn on LED
		for(i=0; i<1000000; i++);
		GPIOB_BSRR |= LED_OFF; //Turn off LED
		for(i=0; i<1000000; i++);
		//for loop of 2000 is 20ms*
	}
	
	//scan operations for main
	unsigned char key;
	char str[50];
	unsigned char len = 0;
	//GPIO and clock configurations
	//LCD initializations
	//configure row pins as open-drain to prevent potential shorts
	/*...*/
	while(1) {
		key = keypad_scan();
		switch(key) {
			case '*'://if * pressed
				/*...*/
				break;
			default://add key pressed to string
				str[len] = key;
				str[len+1] = 0;//NULL string terminator
				len++;
				if(len >= 48) len = 0;//avoid buffer overflow
		}
	}
	
	
}
void USART_Init(USART_Typedef * USARTx) {
	USARTx->CR[0] = 0x00000000; //set ue (usart enable to 0)
	//USARTx->CR[0] = 
}
unsigned char keypad_scan() {
	unsigned char row, col, ColumnPressed;
	unsigned char key = 0xFF;
	
	//check whether any key has been pressed
	//1. output zeros on all row pins
	//2. delay shortly, read inputs of column pins
	//3. if inputs are 1 for all columns then no key has been pressed
	/*...*/
	if(/*no key pressed*/)
		return 0xFF;
	//identify the column of the key pressed
	for(col = 0; col < 3; col++) { //column scan
		if(/*...*/)
			ColumnPressed = col;
	}
	//identify the row of the column pressed
	for(row = 0; row < 4; row++) {
		//set up the row outputs
		/*...*/
		//read the column inputs after a short delay
		/*...*/
		//check the column inputs
		if(/*...*/)//if the input from the column pin ColumnPressed is zero
			key = key_map[row][ColumnPressed];
	}
	return key;
}
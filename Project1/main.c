#include "horton_stm32l432.h"

unsigned char key_map[4][3] = {
	{'1', '2', '3'},//1st row
	{'4', '5', '6'},//2nd row
	{'7', '8', '9'},//3rd row
	{'*', '0', '#'},//4th row
};

unsigned char code[6] = {1,2,3,4,5,6};
int current_index = 0;
uint32_t c[3];

unsigned char keypad_scan(void);
void process_key(unsigned char c);
	
#define GPIOA ((GPIO_Typedef *) 0x48000000)
#define GPIOB ((GPIO_Typedef *) 0x48000400)


int main(){
	//GPIO and clock configurations
	RCC_AHB2ENR |= (1 << 0); 	//enable GPIOA clock
	GPIOA->MODER &= ~0x3FFF; 	//clear only lower 14 bits
														//	and sets PA4,PA5,PA6 mode to input (columns)
	GPIOA->MODER |= 0x55; 		//Set PA0, PA1, PA2, PA3 mode to output(rows)
	GPIOA->OTYPER |= 0x000F;	//Set outputs type to open drain
														//everything else is no pull up pull down default
	RCC_AHB2ENR |= (1 << 1); 	//enable GPIOB clock
	GPIOB->MODER |= 0x5;
	//GPIOB->OTYPER|= 0xF;
	unsigned char key;
	unsigned char last_scanned = '\n';
	//while(1) {
	//	GPIOB->ODR |= (1 << 0);
	//	GPIOB->ODR |= (1 << 1);
	//}
	while(1) {
		key = keypad_scan();
		if((last_scanned != key) && ('f' != key)) {
			//key must be different from last key
			//key must not be idle character
			process_key(key);
			last_scanned = key;
		}
	}
}


unsigned char keypad_scan(void) {
	unsigned char row, col, ColumnPressed;
	unsigned char key = 0xFF;
	uint32_t r[4] = {0x0007, 0x000B, 0x000D, 0x000E};
	
	//note rows are bits 0 -> 3
	// columns are bits 4 -> 6
	
	//check whether any key has been pressed
	//1. output zeros on all row pins
	GPIOA->ODR = 0x000F;
	//2. delay shortly, read inputs of column pins
	uint32_t c[3] = {	GPIOA->IDR & 0x0001, 
										GPIOA->IDR & 0x0002, 
										GPIOA->IDR & 0x0004};
	//3. if inputs are 1 for all columns then no key has been pressed
	if (c[0] == c[1] == c[2] == 1)//if(no key pressed)
		return 'f'; //F for failed
	//else
	//identify the column of the key pressed
	for(col = 0; col < 3; col++) { //column scan
		if(c[col] == 0)
			ColumnPressed = col;
	}
	//identify the row of the column pressed
	for(row = 0; row < 4; row++) {
		//set up the row outputs (done in main)
		//instead of F we want to go 7(0111), B(1011), D(1101), E(1110)
		GPIOA->ODR = r[row];
		//read the column inputs after a short delay
		uint32_t c[3] = {	GPIOA->IDR & 0x0001, 
											GPIOA->IDR & 0x0002, 
											GPIOA->IDR & 0x0004};
		//check the column inputs
		if(c[ColumnPressed] == 0){//if the input from the column pin ColumnPressed is zero
			key = key_map[row][ColumnPressed];
			return key;
		}
	}
	return 'f';
}

void process_key(unsigned char c) {
	if(current_index == 6) { //6 correct digits have been entered successively
		//flash led for open
		GPIOB->ODR |= (1<<0);
		return;
	}
	if(c != code[current_index]) {
		//flash bad led
		current_index = 0;//reset index
	}
	if(c == code[current_index]) {
		current_index++;
	}
}

#include "horton_stm32l432.h"


unsigned char key_map[4][3] = {
	{'1', '2', '3'},//1st row
	{'4', '5', '6'},//2nd row
	{'7', '8', '9'},//3rd row
	{'*', '0', '#'},//4th row
	};//must set up masks for each one of these :^((((((

	
#define GPIOA ((GPIO_Typedef *) 0x48000000)

	
unsigned char keypad_scan();
	
	
int main(){
	unsigned char key;
	char str[50];
	unsigned char len = 0;
	//GPIO and clock configurations
	// columns are pulled high with no pull up pull down 3v3 and 2kohm
	RCC_AHB2ENR |= (1 << 0); //enable GPIOA clock
	GPIOA->MODER  = 0x00000033; //Set PA0, PA1, PA2, PA3 to output(rows)
															//Set PA4, PA5, PA6      to input (columns)
	GPIOA->OTYPER |= 0x000F;//Set outputs to open drain
	GPIOA->ODR = 0x000F;
	while(1)
		GPIOA->ODR |= 0x000F;
	
	/*...*/
	/*
	while(1) {
		key = keypad_scan();
		switch(key) {
			case '*'://if * pressed
				//do this for all keys ???
				break;
			default://add key pressed to string
				str[len] = key;
				str[len+1] = 0;//NULL string terminator
				len++;
				if(len >= 48) len = 0;//avoid buffer overflow
		}
	}
	*/
}



unsigned char keypad_scan() {
	unsigned char row, col, ColumnPressed;
	unsigned char key = 0xFF;
	
	//check whether any key has been pressed
	//1. output zeros on all row pins
	//2. delay shortly, read inputs of column pins
	//3. if inputs are 1 for all columns then no key has been pressed
	/*...*/
//	if(/*no key pressed*/)
		return 0xFF;
	//identify the column of the key pressed
	for(col = 0; col < 3; col++) { //column scan
//		if(/*...*/)
			ColumnPressed = col;
	}
	//identify the row of the column pressed
	for(row = 0; row < 4; row++) {
		//set up the row outputs
		/*...*/
		//read the column inputs after a short delay
		/*...*/
		//check the column inputs
//		if(/*...*/)//if the input from the column pin ColumnPressed is zero
			key = key_map[row][ColumnPressed];
	}
	return key;
}
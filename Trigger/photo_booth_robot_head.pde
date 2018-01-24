/*
Digital output goes out through two cascaded shift registers.
This includes the control outputs that runt he multiplexer (MUX)
and LCD. There are a bunch of shift register ouptuts left over for LEDs.
Digital input comes in through a multiplexer. The multiplexer output is 
conected to the arduino, but the multiplexer outputs run through the 
shift registers.
The contents of the shift registers are held in the global variable int
shiftData. Any function can put data into shiftData and have it
"output" by the shift registers by calling regCommit, which loads shiftData
into the shift registers and then latches the outputs.

Each bit in int shiftData controls one output pin on the shift registers.
The order is:
High byte
7
6
5
4
3 MUX C
2 MUX B
1 MUX A
0 
Low byte
7 LCD DB7
6 LCD DB6
5 LCD DB5
4 LCD DB4
3 LCD ENABLE
2 LCD RW
1 LCD RS
0 LED for menu state
*/

//
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~robot head defines~~~~~~~~~~~~~~~~~~~~~~~
//mask definitions for using shiftData
//high nibble is free
#define MUXC 0x0800 //multiplexer select bits A, B, C
#define MUXB 0x0400
#define MUXA 0x0200
//no def for 0x0100
#define LCD7 0x0080 //LCD data bus bit 7, 6, 5, 4 (used in 4-bit mode)
#define LCD6 0x0040
#define LCD5 0x0020
#define LCD4 0x0010
#define LCDENABLE 0x0008 //LCD enable
#define LCDRW 0x0004     //LCD read/write
#define LCDRS 0x0002     //LCD register select
#define LEDMENU 0x0001   //LED for showing menu state

//pin definitions
//knob pins
#define KNOBRED 4
#define KNOBGREEN 3
#define KNOBBLUE 2
//button pins
#define MUXY 5    //Non-inverted data from multiplexer
//shift register pins
#define REGSER 6 //serial input to shift register
#define REGRCK 7 //storage register clock (for output latches) - load on rising edge
#define REGSCK 8 //shift register clock - shift on rising edge
//multicolor LED pins
#define LEDRED 11
#define LEDGREEN 10
#define LEDBLUE 9
//LCD pin
#define LCDDB7 //Data bus 7 is also buy flag
//default LED pin
#define LED 13

//LCD command definitions
#define CLEAR  0x01 //should also put DDRAM address 0x00 in the adress counter, but may not???
#define HOME   0x02
//command definitions with toggles--add the constants together to make a full command
//for example LCDwriteCommand(ENTRY+ID+S); should set the entry mode to increment the cursor
//and shift the display. LCDwriteCommand(ENTRY); should set the entry mode to decrement and not shift.
//ENTRY
#define ENTRY  0x04 //sets cursor move direction and display shift, used with ID and S
#define ID     0x02 //Increment or decrement cursor/display shift: use to increment, omit to decrement
#define S      0x01 //Shift display: use to shift, omit to not shift
//ON_OFF
#define ON_OFF 0x08 //turn display on or off, use with D, C, and B
#define D      0x04 //use for on, omit for off
#define C      0x02 //use to show cursor as an underline, omit to not show cursor
#define B      0x01 //use to blink cursor location, omit to not blink cursor location
//SHIFT
#define SHIFT  0x10 //moves cursor or shifts display without changing the display RAM contents, use with SC and RL
#define SC     0x08 //use to shift display, omit to move cursor
#define RL     0x04 //use to go right, omit to go left
//Address setting: set an address, then write or read to it in the next command
//for example, LCDwriteCommand(DDRAM + 0x00); LCDwriteData('A'); should write an 'A' into the first position
//of the first line (DDRAM address 0x00)
#define CGRAM  0x40 //set character generator RAM address, use with 6-bit address
#define DDRAM  0x80 //set display RAM address, use with 7-bit address--see note above on invalid addresses

//button names
#define TOPLEFT 2
#define BOTLEFT 1
#define TOPRIGHT 3
#define BOTRIGHT 6
#define FARLEFT 8
#define MIDLEFT 7
#define MIDRIGHT 4
#define FARRIGHT 5


byte state;
byte holdColor; //flag to keep the LED color steady
int shiftData = 0; //holds bits for shift register output

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//setup:
void setup() {
  Serial.begin(9600);
  //LED pins
  pinMode(LED, OUTPUT);
  //Shift register pins
  pinMode(REGSER, OUTPUT);
  pinMode(REGRCK, OUTPUT);
  pinMode(REGSCK, OUTPUT);
  //analog pins and digital input pins don't need their modes set
  
  LCDInit();
  digitalWrite(LED, HIGH);
  delay(500);
  digitalWrite(LED, LOW);
  LCDprint("Hello, World.");
  eyeColor(255, 0, 20);
} //end setup()

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//main loop:
void loop() {
  char input = '\0';
  byte butt = 0;
  
  butt = pollButtons();
  if (butt == 1) {
    Serial.print('B');
    writeLine(1, "BUTTON");
    writeLine(2, "");
    eyeColor(0, 0, 255);
  }
  else {
    writeLine(1, "No Button");
    writeLine(2, "");
    eyeColor(255, 20, 0);
  }
  
  if (Serial.available()) {
    input = Serial.read();
    if (input == 'T') {
      Serial.print('T');
      writeLine(1, "TEST OK");
      writeLine(2, "");
    }
  }
} //end loop()

void eyeColor(int red, int green, int blue) {
  analogWrite(LEDRED, 255-red);
  analogWrite(LEDGREEN, 255-green);
  analogWrite(LEDBLUE, 255-blue);
}

void writeLine(byte line, char* data) {
  //writes "data" to the LCD on line "line"
  //centers the text if it is shorter than the line
  char charIn;
  byte length;
  byte padding = 0;
  //set cursor position
  if(line  == 1) LCDwriteCommand(HOME);
  else if (line == 2) LCDwriteCommand(DDRAM + 0x40);
  else return; //invalid line number
  //count characters
  length = 0;
  charIn = data[0]; //assumes a non-empty string **FIXME
  while(charIn != '\0') {
    length++;
    charIn = data[length];
  }
  //pad front end of line
  if (length < 16) padding = (16-length)/2;
  for (int i = 0; i < padding; i++) LCDwriteData(' '); //write blanks
  //print the data
  for (int i = 0; i < length; i++) LCDwriteData(data[i]);
  //pad the back end of the line
  for (int i = 0; i <= padding; i++) LCDwriteData(' '); //write blanks, one extra on the end to cover rounding
}

byte getButton() {
  //returns the number (1-8) of any button pushed. Stalls execution
  //until all buttons are released. If multiple buttons are pressed, 
  //it returns the last button released. If there is a conflict, 
  //the lower number wins.
  byte buttons, temp;
  buttons = pollButtons(); //stores the state of the 8 buttons in the bits of "buttons"
  if (buttons > 0) {  //if anything is pressed
    delay(1);   //wait for button bounce to end--no idea what right period would be
    //poll until all buttons are released, store last nonzero value in buttons
    temp = pollButtons();
    while (temp != 0) {     //while any button is still pushed
      Serial.println("stalled in getButton function");
      buttons = temp;       //store nonzero value in buttons
      temp = pollButtons(); //see if there is still a button pressed
    } //when we escape, temp is 0 and buttons is the last poll with a button still down
    //return smallest button #
    for (temp = 0; temp <= 7; temp++) {
      if ((buttons & (1<<temp)) > 0) { //if the "temp"th bit of buttons is 1
        return(temp+1);
      }
    }
  }
  return 0; //if no buttons were pressed
}

//########################### Utility Functions #############################
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Shift register functions

void regCommit() {
  //puts the contents of the global variable int shiftData on the register outputs
  byte temp;
  temp = ((shiftData>>8) & 0x00FF); //high byte
  regShiftByte(temp);
  temp = shiftData & 0x00FF; //low byte
  regShiftByte(temp);
  regLatch();
  //Serial.println("regCommit taking up time...");
  delayMicroseconds(100);
  //The LCD doesn't work without at least a 95 uS delay here (or a Serial print)
  //it's probably because LCDbusy() is disabled--should figure out where
  //the timing problem really is and put the delay in the LCD code instead
  //of here. FIXME
}

void regShiftByte(byte data) {
  //shifts one byte of data into the shift registers--does NOT set the outputs
  //bytes go out bigendian: most significant bit first
  for (byte mask = 0x80; mask > 0; mask = mask >> 1) { //shift mask until the 1 shifts out of the byte
    digitalWrite(REGSCK, LOW);
    if ((data & mask) > 0) digitalWrite(REGSER, HIGH); else digitalWrite(REGSER, LOW);
    digitalWrite(REGSCK, HIGH); //shift
  }
}

void regLatch() {
  //transfers the contents of the shift registers to the output latches
  digitalWrite(REGRCK, LOW);
  delayMicroseconds(1);
  digitalWrite(REGRCK, HIGH); //load output latches
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//MUX functions

byte pollButtons() {
  //Serial.println("pollButtons");
  //executes one poll of the buttons through a MUX (74HC151)
  byte buttons = 0;
  //poll through the MUX addresses to put current button values into byte buttons
  for (int i = 0; i <= 0; i++) { //fucked with
    if ((i & 1) == 1) shiftData = shiftData | MUXA; //set the MUXA bit to 1
      else shiftData = shiftData & (~MUXA);         //set the MUXA bit to 0
    if ((i & 2) == 2) shiftData = shiftData | MUXB; 
      else shiftData = shiftData & (~MUXB);
    if ((i & 4) == 4) shiftData = shiftData | MUXC; 
      else shiftData = shiftData & (~MUXC);
    regCommit(); //output shiftData through the shift registers
    delayMicroseconds(1);
    if (digitalRead(MUXY)==HIGH) buttons = (buttons | (1 << i));
  }
  //Serial.println("...end pollButtons");
  return buttons;
}
//%%%%%%%%%%%%%%%%%%%%%%%%%%% LCD Functions %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//high level LCD functions:

void LCDprint(char data[]) {  //start concise version without wrapping
  int index = 0;
  char temp = data[0];
  while (temp != '\0') { //assumes that data[] is a string ending with '\O' (= 0x00)
    LCDwriteData(temp);
    index++;
    temp = data[index];  
  }
}  //end concise version
 

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//LCD write functions:
void LCDwriteCommand(byte data) {
  while (LCDbusy()) {} //check busy flag until LCD is not busy
  shiftData = shiftData & (~LCDRS); //put 0s in LCDRS and LCDRW
  shiftData = shiftData & (~LCDRW);
  regCommit(); //assert the new shiftData on the registers
  LCDwrite(data);
} //end LCDwriteCommand


void LCDwriteData(byte data) {
  //Serial.println("LCDwriteData");
  while (LCDbusy()) {}
  shiftData = shiftData | LCDRS; //put 1 in LCDRS and 0 in LCDRW
  shiftData = shiftData & (~LCDRW);
  regCommit(); //assert the new shiftData on the registers
  LCDwrite(data);
  //Serial.println("...end LCDwriteData");
} //end LCDwriteData


void LCDwrite(byte data) {
  //Serial.println("LCDwrite");
  //sends an ENABLE signal and writes data to the data bus.
  //this function assumes that the calling function has just set up the RS and RW pins
  for (int nibble = 0; nibble < 2; nibble++) { //for the low and high nibble of the byte
    delayMicroseconds(1); //only 140 nS needed for RS, RW setup time
    shiftData = shiftData | LCDENABLE; //1 in LCDENABLE
    regCommit(); //LCD will accept data on the down transition of LCDENABLE
    //put data on the data pins
    //clear the bits of shiftData that correspond to the LCD data bus
    shiftData = shiftData & ~(LCD7+LCD6+LCD5+LCD4);
    //add back in the values of the high nibble of data
    //always reference LCD7-4 by name in case they change in the definitions
    if ((data & 0x80)>0) shiftData = shiftData + LCD7;
    if ((data & 0x40)>0) shiftData = shiftData + LCD6;
    if ((data & 0x20)>0) shiftData = shiftData + LCD5;
    if ((data & 0x10)>0) shiftData = shiftData + LCD4;
    regCommit(); //assert the LCD data bus on the shift registers
    delayMicroseconds(1); //only 195 nS needed for data setup time
    shiftData = (shiftData & (~LCDENABLE)); //0 in LCDENABLE
    regCommit(); //assert the low on LCD enable
    data <<= 4; //shift low nibble to high nibble
  }
  //Serial.println("...end LCDwrite");
} //end LCDwrite

//*************FIXME******************goofy LCDbusy() function
byte LCDbusy() {
  //delayMicroseconds(1);
  return 0;
}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Initialization
void LCDInit() {
  //Initialize the LCD
  delay(20); //min 15 ms needed after power-up
  shiftData = shiftData & (~LCDRS); //put 0s in LCDRS and LCDRW
  shiftData = shiftData & (~LCDRW);
  regCommit(); //assert the new shiftData on the registers
  //Send the high nibble of a function set for 8-bit data bus 3 times. Don't ask why; it's what the datasheet says.
  //  Set data pins
  shiftData = shiftData | LCD4;
  shiftData = shiftData | LCD5;
  shiftData = shiftData & (~LCD6);
  shiftData = shiftData & (~LCD7);
  regCommit();  //assert the new shiftData on the registers
  //  send the data 3 times
  for (int i = 0; i < 4; i++) { //toggle LCDENABLE 3 times
    delayMicroseconds(1); //only 140 nS needed for RS, RW setup time
    shiftData = shiftData | LCDENABLE; //1 in LCDENABLE
    regCommit(); //LCD will accept data on the down transition
    delayMicroseconds(1); //only 195 nS needed for data setup time
    shiftData = shiftData & ~LCDENABLE; //0 in LCDENABLE
    regCommit(); //LCD accepts data now
    delayMicroseconds(5); //datasheet calls for 4.1ms in first pause, 100us in second pause
  }
  //reset data pins to a function set for 4-bit data bus. (This command is still 8-bit, so only one send is needed).
  shiftData = shiftData & (~LCD4); //0 in LCD4, other pins stay as they are
  regCommit();
  shiftData = shiftData | LCDENABLE; //1 in LCDENABLE
  regCommit(); //LCD will accept data on the down transition
  delayMicroseconds(1); //only 195 nS needed for data setup time
  shiftData = shiftData & ~LCDENABLE; //0 in LCDENABLE
  regCommit(); //LCD accepts data now
  delayMicroseconds(1); //function set min delay is 37us
  //subsequent commands use the 4-bit data bus, so LCDwriteCommand can be used
  LCDwriteCommand(0x28); //function set: 4 bit interface, 2 lines, 5x7 pixel characters -- for LM044L
  delay(1); //min 37 uS delay
  LCDwriteCommand(0x08); //display off
  delay(1); //min 37 uS delay
  LCDwriteCommand(0x01); //display clear
  delay(1); //don't know if this one is needed
  LCDwriteCommand(0x06); //entry mode set: increment address, do not shift display
  delay(1); //min 37 uS delay
  //Initialization is complete
  LCDwriteCommand(0x0C); //turn on display
  delay(1);
  LCDwriteCommand(0x02); //cursor home--may be redundant
}

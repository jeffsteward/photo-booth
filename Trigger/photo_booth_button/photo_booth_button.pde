//Pins
#define PIN_STATUS_LED 13
#define PIN_PEDAL 12
#define PIN_BUTTON 2
#define PIN_RED_LED 11
#define PIN_GRN_LED 10
//Commands (serial input)
#define CMD_TEST 'T'
#define CMD_RED_ON 'R'
#define CMD_RED_OFF 'r'
#define CMD_GRN_ON 'G'
#define CMD_GRN_OFF 'g'
#define CMD_DISPLAY '"' //start and stop character for input strings
//Replies (serial output)
#define REP_OK 'K'
#define REP_TEST 'T'
#define REP_BUTTON 'B'
#define REP_PEDAL 'P'
//States
#define ST_DEFAULT 0
#define ST_DISPLAY 1 //when waiting for more serial data to display
//Errors
#define ERR_INVALID_INPUT 1
#define ERR_INPUT_TIMEOUT 2
#define ERR_INPUT_TOO_LONG 3
//Misc.
#define DEBOUNCE 300 //milliseconds of debounce
#define SERIAL_TIMEOUT 500 //timeout when waiting to finish a display command
#define DISPLAY_SIZE 10

long pedal_time, button_time, serial_time; //timers for debounce and timeout
int state;
char displayString[DISPLAY_SIZE + 1]; //+1 for the '\0' at the end
int displayStringLength; //index of the '\0' character at the end of the string

void setup() {
  pinMode(PIN_STATUS_LED, OUTPUT);
  pinMode(PIN_PEDAL, INPUT);
  pinMode(PIN_BUTTON, INPUT);
  pinMode(PIN_RED_LED, OUTPUT);
  pinMode(PIN_GRN_LED, OUTPUT);
  pedal_time = 0;
  button_time = 0;
  state = ST_DEFAULT;
  displayStringLength = 0;
  for (int i = 0; i < DISPLAY_SIZE + 1; i++) {
    displayString[i] = '\0'; //null character
  }
  Serial.begin(9600);
  //Blink reassuringly
  digitalWrite(PIN_STATUS_LED, HIGH);
  digitalWrite(PIN_RED_LED, HIGH);
  digitalWrite(PIN_GRN_LED, HIGH);
  delay(500);
  digitalWrite(PIN_STATUS_LED, LOW);
  digitalWrite(PIN_RED_LED, LOW);
  digitalWrite(PIN_GRN_LED, LOW);  
}

void loop() {
  button_task();
  serial_input_task();
}

void button_task() {
  int button_input = digitalRead(PIN_BUTTON);
  int pedal_input = digitalRead(PIN_PEDAL);

  //if button is pushed and hasn't been in the previous DEBOUNCE milliseconds, send the command
  //FIXME: what about timer roll-over from millis()?
  if (button_input == HIGH && millis()-button_time > DEBOUNCE) {
    Serial.print(REP_BUTTON);
    button_time = millis();
  }
  //same for pedal
  if (pedal_input == HIGH && millis()-pedal_time > DEBOUNCE) {
    Serial.print(REP_PEDAL);
    pedal_time = millis();
  }
}

void serial_input_task() {
  char input = '\0'; //null character
  
  switch (state) {
    case ST_DEFAULT:  
      if (Serial.available()) {
        input = Serial.read();
        switch (input) {
          case CMD_TEST:
            Serial.print(REP_TEST);
            break;
          case CMD_RED_ON:
            digitalWrite(PIN_RED_LED, HIGH);
            //Serial.print(REP_OK);
            break;
          case CMD_RED_OFF:
            digitalWrite(PIN_RED_LED, LOW);
            //Serial.print(REP_OK);
            break;
          case CMD_GRN_ON:
            digitalWrite(PIN_GRN_LED, HIGH);
            //Serial.print(REP_OK);
            break;
          case CMD_GRN_OFF:
            digitalWrite(PIN_GRN_LED, LOW);
            //Serial.print(REP_OK);
            break;
          case CMD_DISPLAY:
            state = ST_DISPLAY;      //start looking for input to display
            serial_time = millis();  //start timeout timer
            displayString[0] = '\0'; //zero the display string for new data
            displayStringLength = 0;
            break;
          default:
            reportError(ERR_INVALID_INPUT);
            break;
        }
      }
      break;
    case ST_DISPLAY:
      //check for a timeout
      if (millis() > serial_time + SERIAL_TIMEOUT) { //timeout waiting for input to end
        //FIXME: what about timer roll-over from millis()?
        displayString[0] = '\0';
        displayStringLength = 0;
        state = ST_DEFAULT;
        reportError(ERR_INPUT_TIMEOUT);
      }
      //check if a character is available
      if (Serial.available()) {
        input = Serial.read();
        //see what the character is
        switch (input) {
          case CMD_DISPLAY:    //found the end of the input
            state = ST_DEFAULT;     //return to normal operating state
            display();              //display the input
            Serial.print(REP_OK); //acknowledge
            break;
          default: //any other input
            //make sure another character can fit
            if (displayStringLength >= DISPLAY_SIZE) { //error condition: input too long
              //zero the display string
              displayString[0] = '\0';
              displayStringLength = 0;
              Serial.flush();
              state = ST_DEFAULT;
              //report the error
              reportError(ERR_INPUT_TOO_LONG);
            }
            else { //everything is OK. append the character to the display string
              displayString[displayStringLength] = input;
              displayStringLength++;
              displayString[displayStringLength] = '\0';
            }
            break;
        }
      }
      break;
    default: //unexpected state number
      digitalWrite(PIN_STATUS_LED, HIGH);
      Serial.println("ERROR: Invalid state found in serial_input_task function.");
  }
}

void reportError(int err) {
  switch (err) {
    case ERR_INVALID_INPUT:
      Serial.println("ERROR: Invalid input received.");
      break;
    case ERR_INPUT_TIMEOUT:
      Serial.println("ERROR: Timeout while waiting for display input.");
      Serial.println("Display input must end with '\"'.");
      break;
    case ERR_INPUT_TOO_LONG:
      Serial.print("ERROR: Display input is too long. Max length is ");
      Serial.println(DISPLAY_SIZE);
      break;
    default:
      Serial.println("ERROR: unknown error code.");
      break;
  }
}

void display() {
  Serial.print("DISPLAYING: "); Serial.println(displayString);
}

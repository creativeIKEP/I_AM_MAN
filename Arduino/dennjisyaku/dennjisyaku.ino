int pin1 = 9;
int pin2 =10;

char data[3]="200";
int flag=0;
int flag2=100;
int data2=0;

void setup() {
  pinMode(pin1, OUTPUT);
  pinMode(pin2, OUTPUT);

  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);

  Serial.begin(9600);
}

void loop() {

   if(Serial.available()>0){
    char inbyte=Serial.read();
    InData(inbyte);
    Serial.println(data);
  }



  
  //rotate forward
  digitalWrite(pin2, LOW);
  /*
  for(int i=0; i<=255; i++){
    analogWrite(pin1, i);
    delay(10);
  }
  for(int i=255; i>=0; i--){
    analogWrite(pin1, i);
    delay(10);
  }
  */

  if(strcmp(data, "175")>0 && strcmp(data,"200")<=0){
    analogWrite(pin1, 159);
    delay(10);
  }
  
  if(strcmp(data, "150")>0 && strcmp(data,"175")<=0){
    analogWrite(pin1, 171);
    delay(10);
  }

  if(strcmp(data, "125")>0 && strcmp(data,"150")<=0){
    analogWrite(pin1, 183);
    delay(10);
  }

  if(strcmp(data, "100")>0 && strcmp(data,"125")<=0){
    analogWrite(pin1, 195);
    delay(10);
  }

  if(strcmp(data, "075")>0 && strcmp(data,"100")<=0){
    analogWrite(pin1, 207);
    delay(10);
  }

  if(strcmp(data, "050")>0 && strcmp(data,"075")<=0){
    analogWrite(pin1, 219);
    delay(10);
  }

  if(strcmp(data, "025")>0 && strcmp(data,"050")<=0){
    analogWrite(pin1, 231);
    delay(10);
  }

  if(strcmp(data, "000")>0 && strcmp(data,"025")<=0){
    analogWrite(pin1, 243);
    delay(10);
  }

  if(strcmp(data, "000")==0){
    analogWrite(pin1, 255);
    delay(10);
  }
/*
  //rotate reverse
  digitalWrite(pin1, LOW);
  for(int i=0; i<=255; i++){
    analogWrite(pin2, i);
    delay(10);
  }
  for(int i=255; i>=0; i--){
    analogWrite(pin2, i);
    delay(10);
  }
  */
}





void InData(char rec){
  if(rec=='0' || rec=='1' || rec=='2' || rec=='3' || rec=='4' || rec=='5' || rec=='6' || rec=='7' || rec=='8' || rec=='9'){
    data[flag]=rec;
    flag++;
    if(flag>2){flag=0;}
  }
}


void CharToInt(char c[]){
  for(int i=0; i<3; i++){
    switch(c[i]){
      case '0':
        data2+=0;
      case '1':
        data2+=flag2;
      case '2':
        data2+=flag2*2;
      case '3':
        data2+=flag2*2;
      case '4':
        data2+=flag2*2;
      case '5':
        data2+=flag2*2;
      case '6':
        data2+=flag2*2;
      case '7':
        data2+=flag2*2;
      case '8':
        data2+=flag2*2;
      case '9':
        data2+=flag2*2;
    }
  }
  
}







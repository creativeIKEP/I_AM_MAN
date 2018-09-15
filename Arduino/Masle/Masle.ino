int switchPin2 = 7;
int switchPin1 = 8;

//追加
char data[3]="200";
int flag=0;
int flag2=100;
int data2=0;

////////////////////

void setup() 
{
  pinMode(switchPin1, OUTPUT);
  pinMode(switchPin2, OUTPUT);

  digitalWrite(switchPin1, HIGH);
  digitalWrite(switchPin2, LOW);

  //////////////////////
  Serial.begin(9600);
}

void loop() 
{
  /*digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);*/



  if(Serial.available()>0){
    char inbyte=Serial.read();
    if(inbyte=='A'){BatteryUP();}
    else{
      InData(inbyte);
      if(flag>2){
        Serial.println(data);
        BatteryDown();
        flag=0;
      }
    }
  }
}

void BatteryUP(){
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, HIGH);
    delay(2000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
}

void BatteryDown(){
  if(strcmp(data, "200")==0)
  {
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, HIGH);
    delay(10000);
  }
  if(strcmp(data, "170")>0 && strcmp(data, "190")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(3000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "150")>0 && strcmp(data, "170")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(3000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "130")>0 && strcmp(data, "150")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(3000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "110")>0 && strcmp(data, "130")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "090")>0 && strcmp(data, "110")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "075")>0 && strcmp(data, "090")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000); 
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "060")>0 && strcmp(data, "075")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
if(strcmp(data, "045")>0 && strcmp(data, "060")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "035")>0 && strcmp(data, "045")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "025")>0 && strcmp(data, "035")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(4000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "015")>0 && strcmp(data, "025")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(5000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "010")>0 && strcmp(data, "015")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(5000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "005")>0 && strcmp(data, "010")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(5000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
  
  if(strcmp(data, "000")>0 && strcmp(data, "005")<=0)
  {
    digitalWrite(switchPin1, LOW);
    digitalWrite(switchPin2, HIGH);
    delay(5000);
    digitalWrite(switchPin1, HIGH);
    digitalWrite(switchPin2, LOW);
  }
}


void InData(char rec){
  if(rec=='0' || rec=='1' || rec=='2' || rec=='3' || rec=='4' || rec=='5' || rec=='6' || rec=='7' || rec=='8' || rec=='9'){
  data[flag]=rec;
  
  flag++;
  
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

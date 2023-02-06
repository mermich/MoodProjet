export class Mood {
  id:number=0;
  moodFaceId:number = 0;
  date :Date = new Date(Date.now());
  moodDeviceId : number = 0;

  moodFace : MoodFace = new  MoodFace();
  device : Device = new  Device();
}


export class MoodFace {
  id:number=0;
  key: string = "";
  label: string = "";
  isActive:boolean = true;
  picture: string = "";
}


export class Device
{
  id:number=0;
  label: string = "";
  isActive: boolean = true;
}
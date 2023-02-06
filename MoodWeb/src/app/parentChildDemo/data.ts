export class Data
{
  constructor(public id: number, public label: string) { }
}

export class BigData
{
  datas:Data[] = [
    new Data(1,'test1'), 
    new Data(2,'test2'), 
    new Data(3,'test3'), 
    new Data(4,'test4'),
    new Data(5,'test5'),
  ]
}
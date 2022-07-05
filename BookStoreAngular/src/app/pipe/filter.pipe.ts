import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, filterString:string): any {
    if(!value || !filterString){
      return value;
  }

  const dataArray:any[] = [];
  for(let book of value){
      if(book.bookName.toLocaleLowerCase().includes(filterString) || book.author.toLocaleLowerCase().includes(filterString || book.description.toLocaleLowerCase().includes(filterString) || book.author.toLocaleLowerCase().includes(filterString)  || book.discountPrice.toString().includes(filterString))) {
        dataArray.push(book);
      }
  }
  return dataArray;

}

}

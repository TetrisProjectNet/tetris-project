import { JsonPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { concat } from 'rxjs';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {
  transform(data: any[] | null, phrase: string, key: string = ''): any {
    if (!Array.isArray(data) || !phrase) {
      // console.log(phrase);
      return data;
    }

    phrase = (phrase + '').toLowerCase();

    if (!key) {
      return data.filter(item => {
        const objectvalues: string = `${Object.values(item).reduce((acc, curr) => Array.isArray(curr) ? acc : `${acc} ${curr}`,'')}`;
        // console.log(objectvalues.toLowerCase())
      return objectvalues.toLowerCase().includes(phrase)})
    } else {
      return data.filter((item) => {
        const strItem: string = (item[key] + '').toLowerCase();
        console.log(strItem.includes(phrase));
        return strItem.includes(phrase);
      });
    }
    // if (!key) {
    //   return data.forEach(item => {
    //     console.log(Object.values(item));
    //     return Object.values(item).includes(phrase);

    //     // Object.keys(item).filter(function(key) {
    //     //   return item[key] === phrase;
    //     // });
    //   })

    // return data.filter(item => {
    //   const values: any[] = Object.values(item);
    //   let strItem: string = '';
    //   values.map(i => {
    //     strItem = (i + '').toLowerCase();
    //     console.log(strItem.includes(phrase));
    //     // if (typeof(i) !== 'object') {

    //     // } else {
    //     //   console.log('lse');
    //     //   return false;
    //     // }
    //   });
    //   return strItem.includes(phrase);

    //   // console.log(strItem);
    //   // return data;
    // })
  }

  //   return data.filter(item => {
  //     const strItem: string = (item[key] + '').toLowerCase();
  //     console.log(strItem.includes(phrase));
  //     return strItem.includes(phrase);
  //   })
  // }
}

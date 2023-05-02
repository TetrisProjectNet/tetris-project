import { JsonPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { concat } from 'rxjs';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {
  transform(data: any[] | null, phrase: string, key: string = ''): any {
    
    if (!Array.isArray(data) || !phrase) {
      return data;
    }

    phrase = (phrase + '').toLowerCase();

    if (!key) {
      return data.filter(item => {
        const objectvalues: string = `${Object.values(item).reduce((acc, curr) => Array.isArray(curr) ? acc : `${acc} ${curr}`,'')}`;
      return objectvalues.toLowerCase().includes(phrase)})
    } else {
      return data.filter((item) => {
        const strItem: string = (item[key] + '').toLowerCase();
        return strItem.includes(phrase);
      });
    }
  }

}

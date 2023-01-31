import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sorter',
})
export class SorterPipe implements PipeTransform {

  transform(value: any[] | null, column: string, direct: boolean, length?: number): any[] | null {
    if (!Array.isArray(value) || !column) {
      return value;
    }

    if (column === 'scores' && length) {
      value.map((item) => {
        item[column] = item[column].sort((a: any, b: any) => {
          b - a;
        })[0];
      });
      return value.sort((a: any, b: any) => {
        // equal items sort equally
        if (a[column] === b[column]) {
          return 0;
        }

        // nulls sort after anything else
        if (a[column] === undefined) {
          return 1;
        }
        if (b[column] === undefined) {
          return -1;
        }

        return b[column] - a[column];
      })
      .slice(0, length);
    }

    return value.sort((a: any, b: any) => {
      if (typeof a[column] === 'number' && typeof b[column] === 'number') {
        // ascending
        if (direct) {
          return a[column] - b[column];
        }
        // descanding
        return b[column] - a[column];
      } else {
        const aA: string = ('' + a[column]).toLowerCase();
        const bB: string = ('' + b[column]).toLowerCase();
        if (direct) {
          return aA.localeCompare(bB);
        }
        return bB.localeCompare(aA);
      }
    });
  }
}

// // nulls sort after anything else
// if (a[column] === null) {
//   return 1;
// }
// if (b[column] === null) {
//   return -1;
// }

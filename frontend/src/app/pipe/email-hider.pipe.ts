import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'emailHider'
})
export class EmailHiderPipe implements PipeTransform {

  transform(value: string): string {
    const emailParts = value.split('@');
    return emailParts[0].slice(0, 3) + '***@' + emailParts[1];
  }

}

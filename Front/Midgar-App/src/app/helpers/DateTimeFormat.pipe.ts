import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/constants';

@Pipe({
  name: 'DateTimeFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {
  transform(value: any, args?: any): any {
    if (value) {
      let date: Date;

      if (typeof value === 'string' && value.includes('/')) {
        const parts = value.split(' ');
        const dateParts = parts[0].split('/');
        const timeParts = parts[1]?.split(':') || ['00', '00'];

        date = new Date(
          +dateParts[2],           // ano
          +dateParts[1] - 1,       // mês (zero-based)
          +dateParts[0],           // dia
          +timeParts[0],           // hora
          +timeParts[1]            // minuto
        );
      } else {
        date = new Date(value);
      }

      return super.transform(date, Constants.DATE_TIME_FMT);
    }
    return null;
  }
}
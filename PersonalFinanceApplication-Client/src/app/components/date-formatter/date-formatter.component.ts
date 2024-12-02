import { DatePipe } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-date-formatter',
  templateUrl: './date-formatter.component.html',
  styleUrls: ['./date-formatter.component.css'],
})
export class DateFormatterComponent {
  @Input() rawDate!: string | null;
  formattedDate!: string | null;

  constructor(private datePipe: DatePipe) {}

  ngOnChanges() {
    this.formattedDate = this.datePipe.transform(this.rawDate, 'longDate');
  }
}

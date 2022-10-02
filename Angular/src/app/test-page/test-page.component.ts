import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxCsvParser } from 'ngx-csv-parser';
import { NgxCSVParserError } from 'ngx-csv-parser';

import { PriceHistoryLine } from '../models/item/price';

@Component({
  selector: 'app-test-page',
  templateUrl: './test-page.component.html',
  styleUrls: ['./test-page.component.css']
})
export class TestPageComponent implements OnInit {
  csvRecords: any[] = [];
  header: boolean = false;
  prices: PriceHistoryLine[] = [];

  constructor(private ngxCsvParser: NgxCsvParser) { }

  @ViewChild('fileImportInput') fileImportInput: any;

  ngOnInit(): void {
  }

  fileChangeListener($event: any): void {

    const files = $event.srcElement.files;
    this.header = (this.header as unknown as string) === 'true' || this.header === true;

    this.ngxCsvParser.parse(files[0], { header: this.header, delimiter: ',' })
      .pipe().subscribe((result: any) => {
        this.prices = [];
        this.csvRecords = result;
        this.csvRecords.forEach( (row: any[] )=> {
          var pricesline = new PriceHistoryLine(); 
          pricesline.PriceHisLItemInternalCode = row[0];
          pricesline.PriceHisLItemInternalCode = row[1];
          pricesline.PriceHisLItemInternalCode = row[2];
          pricesline.PriceHisLItemInternalCode = row[3];
          this.prices.push(pricesline);
        });
        console.log(JSON.stringify(this.prices));
      }, (error: NgxCSVParserError) => {
        console.log('Error', error);
      });
  }
}

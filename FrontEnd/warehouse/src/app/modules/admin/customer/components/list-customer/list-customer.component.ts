import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { CustomerModel } from '../../models/customer.model';
import { ISortEvent } from 'src/app/core/interfaces/sort-event.interface';

@Component({
  selector: 'app-list-customer',
  templateUrl: './list-customer.component.html',
  styleUrls: ['./list-customer.component.css']
})
export class ListCustomerComponent implements OnInit {

  list: CustomerModel[] = [];
  tableColumn: any[];

  constructor(private carService: CustomerService) { }

  ngOnInit() {


    this.tableColumn = [
        { field: 'vin', header: 'Vin' },
        {field: 'year', header: 'Year' },
        { field: 'brand', header: 'Brand' },
        { field: 'color', header: 'Color' }
    ];
  }

  filter(text: string) {
    if (text) {
      window.alert(text);
    }
  }

  sort(event: ISortEvent) {
    if (event) {
      console.log(event);
    }
  }

  pageChange(page: any) {
    if (page) {
      console.log(page);
      window.alert(page);
    }
  }

  getData(): CustomerModel[] {

    return null;
  }

}

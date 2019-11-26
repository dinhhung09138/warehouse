import { Component, OnInit } from '@angular/core';
import { Car } from '../models/Car';
import { DataTableService } from '../services/datatable.service';
import { ISortEvent } from 'src/app/core/interfaces/sort-event.interface';

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.css']
})
export class DataTableComponent implements OnInit {

  cars: Car[];

  cols: any[];

    constructor(private carService: DataTableService) { }

    ngOnInit() {
      this.cars = this.getData();

      this.cols = [
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

    getData(): Car[] {
      let data: Car[] = [];

      data.push(new Car('Vin 1', 5019, 'Brand 1', 'Color 1', 10050, new Date()));
      data.push(new Car('Vin Click 2', 7019, 'Brand 2', 'Color 2', 23000, new Date()));
      data.push(new Car('Vin Custom 3', 9011, 'Brand 3', 'Color 3', 30500, new Date()));
      data.push(new Car('Vin OK 4', 1012, 'Brand 4', 'Color 4', 50000, new Date()));
      data.push(new Car('Vin done 5', 2013, 'Brand 5', 'Color 5', 50060, new Date()));
      data.push(new Car('Vin Tema 6', 2319, 'Brand 6', 'Color 6', 60000, new Date()));
      data.push(new Car('Vin 1', 2015, 'Brand 1', 'Color 1', 10500, new Date()));
      data.push(new Car('Vin 2', 2019, 'Brand 2', 'Color 2', 20000, new Date()));
      data.push(new Car('Vin 3', 2019, 'Brand Cali 3', 'Color Cali 3', 36000, new Date()));
      data.push(new Car('Vin Mos 4', 2016, 'Brand 4', 'Color 4', 40000, new Date()));
      data.push(new Car('Vin 5', 2018, 'Brand 5', 'Color 5', 50070, new Date()));
      data.push(new Car('Vin 6', 2015, 'Brand 6', 'Color 6', 60700, new Date()));
      data.push(new Car('Vin Notes 1', 2049, 'Brand 1', 'Color 1', 101000, new Date()));
      data.push(new Car('Vin 2', 25019, 'Brand New 2', 'Color New 2', 20000, new Date()));
      data.push(new Car('Vin Todo 3', 2019, 'Brand 3', 'Color 3', 60000, new Date()));
      data.push(new Car('Vin Note 4', 2019, 'Brand 4', 'Color 4', 45700, new Date()));
      data.push(new Car('Vin Comment 5', 9019, 'Brand Custom 5', 'Color Custom 5', 50500, new Date()));
      data.push(new Car('Vin 1', 2019, 'Brand 6', 'Color 6', 658000, new Date()));

      return data;
    }

}

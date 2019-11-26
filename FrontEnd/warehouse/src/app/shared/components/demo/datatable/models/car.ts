export class Car {
  vin: string;
  year: number;
  brand: string;
  color: string;
  price: number;
  saleDate: Date;

  constructor(vin: string, year: number, brand: string, color: string, price: number, date: Date) {
    this.vin = vin;
    this.year = year;
    this.brand = brand;
    this.color = color;
    this.price = price;
    this.saleDate = date;
  }
}

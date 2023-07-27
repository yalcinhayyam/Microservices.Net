import { Component } from '@angular/core';
import { Service } from './service';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  // products$ = Observable<IProductViewModel[]>;
  products: IProductViewModel[] | undefined;

  constructor(private httpClient: HttpClient, public Service: Service) {
    this.getProducts();
  }

  increase(value: number) {
    this.Service.setsetValue(value)
    
  }

  getProducts() {
    this.httpClient
      .get<IProductViewModel[]>('http://localhost:5259/products')
      .subscribe((products) => (this.products = products));
  }
}



interface IProductViewModel {
  id: string;
  title: string;
  prices: [
    {
      amount: number;
      currency: string;
    }
  ];
  stock: {
    amount: number;
    type: string;
  };
}

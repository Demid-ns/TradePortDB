import {Component, OnInit} from '@angular/core';
import {Product} from '../shared/models/product';
import {ProductService} from '../shared/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  public products: Product[];

  constructor(private productService: ProductService) {
  }

  ngOnInit(): void {
    this.productService.getProducts()
      .subscribe(response => {
        this.products = response;
      });
  }
}


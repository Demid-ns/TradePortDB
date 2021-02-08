import {Component, Inject, OnInit} from '@angular/core';
import {Product} from '../shared/models/product';
import {ProductService} from '../shared/services/product.service';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  public products: Product[];
  displayedColumns: string[] = ['id', 'name', 'buttons'];
  public loading = true;

  constructor(private productService: ProductService,
              public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.productService.getProducts()
      .subscribe(response => {
        this.loading = false;
        this.products = response;
      });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ProductsDialogComponent,
      {
        width: '300px',
        data: this.products
      }
    );

    dialogRef.afterClosed().subscribe(() => {
      this.products = this.products.filter(p => p.name);
    });
  }

  remove(id: number): void {
    this.productService.removeProduct(id).subscribe(response => {
      console.log(response);
    });
    this.products = this.products.filter(p => p.id !== id);
  }
}

@Component({
  selector: 'app-products-dialog',
  templateUrl: './products-dialog.component.html',
  styleUrls: ['./products-dialog.component.scss']
})
export class ProductsDialogComponent {
  form: FormGroup;
  error = '';

  constructor(
    public dialogRef: MatDialogRef<ProductsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public products: Product[],
    private productService: ProductService) {
    this.form = new FormGroup({
      name: new FormControl(null, [Validators.minLength(3), Validators.required])
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  submit(): void {
    if (this.form.invalid) {
      return;
    }

    const product: Product = {
      name: this.form.value.name
    };

    this.productService.addProduct(product).subscribe(response => {
      this.products.push(response);
      this.dialogRef.close();
    }, () => {
      this.error = 'Добавление невозможно';
    });
  }
}


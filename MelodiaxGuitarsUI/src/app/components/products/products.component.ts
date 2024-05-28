import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { BrandService } from '../../services/brand/brand.service';
import { CategoryService } from '../../services/category/category.service';
import { Brand } from '../../models/Brand';
import { Category } from '../../models/Category';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    MatButtonModule,
    MatTableModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    MatSelectModule,
    MatFormFieldModule,
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  constructor(
    private brandService:BrandService, 
    private categoryService:CategoryService,
    private productService:ProductService
    ){}
  brands:Brand[] = [];
  categories:Category[] = [];
  dataSource: Product[] = [];
  addProductFlag: boolean = false;
  editProductId:string | null = null;

  displayedColumns: string[] = ['id', 'name', 'description', 'brandId', 'model', 'type',
    'hand', 'bodyShape', 'color', 'top', 'sidesAndBack', 'neck', 'nut', 'fingerboard', 'strings',
    'tuners', 'bridge', 'controls', 'pickups', 'pickupSwitch', 'cutaway', 'pickguard', 'case',
    'scaleLength', 'width', 'depth', 'weight', 'categoryId', 'imageUrl', 'price', 'actions']

  form: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    brandId: new FormControl(''), 
    model: new FormControl(''),
    type: new FormControl(''),
    hand: new FormControl(''),
    bodyShape: new FormControl(''),
    color: new FormControl(''),
    top: new FormControl(''),
    sidesAndBack: new FormControl(''),
    neck: new FormControl(''),
    nut: new FormControl(''),
    fingerboard: new FormControl(''),
    strings: new FormControl(''),
    tuners: new FormControl(''),
    bridge: new FormControl(''),
    controls: new FormControl(''),
    pickups: new FormControl(''),
    pickupSwitch: new FormControl(''),
    cutaway: new FormControl(''), 
    pickguard: new FormControl(''),
    case: new FormControl(''),
    scaleLength: new FormControl(''),
    width: new FormControl(''),
    depth: new FormControl(''),
    weight: new FormControl(''),
    categoryId: new FormControl(''),
    imageUrl: new FormControl(''),
    price: new FormControl('')
  })

  ngOnInit(): void {
    this.loadProducts();

    this.brandService.getBrands().subscribe(
      (brands) => {
        this.brands = brands;
      }
    )

    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
      }
    )
  }

  loadProducts():void{
    this.productService.getProducts().subscribe({
      next: (products) => {
        this.dataSource = products;
      },
      // error: (err) => {
      //   console.error('Error getting products: ', err)
      // }
    })
  }

  showAddForm(){
    this.addProductFlag = true;
  }

  addProduct(){
    if(this.form == null){
      return;
    }

    const formData = this.form.value;
    this.productService.addProduct(formData).subscribe({
      next: () => {
        this.addProductFlag = false;
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  cancelAdd(){
    this.addProductFlag = false;
  }

  editProduct(id:string){
    this.editProductId = id;
  }

  cancelEdit():void{
    this.editProductId = null;
  }

  deleteProduct(id:string){
    if(confirm('Are you sure you want to delete this product?')){
      this.productService.deleteProduct(id).subscribe(
        () => {
          this.dataSource = this.dataSource.filter(product => product.id !== id);
        },
        (error) => {
          console.error('Error deleting product: ', error);
        }
      )
    }
  }

  saveProductChanges():void{
    if(this.form == null || this.editProductId == null){
      return;
    }

    const formData = this.form.value;
    this.productService.updateProduct(this.editProductId, formData).subscribe(
      () => {
        const index = this.dataSource.findIndex(product => product.id === this.editProductId);
        if(index !== -1){
          this.dataSource[index] = formData;
        }

        this.loadProducts();
        this.editProductId = null;
      },
      (err) => {
        console.error('Error updating brand:', err);
      }
    )
  }
}

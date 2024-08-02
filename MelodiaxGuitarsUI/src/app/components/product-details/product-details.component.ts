import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/Product';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent {
  constructor(private route:ActivatedRoute, private productService:ProductService){}

  product !: Product;
  specifications: {label: string, value: string | boolean}[] = []; 

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if(productId){
      this.productService.getProductById(productId).subscribe((product) => {
        this.product = product;
        this.setSpecifications(product);
      })
    } 
  }

  setSpecifications(product:Product): void{
    const specs = [
     {label: "Model", value: product.model},
      {label: "Type", value: product.type},
      {label: "Hand", value: product.hand},
      {label: "Body Shape", value: product.bodyShape},
      {label: "Color", value: product.color},
      {label: "Top", value: product.top},
      {label: "Sides and Back", value: product.sidesAndBack},
      {label: "Neck", value: product.neck},
      {label: "Nut", value: product.nut},
      {label: "Fingerboard", value: product.fingerboard},
      {label: "Strings", value: product.strings},
      {label: "Tuners", value: product.tuners},
      {label: "Bridge", value: product.bridge},
      {label: "Controls", value: product.controls},
      {label: "Pickups", value: product.pickups},
      {label: "Pickup Switch", value: product.pickupSwitch},
      {label: "Cutaway", value: product.cutaway ? "Yes" : "No"},
      {label: "Pickguard", value: product.pickguard},
      {label: "Case", value: product.case},
      {label: "Scale Length", value: product.scaleLength},
      {label: "Width", value: product.width},
      {label: "Depth", value: product.depth},
      {label: "Weight", value: product.weight}
    ]

    this.specifications = specs.filter(spec => spec.value !== null && spec.value !== '');
  }
}

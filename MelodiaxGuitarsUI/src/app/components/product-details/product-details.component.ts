import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product/product.service';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent {
  constructor(private route:ActivatedRoute, private productService:ProductService){}

  product !: Product;

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if(productId){
      this.productService.getProductById(productId).subscribe((product) => {
        this.product = product;
      })
    } else {
      console.error("Product ID is null");
    }
      
  }
}

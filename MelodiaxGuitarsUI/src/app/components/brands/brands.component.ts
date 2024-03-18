import { Component, OnInit } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { Brand } from '../../models/Brand';
import { BrandService } from '../../services/brand/brand.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-brands',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
  ],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.scss'
})
export class BrandsComponent implements OnInit{
  constructor(private brandService:BrandService){}

  displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  dataSource: Brand[] = [];

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands():void{
    this.brandService.getBrands().subscribe(
      (brands:Brand[]) => {
        this.dataSource = brands;
      },
      (error) => {
        console.error('Error fetching brands: ', error);
      }
    )
  }

  editBrand(brand:Brand){

  }

  deleteBrand(id:string): void{
    if(confirm('Are you sure you want to delete this brand?')){
      this.brandService.deleteBrand(id).subscribe(
        ()=>{
          this.dataSource = this.dataSource.filter(brand => brand.id !== id);
        },
        (error) => {
          console.error('Error deleting brand: ', error);
        }
      )
    }
  }
}

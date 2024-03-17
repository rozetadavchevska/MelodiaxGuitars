import { Component, OnInit } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { Brand } from '../../models/Brand';
import { BrandService } from '../../services/brand/brand.service';

@Component({
  selector: 'app-brands',
  standalone: true,
  imports: [
    MatTableModule
  ],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.scss'
})
export class BrandsComponent implements OnInit{
  constructor(private brandService:BrandService){}

  displayedColumns: string[] = ['name', 'description'];
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
}

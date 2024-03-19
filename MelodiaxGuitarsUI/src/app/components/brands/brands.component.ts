import { Component, OnInit } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { Brand } from '../../models/Brand';
import { BrandService } from '../../services/brand/brand.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { HttpClientModule } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-brands',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatLabel,
    MatFormField,
    MatFormFieldModule,
    HttpClientModule,
    MatInputModule,
  ],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.scss'
})
export class BrandsComponent implements OnInit{
  constructor(private brandService:BrandService){}

  displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  dataSource: Brand[] = [];
  editingBrandId:string | null = null;
  showAddFormFlag: boolean = false;

  form: FormGroup = new FormGroup ({
    name: new FormControl('', [Validators.required]),
    description: new FormControl(''),
  })

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands():void{
    this.brandService.getBrands().subscribe(
      (brands) => {
        this.dataSource = brands;
      },
      // (error) => {
      //   console.error('Error fetching brands: ', error);
      // }
    )
  }

  showAddForm() {
    this.showAddFormFlag = true;
  }

  addBrand() {
    if (this.form == null) {
      return;
    }

    const formData = this.form.value;
    this.brandService.addBrand(formData).subscribe({
      next: () => {
        this.loadBrands();
        this.showAddFormFlag = false;
      },
      error: err => {
        console.error('Error adding brand ', err);
      }
    })
  }

  cancelAdd() {
    this.showAddFormFlag = false;
  }

  editBrand(id:string){
    this.editingBrandId = id;
  }

  cancelEdit():void{
    this.editingBrandId = null;
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

  saveBrandChanges():void{
    if (this.form == null || this.editingBrandId == null) {
      return;
    }

    const formData = this.form.value;
    this.brandService.updateBrand(this.editingBrandId, formData).subscribe(
      () => {
        const index = this.dataSource.findIndex(brand => brand.id === this.editingBrandId);
        if (index !== -1) {
          this.dataSource[index] = formData;
        }

        this.loadBrands();
        this.editingBrandId = null;
      },
      (err) => {
        console.error('Error updating brand:', err);
      }
    );
  }
}

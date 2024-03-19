import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { Category } from '../../models/Category';
import { CategoryService } from '../../services/category/category.service';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [
    HttpClientModule,
    MatTableModule,
    MatButtonModule,
    MatLabel,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    MatIconModule
  ],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent implements OnInit{
  constructor(private categoryService:CategoryService){}

  displayedColumns: string[] = ['id', 'name', 'description', 'actions'];
  dataSource: Category[] = [];
  showAddFormFlag: boolean = false;
  editingCategoryId: string | null = null;

  form: FormGroup = new FormGroup ({
    name: new FormControl('', [Validators.required]),
    description: new FormControl(''),
  })

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories():void{
    this.categoryService.getCategories().subscribe(
      (categories) => {
        this.dataSource = categories;
      },
    )
  }

  showAddForm(){
    this.showAddFormFlag = true;
  }

  addCategory(){
    if(this.form == null){
      return;
    }

    const formData = this.form.value;
    this.categoryService.addCategory(formData).subscribe({
      next: () => {
        this.loadCategories();
        this.showAddFormFlag = false;
      },
      error: err => {
        console.error('Error adding category ', err);
      }
    })
  }

  cancelAdd(){
    this.showAddFormFlag = false;
  }

  editCategory(id:string){
    this.editingCategoryId = id;
  }

  cancelEdit(){
    this.editingCategoryId = null;
  }
  
  deleteCategory(id:string){
    if(confirm('Are you sure you want to delete this category?')){
      this.categoryService.deleteCategory(id).subscribe(
        () => {
          this.dataSource = this.dataSource.filter(brand => brand.id !== id);
        },
        (error) => {
          console.error('Error deleting category: ', error);
        }
      )
    }
  }

  saveCategoryChanges():void{
    if(this.editingCategoryId == null || this.form == null){
      return;
    }

    const formData = this.form.value;
    this.categoryService.updateCategory(this.editingCategoryId,formData).subscribe(
      () => {
        const index = this.dataSource.findIndex(category => category.id == this.editingCategoryId);
        if(index !== -1){
          this.dataSource[index] = formData;
        }

        this.loadCategories();
        this.editingCategoryId = null;
      },
      (error) => {
        console.error('Error updateing category: ', error);
      } 
    )
  }
}

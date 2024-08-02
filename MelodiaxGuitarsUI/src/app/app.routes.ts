import { Routes } from '@angular/router';
import { AcousticGuitarsComponent } from './components/acoustic-guitars/acoustic-guitars.component';
import { ElectricGuitarsComponent } from './components/electric-guitars/electric-guitars.component';
import { BassGuitarsComponent } from './components/bass-guitars/bass-guitars.component';
import { AmpsComponent } from './components/amps/amps.component';
import { PedalsComponent } from './components/pedals/pedals.component';
import { AccessoriesComponent } from './components/accessories/accessories.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { BrandsComponent } from './components/brands/brands.component';
import { ProductsComponent } from './components/products/products.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';

export const routes: Routes = [
    {path: 'acoustic', component: AcousticGuitarsComponent},
    {path: 'electric', component: ElectricGuitarsComponent},
    {path: 'bass', component: BassGuitarsComponent},
    {path: 'amps', component: AmpsComponent},
    {path: 'pedals', component: PedalsComponent},
    {path: 'accessories', component: AccessoriesComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'login', component: LoginComponent},
    {path: 'cart', component: ShoppingCartComponent},
    {path: 'brands', component: BrandsComponent},
    {path: 'products', component:ProductsComponent},
    {path: 'categories', component: CategoriesComponent},
    {path: 'product/:id', component: ProductDetailsComponent} 
];

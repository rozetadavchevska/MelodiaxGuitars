import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/shared/navbar/navbar.component";
import { AcousticGuitarsComponent } from './components/acoustic-guitars/acoustic-guitars.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthGuard } from './guards/auth.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthService } from './services/auth/auth.service';
import { AdminNavbarComponent } from './components/shared/adminNavbar/admin-navbar/admin-navbar.component';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [
      RouterOutlet, 
      RouterModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      CommonModule,
      NavbarComponent, 
      AcousticGuitarsComponent,
      AdminNavbarComponent
    ],
    providers: [
      AuthGuard,
      {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi:true}
    ],
})
export class AppComponent implements OnInit{
  title = 'MelodiaxGuitarsUI';

  protected isAdmin: boolean = false;

  constructor(private authService:AuthService){}

  ngOnInit(){
    this.isAdmin = this.authService.isAdmin();
  }
}

import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../../services/auth/auth.service';

@Component({
  selector: 'app-admin-navbar',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    CommonModule,
    RouterModule,
    MatButtonModule,
  ],
  templateUrl: './admin-navbar.component.html',
  styleUrl: './admin-navbar.component.scss'
})
export class AdminNavbarComponent {
  constructor(protected authService: AuthService, private router:Router){}

  isMenuActive:boolean = false;

  toggleMenu(){
    this.isMenuActive = !this.isMenuActive;
  }

  logout(){
    this.authService.removeToken();
    this.router.navigate(['/']).then(() => {
      window.location.reload();
    });
  }
}

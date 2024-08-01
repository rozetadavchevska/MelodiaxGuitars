import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge'; 
import { CommonModule } from '@angular/common';
import { AcousticGuitarsComponent } from '../../acoustic-guitars/acoustic-guitars.component';
import { Router, RouterModule} from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    MatToolbarModule, 
    MatIconModule, 
    MatBadgeModule, 
    CommonModule,
    AcousticGuitarsComponent,
    RouterModule,
    MatButtonModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  constructor(protected authService:AuthService,private router:Router){}

  isMenuActive = false;

  toggleMenu() {
    this.isMenuActive = !this.isMenuActive;
  }

  logout(){
    this.authService.removeToken();
    this.router.navigate(['/']).then(() => {
      window.location.reload();
    });
  }
}

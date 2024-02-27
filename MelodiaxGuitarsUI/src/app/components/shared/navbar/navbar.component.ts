import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge'; 
import { CommonModule } from '@angular/common';
import { AcousticGuitarsComponent } from '../../acoustic-guitars/acoustic-guitars.component';
import { RouterModule} from '@angular/router';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    MatToolbarModule, 
    MatIconModule, 
    MatBadgeModule, 
    CommonModule,
    AcousticGuitarsComponent,
    RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent {
  isMenuActive = false;

  toggleMenu() {
    this.isMenuActive = !this.isMenuActive;
  }
}

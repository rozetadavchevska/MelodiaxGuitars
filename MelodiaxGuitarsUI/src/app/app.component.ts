import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/shared/navbar/navbar.component";
import { AcousticGuitarsComponent } from './components/acoustic-guitars/acoustic-guitars.component';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [
      RouterOutlet, 
      RouterModule,
      NavbarComponent, 
      AcousticGuitarsComponent
    ]
})
export class AppComponent {
  title = 'MelodiaxGuitarsUI';
}

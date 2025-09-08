import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  // Não precisamos mais da propriedade isMenuCollapsed
  // pois o Bootstrap nativo cuida do colapso automaticamente
}

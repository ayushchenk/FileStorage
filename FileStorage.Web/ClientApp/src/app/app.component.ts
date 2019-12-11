import { Component, HostListener } from '@angular/core';
import { TokenManager } from 'src/app/modules/account/service/token-manager';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [TokenManager]
})
export class AppComponent {
  title = 'ClientApp';

  constructor(
    private tokenManager: TokenManager,
    private router: Router,
  ) { }

  get isAuthenticated() {
    return this.tokenManager.isAuthenticated();
  }

  get isAdmin() {
    if (this.tokenManager.get() == null)
      return false;
    return this.tokenManager.get().isAdmin;
  }

  logout() {
    this.tokenManager.remove();
    this.router.navigate(['/login']);
  }
}

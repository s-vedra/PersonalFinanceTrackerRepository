import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-component',
  templateUrl: './nav-component.component.html',
  styleUrls: ['./nav-component.component.css'],
})
export class NavComponentComponent {
  isSidebarActive = false;
  logout() {
    console.log('Logout clicked');
    // Implement your logout logic here
  }
  toggleSidebar() {
    this.isSidebarActive = !this.isSidebarActive;
  }
}

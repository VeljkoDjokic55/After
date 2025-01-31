import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss']
})
export class SidenavComponent implements OnInit {

  constructor(
    private authService: AuthService, 
    private router: Router) {
  }

  ngOnInit(): void {
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['']);
  }
}

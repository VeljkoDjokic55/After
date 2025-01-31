import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from 'src/app/config/config';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public email: any;
  public pass: any;
  public showPw: boolean = false;


  constructor(private authService: AuthService,
    // private userService: UserService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    if(this.authService.isLoggedIn())
      this.router.navigate(['dashboard']);
  }

  login() {
    this.authService.login(this.email, this.pass).subscribe(x => {
      if (x.status === "OK" || x.status === "200" ){
        localStorage.setItem('token', x.data); 
        this.router.navigate(['dashboard']);
      }      
      else 
        this.toastr.error(x.message);      
    }, error => {
      this.toastr.error('An error ocurred.');
    });
  }

  showPassword() {
    this.showPw = !this.showPw;
  }
}

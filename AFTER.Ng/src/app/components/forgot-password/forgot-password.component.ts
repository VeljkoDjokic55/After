import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from 'src/app/config/config';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  public resetMessageSent: boolean = false;
  public showEmailForm: boolean = true;
  public showCodeForm: boolean = false;
  public showPasswordForm: boolean = false;

  //Form input
  public email: any;
  public code:any;
  public pw: any;
  public confirmPw: any;
  public showPw: boolean = false;
  public showConfirmPw: boolean = false;
  private resCode:any;

  constructor(private authService: AuthService,
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    if(this.authService.isLoggedIn())
      this.router.navigate(['dashboard']);
  }

  resetPasswrod():void {
    this.userService.forgotPasswordRequest(this.email).subscribe(res => {
      if (res.status === 200 || res.status === 'OK') {
        //On success
        this.resCode = res.data;
        this.showEmailForm = false;
        this.resetMessageSent = true;
        this.showCodeForm = true;
    } else {
      this.toastr.error(res.message);      
    }}, error => {
      this.toastr.error('An error occurred.');
    });
  }

  checkCode():void {
    if(this.code === this.resCode) {
      //On success
      this.showCodeForm = false;
      this.showPasswordForm = true;
    } else {
      this.toastr.error('Code is not valid!');
    }

  }

  submitNewPw():void {
    if (this.pw?.length < 8) {
      this.toastr.error("Password must be at least 8 characters");
      return;
    }
    if (this.pw?.search(/[a-z]/i) < 0) {
      this.toastr.error("Password must contain at least one letter.");
      return;
    }
    if (this.pw?.search(/[0-9]/) < 0) {
      this.toastr.error("Password must contain at least one digit.");
      return;
    }
    if (this.pw?.search(/[$&+,:;=?@#|'<>.^*()%!-]/) < 0) {
      this.toastr.error("Password must contain at least one special character ($&+,:;=?@#|'<>.^*()%!-).");
      return;
    }
    if (this.pw != this.confirmPw) {
      this.toastr.error("Passwords must match");
      return;
    }
    
    let userId = this.email.split("@")[0];
    if (this.pw.toLowerCase().includes(userId.toLowerCase())) {
      this.toastr.error("Your password must not contain your email.");
      return;
    }

    let obj = {
      password: this.pw,
      email: this.email,
      code: this.code
    }

    this.userService.resetPassword(obj).subscribe(x => {
      if (x?.status == 200 || x?.status == "OK") {
        this.toastr.success('Password changed successfully.')
        setTimeout(() => {
          this.router.navigate(['/']);
        }, 1000);
      }
    }, error => {
      this.toastr.error(error?.error?.title);
    });
  }

  closeModal() {
    this.resetMessageSent = false;
  }
  showPassword() {
    this.showPw = !this.showPw;    
  }
  showConfirmPassword() {
    this.showConfirmPw = !this.showConfirmPw;    
  }
}

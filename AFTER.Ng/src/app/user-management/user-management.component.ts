import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { ToastrService } from 'ngx-toastr';
import { User } from '../models/user.model';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  public currentPage: number = 1;
  public pageSize: number = 11;
  public count: number = 0;
  public searchText: string = "";

  public users: User[] | any = [];
  

  //User info
  public addNewUser:boolean = false;
  public showPw: boolean = false;

  public userId:number = 0;
  public name: string = '';
  public lastName: string = '';
  public email: string = '';
  public role: string | any = '';
  public pass: string = '';
	
	constructor(
    private userService: UserService,
    private toastr: ToastrService
  ) { }
	
	ngOnInit() {
		this.getAllUsers();
	}


  options = [
    { name: "Admin", value: 0 },
    { name: "MobileUser", value: 1 }
  ]

  selectOption(e:any) {
    let roleStr = this.options.find(option => option.value == e.target.value);
    this.role = roleStr?.name;
  }

  // User
  getAllUsers() {
    let obj = {
      pageInfo:{
        page: this.currentPage,
        pageSize: this.pageSize
      },
      filterParams: {
        "searchParam": this.searchText
      }
    };
    this.userService.getAll(obj).subscribe(response => {
      this.users = response.data.data;
      this.count = response.data.count;
    }, error => {
      this.toastr.error('An error occurred.');
    });
  }
  onSearch() {
    this.getAllUsers();
  }

  onEdit(user:any) {
    this.userId = user.id;
    this.name = user.firstName;
    this.lastName =  user.lastName;
    this.email = user.email;
    this.role = user.role;

    this.addNewUser = true;
  }

  onSetStatus(id:number, status:string) {
    let newStatus = '';
    if(status.toLowerCase() === "active") {
      newStatus = "Inactive"
    } else {
      newStatus = "Active"
    }
    let obj = {
      "id": id,
      "status": newStatus
    };

    this.userService.setStatus(obj).subscribe(x => {
      if (x.status === "200" || x.status === "OK" ){
        this.toastr.success('Status changed successfully.');
        this.reloadTable();
      }      
      else 
        this.toastr.error(x.message); 
    }, error => {
      this.toastr.error('An error occurred.');
    });
  }
  
  reloadTable() {
    //Here you can change page

    //Get All users
    this.getAllUsers();
  };

  onSave() {
    
  };

  onAddNewUser() {
    this.addNewUser = true;
  };

  onRoleAccess() {
    this.toastr.info('To be implemented');
  };
 
  onCancel() {
    this.addNewUser = false;
    this.initialState();
  };

  showPassword() {
    this.showPw = !this.showPw;
  };
 
  handleFormSubmit() {
    if(this.userId === 0) {
      this.checkPassword();
    }


    let obj = {
      "id": this.userId,
      "firstName": this.name,
      "lastName": this.lastName,
      "email": this.email,
      "password": this.pass,
      "role": this.role,
      "status": "Active",
    };

    this.userService.saveUser(obj).subscribe(x => {
      if (x.status === "200" || x.status === "OK" ){
        this.toastr.success('User saved successfully');
        this.addNewUser = false;
        this.reloadTable();
        this.initialState();
      }      
      else 
        this.toastr.error(x.message); 

    }, error => {
      this.toastr.error('An error occurred.');
    });
  };

  //Checkboxes
  checkAllCheckBox(e: any) {
    this.users.forEach((product:any) => product.checked = e.target.checked);
	}
	isAllCheckBoxChecked() {
		return this.users.every((p:any) => p.checked);
	}


  initialState() {
    this.userId = 0;
    this.name = '';
    this.lastName = '';
    this.email= '';
    this.role = '';
    this.pass= '';
  }

  checkPassword() {
    if (this.pass?.length < 8) {
      this.toastr.error("Password must be at least 8 characters");
      return;
    }
    if (this.pass?.search(/[a-z]/i) < 0) {
      this.toastr.error("Password must contain at least one letter.");
      return;
    }
    if (this.pass?.search(/[0-9]/) < 0) {
      this.toastr.error("Password must contain at least one digit.");
      return;
    }
    if (this.pass?.search(/[$&+,:;=?@#|'<>.^*()%!-]/) < 0) {
      this.toastr.error("Password must contain at least one special character ($&+,:;=?@#|'<>.^*()%!-).");
      return;
    }
    if (this.pass.toLowerCase().includes(this.email.toLowerCase())) {
      this.toastr.error("Password must not contain user email.");
      return;
    }
  }
}

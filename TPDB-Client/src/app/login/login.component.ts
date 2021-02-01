import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../shared/interfaces/interfaces';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  private email = '';
  private password = '';

  public get isLoggedIn(): boolean {
    return this.auth.isAuthenticated();
  }

  constructor(public dialog: MatDialog, private router: Router, private auth: AuthService) {
  }

  ngOnInit(): void {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent,
      {
        width: '300px',
        data: {login: this.email, password: this.password}
      }
    );

    dialogRef.afterClosed().subscribe((result) => {
    });
  }

  public logout(): void {
    this.auth.logout();
  }
}

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss']
})
export class LoginDialogComponent {
  form: FormGroup;
  error = '';

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User,
    private auth: AuthService) {
    this.form = new FormGroup({
      email: new FormControl(null, [Validators.email, Validators.required]),
      password: new FormControl(null, [Validators.minLength(4), Validators.required])
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  submit(): void {
    if (this.form.invalid) {
      return;
    }

    const data: User = {
      email: this.form.value.email,
      password: this.form.value.password,
      returnJWTToken: false
    };

    this.auth.login(data).subscribe(response => this.dialogRef.close(),
      () => {
        this.error = 'Неправильный email или пароль';
      });
  }
}



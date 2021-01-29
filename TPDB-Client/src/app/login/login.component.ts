import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from '@angular/material/dialog';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {User} from '../shared/interfaces/user';
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

  constructor(public dialog: MatDialog, private auth: AuthService, private router: Router) {
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
      if (result != null) {
        this.auth.login(result).subscribe(() => {
          this.router.navigate(['/', '']);
        });
      }
    });
  }
}

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss']
})
export class LoginDialogComponent {
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: User) {
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
      password: this.form.value.password
    };

    this.dialogRef.close(data);
  }
}



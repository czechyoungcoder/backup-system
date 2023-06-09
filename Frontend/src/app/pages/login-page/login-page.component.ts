import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, FormArray } from '@angular/forms';
import { SessionsService } from '../../services/sessions.service';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
})
export class LoginPageComponent implements OnInit {
  public form: FormGroup;
  public error: boolean = false;

  public constructor(
    private fb: FormBuilder,
    private router: Router,
    private sessions: SessionsService
  ) {}

  public ngOnInit(): void {
    this.form = this.fb.group({
      credentials: this.fb.group({
        username: '',
        passwordHash: '',
      }),
      savePassword: false,
    });
  }

  public login(): void {
    const { credentials, savePassword } = this.form.value;
    this.sessions
      .login(credentials, savePassword)
      .pipe(
        catchError(() => {
          this.error = true;
          return of(false);
        })
      )
      .subscribe((result) => {
        if (result) {
          this.router.navigate(['']);
        }
      });
  }
}

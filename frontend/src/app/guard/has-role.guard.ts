import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, filter, map } from 'rxjs';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class HasRoleGuard implements CanActivate {

  loggedUser$ = this.authService.loggedUser$;

  constructor(
    private authService: AuthService,
    private router: Router,

  ) { }

  canActivate(): Observable<boolean> {
    return this.loggedUser$.pipe(
      filter((loggedUser) => loggedUser !== undefined),
      map((loggedUser) => {
        if (!loggedUser || loggedUser.role != 'admin') {
          this.router.navigateByUrl('/home');
          return false;
        }
        return true;
      })
    )
  }

}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent } from './page/forgot-password/forgot-password.component';
import { HomeComponent } from './page/home/home/home.component';
import { LoginComponent } from './page/login/login.component';
import { NewPasswordComponent } from './page/new-password/new-password.component';
import { ShopItemEditorComponent } from './page/shop-item-editor/shop-item-editor.component';
import { ShopComponent } from './page/shop/shop.component';
import { SignupComponent } from './page/signup/signup.component';
import { StatisticsComponent } from './page/statistics/statistics.component';
import { UserEditorComponent } from './page/user-editor/user-editor.component';
import { UserComponent } from './page/user/user.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent,
  },
  {
    path: 'new-password',
    component: NewPasswordComponent,
  },
  {
    path: 'shop',
    component: ShopComponent,
  },
  {
    path: 'shop/:id',
    component: ShopItemEditorComponent,
  },
  {
    path: 'statistics',
    component: StatisticsComponent,
  },
  {
    path: 'user',
    component: UserComponent,
  },
  {
    path: 'user/:id',
    component: UserEditorComponent,
  },
  {
    path: '**',
    redirectTo: 'home'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './page/home/home/home.component';
import { LoginComponent } from './page/login/login.component';
import { ShopComponent } from './page/shop/shop.component';
import { SignupComponent } from './page/signup/signup.component';
import { StatisticsComponent } from './page/statistics/statistics.component';

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
    path: 'shop',
    component: ShopComponent,
  },
  {
    path: 'statistics',
    component: StatisticsComponent,
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

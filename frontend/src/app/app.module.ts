import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './page/home/home/home.component';
import { NavbarComponent } from './common/nav/navbar/navbar.component';
import { FooterComponent } from './common/footer/footer.component';
import { SignupComponent } from './page/signup/signup.component';
import { LoginComponent } from './page/login/login.component';
import { SidebarComponent } from './common/nav/sidebar/sidebar.component';
import { AnimatedCounterComponent } from './page/home/animated-counter/animated-counter.component';

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { ShopComponent } from './page/shop/shop.component';
import { StatisticsComponent } from './page/statistics/statistics.component';
import { UserComponent } from './page/user/user.component';
import { UserEditorComponent } from './page/user-editor/user-editor.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './page/forgot-password/forgot-password.component';
import { NewPasswordComponent } from './page/new-password/new-password.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { ShopItemEditorComponent } from './page/shop-item-editor/shop-item-editor.component';
import { VerificationComponent } from './page/verification/verification.component';
import { NgOtpInputModule } from 'ng-otp-input';
import { EmailHiderPipe } from './pipe/email-hider.pipe';
import { AnimateOnScrollModule } from 'ng2-animate-on-scroll';
import { FilterPipe } from './pipe/filter.pipe';
import { SorterPipe } from './pipe/sorter.pipe';
import { NgChartsModule } from 'ng2-charts';
import { FloatingLabelInputComponent } from './common/floating-label-input/floating-label-input.component';
import { NgxPopperjsModule } from 'ngx-popperjs';
import { PrivacyPolicyComponent } from './page/privacy-policy/privacy-policy.component';
import { ProfileDropdownComponent } from './common/nav/profile-dropdown/profile-dropdown.component';
import { ToastrModule } from 'ngx-toastr';
import { ToastrComponent } from './common/toastr/toastr.component';
import { ParallaxDirective } from './common/directive/parallax.directive';
import { AuthInterceptor } from './service/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    FooterComponent,
    SignupComponent,
    LoginComponent,
    SidebarComponent,
    AnimatedCounterComponent,
    ShopComponent,
    StatisticsComponent,
    UserComponent,
    UserEditorComponent,
    ForgotPasswordComponent,
    NewPasswordComponent,
    ShopItemEditorComponent,
    VerificationComponent,
    EmailHiderPipe,
    FilterPipe,
    SorterPipe,
    FloatingLabelInputComponent,
    PrivacyPolicyComponent,
    ProfileDropdownComponent,
    ToastrComponent,
    ParallaxDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    NgCircleProgressModule.forRoot({
      "radius": 60,
      "space": -10,
      "outerStrokeGradient": true,
      "outerStrokeWidth": 10,
      "outerStrokeColor": "#4882c2",
      "outerStrokeGradientStopColor": "#53a9ff",
      "innerStrokeColor": "#eee",
      "innerStrokeWidth": 10,
      "title": "UI",
      "animateTitle": true,
      "animationDuration": 1000,
      "showUnits": false,
      "showBackground": false,
      "clockwise": false,
      "startFromZero": false,
      "lazy": true}),
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    NgOtpInputModule,
    AnimateOnScrollModule.forRoot(),
    NgChartsModule,
    NgxPopperjsModule,
    ToastrModule.forRoot({
      toastComponent: ToastrComponent,
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

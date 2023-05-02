import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Email } from 'src/app/model/email';
import { Verification } from 'src/app/model/verification';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { EmailService } from 'src/app/service/email.service';
import { UserService } from 'src/app/service/user.service';
import { VerificationService } from 'src/app/service/verification.service';
import EmailTemplate from "src/assets/email.template"

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {

  email: string = '';

  validGuideMessage: string = 'Please enter an existing email address!';

  toastRef: any;

  constructor(
    private verificationService: VerificationService,
    private emailService: EmailService,
    private userService: UserService,
    private router: Router,
    private toastr: CustomToastrService,
  ) { }

  ngOnInit(): void {
  }

  getRandomInt(max: number) {
    return Math.floor(Math.random() * max);
  }

  generateCode(length: number) {
    let code = '';

    for(let i = 0; i < length; i++) {
      code += this.getRandomInt(10);
    }

    return code;
  }

  sendEmail(obj: any) {
    this.emailService.create(obj)
    .subscribe({
      error: () => this.onDanger('We could not send you an email.<br>Please try again later!', 'Something went wrong.'),
      complete: () => {
        this.onSuccess('It will be live for 30 mins.', 'Code sent.');
        this.router.navigate(['/verification'], {state: {data: obj.to}});
      }
    });
  }

  onSubmit(email: string): void {
    this.userService.isEmailRegistered(email)
    .subscribe({
      next: res => {
        if (res) {
          const code = this.generateCode(6);
          const verificationObj = new Verification();
          verificationObj.email = email;
          verificationObj.code = code;

          this.verificationService.getBasedOnEmail(email)
          .subscribe({
            next: data => {
              if (data) {
                verificationObj.id = data.id;
                this.verificationService.update(verificationObj)
                .subscribe({
                  error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
                  complete: () => {
                    const emailObj = new Email();
                    emailObj.to = email;
                    emailObj.subject = `[tetris-project] Please verify your identity! - ${new Date().toLocaleString()}`;
                    emailObj.body = EmailTemplate.generateTemplateWithCode(code);

                    this.sendEmail(emailObj);
                  }
                });

              } else {
                this.verificationService.create(verificationObj)
                .subscribe({
                  error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
                  complete: () => {
                    const emailObj = new Email();
                    emailObj.to = email;
                    emailObj.subject = `[tetris-project] Please verify your identity! - ${new Date().toLocaleString()}`;
                    emailObj.body = EmailTemplate.generateTemplateWithCode(code);

                    this.sendEmail(emailObj);
                  }
                });
              }

            },
          })
          
        } else {
          this.onDanger(`This email is not registered yet.<br>Please <a href="/signup" [routerLink]="['/signup']">Sign up!</a>`)
        }
      }
    });

  }

  getValidationData($event: any) {
    console.log('foo', $event);
  }

  onSuccess(message: string, title: string = 'Success!') {
    this.toastRef = this.toastr.showSuccessToast(title, message);
  }

  onDanger(message: string, title: string = 'Error!') {
    this.toastRef = this.toastr.showDangerToast(title, message);
  }

  onWarning(message: string, title: string = 'Warning!') {
    this.toastRef = this.toastr.showWarningToast(title, message);
  }

  onInfo(message: string, title: string = 'Info!') {
    this.toastRef = this.toastr.showInfoToast(title, message);
  }

  onRemoveToast() {
    this.toastr.removeToast(this.toastRef);
  }

}

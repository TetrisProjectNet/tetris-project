import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Email } from 'src/app/model/email';
import { Verification } from 'src/app/model/verification';
import { CustomToastrService } from 'src/app/service/custom-toastr.service';
import { EmailService } from 'src/app/service/email.service';
import { UserService } from 'src/app/service/user.service';
import { VerificationService } from 'src/app/service/verification.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {

  email: string = '';

  validGuideMessage: string = 'Please enter an existing email address!';

  toastRef: any;

  // emailClass: string = '';
  // selectedElement: any;

  constructor(
    private verificationService: VerificationService,
    private emailService: EmailService,
    private userService: UserService,
    private router: Router,
    private toastr: CustomToastrService,
  ) { }

  ngOnInit(): void {
  }

  // focusToggler(event: Event, className: string): string {
  //   event.type == 'focus' ? className= 'focused' : className='';

  //   if(event) {
  //     this.selectedElement = event.target;
  //   } else {
  //     this.selectedElement = null;
  //   }

  //   if (this.selectedElement.value != '') {
  //     className= 'focused';
  //   }
  //   return className;
  // }

  // emailFocusToggler(event: Event): void {
  //   this.emailClass = this.focusToggler(event, this.emailClass)
  // }

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

  // checkExistence(email: string) {
  //   let result: any = [];
  //   this.verificationService.getAll()
  //   .subscribe({
  //     next: data => {
  //       console.log(data);
  //       data.map(obj => {
  //         if (obj.email == email) {
  //           console.log('asd');
  //           result.push(obj);
  //         }
  //       })
  //       // return data;
  //       // if (this.otpValue == data.code) {
  //       //   this.router.navigate(['/new-password']);
  //       // } else {
  //       //   this.onDanger('We could not verify your code.');
  //       // }
  //     },
  //     error: () => this.onDanger('We could not check your code.<br>Please try again later!', 'Something went wrong.'),
  //     // complete: () => {
  //     //   this.router.navigate(['shop']);
  //     //   this.onSuccess('Shop item updated.');
  //     // }
  //   });
  //   console.log('result: ', result);
  //   return result;
  // }

  // checkExistence(email: string) {
  //   let result: any = [];
  //   this.verificationService.getBasedOnEmail(email)
  //   .subscribe({
  //     next: data => {
  //       console.log(data);
  //       // data.map(obj => {
  //       //   if (obj.email == email) {
  //       //     console.log('asd');
  //       //     result.push(obj);
  //       //   }
  //       // })
  //       // return data;
  //       // if (this.otpValue == data.code) {
  //       //   this.router.navigate(['/new-password']);
  //       // } else {
  //       //   this.onDanger('We could not verify your code.');
  //       // }
  //     },
  //     error: () => this.onDanger('We could not check your code.<br>Please try again later!', 'Something went wrong.'),
  //     // complete: () => {
  //     //   this.router.navigate(['shop']);
  //     //   this.onSuccess('Shop item updated.');
  //     // }
  //   });
  //   console.log('result: ', result);
  //   return result;

  // }

  onSubmit(email: string): void {
    this.userService.isEmailRegistered(email)
    .subscribe({
      next: res => {
        // console.log('isEmailRegistered', res);
        if (res) {
          const code = this.generateCode(6);
          const verificationObj = new Verification();
          verificationObj.email = email;
          verificationObj.code = code;
          console.log(verificationObj);

          this.verificationService.getBasedOnEmail(email)
          .subscribe({
            next: data => {
              if (data) {
                // console.log('asdasdasd', data);
                verificationObj.id = data.id;
                this.verificationService.update(verificationObj)
                .subscribe({
                  error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
                  complete: () => {
                    const emailObj = new Email();
                    emailObj.to = email;
                    emailObj.subject = `[tetris-project] Please verify your identity! - ${new Date().toLocaleString()}`;
                    emailObj.body = `This email is sent from full stack api. <br>Your code is: <h1 style="color: aqua;">${code}</h1>`;
                    console.log(emailObj);

                    this.sendEmail(emailObj);

                  }
                });

              } else {
                // console.log('checkExistence', data)

                this.verificationService.create(verificationObj)
                .subscribe({
                  error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
                  complete: () => {
                    const emailObj = new Email();
                    emailObj.to = email;
                    emailObj.subject = `[tetris-project] Please verify your identity! - ${new Date().toLocaleString()}`;
                    emailObj.body = `This email is sent from full stack api. <br>Your code is: <h1 style="color: aqua;">${code}</h1>`;
                    console.log(emailObj);

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




    // console.log('checkExistence', this.verificationService.getBasedOnEmail(email));



    // let result;
    // this.checkExistence(email).subscribe({
    //   next: (data: any) => {
    //     console.log('data', data);

    //   }
    // });


    // nagyklevi@gmail.com

    // let result: any = [];
    // this.verificationService.getAll()
    // .subscribe({
    //   next: data => {
    //     console.log('data', data);

    //     if (data.length > 0) {

    //     } else {

    //     }

    //     data.map(obj => {
    //       console.log('obj', obj);
    //       if (obj.email == email) {

    //         // result.push(obj);
    //       } else {
    //         this.verificationService.create(verificationObj)
    //         .subscribe({
    //           error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
    //           complete: () => {
    //             const emailObj = new Email();
    //             emailObj.to = email;
    //             emailObj.subject = `[tetris-project] Please verify your identity!`;
    //             emailObj.body = `This email is sent from full stack api. <br>Your code is: <h1 style="color: aqua;">${code}</h1>`;
    //             console.log(emailObj);

    //             // this.sendEmail(emailObj);

    //           }
    //         });
    //       }
    //     })
    //     // return data;
    //     // if (this.otpValue == data.code) {
    //     //   this.router.navigate(['/new-password']);
    //     // } else {
    //     //   this.onDanger('We could not verify your code.');
    //     // }
    //   },
    //   error: () => this.onDanger('We could not check your code.<br>Please try again later!', 'Something went wrong.'),
    //   // complete: () => {
    //   //   this.router.navigate(['shop']);
    //   //   this.onSuccess('Shop item updated.');
    //   // }
    // });

    // console.log('result: ', result);


    // if (this.checkExistence(email).length < 1) {
    //   this.verificationService.create(verificationObj)
    //   .subscribe({
    //     error: () => this.onDanger('We could not send your code to this address.<br>Please try again later!', 'Something went wrong.'),
    //     complete: () => {
    //       const emailObj = new Email();
    //       emailObj.to = email;
    //       emailObj.subject = `[tetris-project] Please verify your identity!`;
    //       emailObj.body = `This email is sent from full stack api. <br>Your code is: <h1 style="color: aqua;">${code}</h1>`;
    //       console.log(emailObj);

    //       // this.sendEmail(emailObj);

    //     }
    //   });
    // }



  }

  getValidationData($event: any) {
    console.log('$event', $event);
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

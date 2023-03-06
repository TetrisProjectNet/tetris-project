import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(
    private toastr: ToastrService
  ) { }

  showSuccessToast=(toastRef: any, title: string, message: string)=>{
    toastRef = this.toastr.show(message, title,{
      disableTimeOut: false,
      tapToDismiss: false,
      toastClass: "custom-toast-success",
      closeButton: true,
      progressBar: true,
      positionClass:'toast-top-right',
      timeOut: 5000,
      extendedTimeOut: 2000
    });
  }

  showDangerToast=(toastRef: any, title: string, message: string)=>{
    toastRef = this.toastr.show(message, title,{
      disableTimeOut: false,
      tapToDismiss: false,
      toastClass: "custom-toast-danger",
      closeButton: true,
      progressBar: true,
      positionClass:'toast-top-right',
      timeOut: 5000,
      extendedTimeOut: 2000,
      enableHtml: true,
    });
  }

  showWarningToast=(toastRef: any, title: string, message: string)=>{
    toastRef = this.toastr.show(message, title,{
      disableTimeOut: false,
      tapToDismiss: false,
      toastClass: "custom-toast-warning",
      closeButton: true,
      progressBar: true,
      positionClass:'toast-top-right',
      timeOut: 5000,
      extendedTimeOut: 2000
    });
  }

  showInfoToast=(toastRef: any, title: string, message: string)=>{
    toastRef = this.toastr.show(message, title,{
      disableTimeOut: false,
      tapToDismiss: false,
      toastClass: "custom-toast-info",
      closeButton: true,
      progressBar: true,
      positionClass:'toast-top-right',
      timeOut: 5000,
      extendedTimeOut: 2000
    });
  }

  removeToast = (toastRef: any) =>{
    this.toastr.clear(toastRef.ToastId);
  }

}

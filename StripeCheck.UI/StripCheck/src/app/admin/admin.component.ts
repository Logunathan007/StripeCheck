import { CommonService } from './../services/common.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html'
})
export class AdminComponent {
  firstName = ''
  lastName = ''
  email = ''
  referral = ''
  notificationMessage = {
    enable:false,
    msg:"",
    colorClass:""
  }


  constructor(private service:CommonService) { }

  onboard(){
    this.service.OnboardServiceProvider(this.firstName,this.lastName,this.email,this.referral).subscribe(
      (obj:any)=>{
        this.notificationMessage.msg = obj.msg;
        this.notificationMessage.colorClass = (obj.status=='Success')?"bg-success":"bg-danger";
        this.notificationMessage.enable = true;
        setTimeout(()=>{
          this.notificationMessage.enable = false;
          this.notificationMessage.msg =""
          this.notificationMessage.colorClass = "";
        },3000)
      },
      (error: any) => {
        this.notificationMessage.msg = "400 "+error.message;
        this.notificationMessage.colorClass = "bg-danger";
        this.notificationMessage.enable = true;
        setTimeout(() => {
          this.notificationMessage.enable = false;
          this.notificationMessage.msg = "";
          this.notificationMessage.colorClass = "";
        }, 3000);
      }
    );
  }

}

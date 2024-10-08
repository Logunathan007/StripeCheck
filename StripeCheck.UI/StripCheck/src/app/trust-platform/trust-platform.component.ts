import { Component, OnInit } from '@angular/core';
import { CommonService } from '../services/common.service';

@Component({
  selector: 'app-trust-platform',
  templateUrl: './trust-platform.component.html'
})
export class TrustPlatformComponent implements OnInit {
  accountList:string[]=[];
  selectedAccount = ""
  amount = 0;
  notificationMessage = {
    enable:false,
    msg:"",
    colorClass:""
  }

  constructor(private service:CommonService) {  }

  ngOnInit(): void {
    this.service.getAllConnectedAccount().subscribe(
      (obj:any)=>{
        this.accountList = obj;
      }
    )
  }

  payToPlatform(){
    this.service.payToPlatform(this.amount,this.selectedAccount).subscribe(
      (obj:any)=>{
        this.notificationMessage.msg = obj.msg;
        this.notificationMessage.colorClass = (obj.status=='Success')?"bg-success":"bg-danger";
        this.notificationMessage.enable = true;
        setTimeout(()=>{
          this.notificationMessage.enable = false;
          this.notificationMessage.msg =""
          this.notificationMessage.colorClass = "";
        },3000);
        this.amount = 0;
        this.selectedAccount = ""
      },
      (error: any) => {
        console.error('Error occurred:', error);
        this.notificationMessage.msg = "400 "+error.message;
        this.notificationMessage.colorClass = "bg-danger";
        this.notificationMessage.enable = true;
        setTimeout(() => {
          this.notificationMessage.enable = false;
          this.notificationMessage.msg = "";
          this.notificationMessage.colorClass = "";
        }, 3000);
      }
    )
  }

}

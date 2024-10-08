import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  private readonly URL = "https://localhost:7121/api/Payment/"

  constructor(private httpClient:HttpClient) { }

  OnboardServiceProvider(fname:string,lname:string,email:string,referral:string){
    return this.httpClient.post(this.URL+'Onboard',{"FirstName":fname,"LastName":lname,"Email":email,"Referral":referral})
  }

  getAllConnectedAccount(){
    return this.httpClient.get(this.URL+'GetAllConnectedAccounts')
  }

  payToServiceProvider(amout:number,referral:string){
    return this.httpClient.post(this.URL+'PayToServiceProvider',{"Amount":amout,"Referral":referral})
  }

  payToPlatform(amout:number,referral:string){
    return this.httpClient.post(this.URL+'PayToPlatform',{"Amount":amout,"Referral":referral})
  }

}

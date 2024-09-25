import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { TrustPlatformComponent } from './trust-platform/trust-platform.component';
import { TrustServiceProviderComponent } from './trust-service-provider/trust-service-provider.component';

const routes: Routes = [
  {
    path:'',
    component:AppComponent
  },
  {
    path:'admin',
    component:AdminComponent
  },
  {
    path:'trust-platform',
    component:TrustPlatformComponent
  },
  {
    path:'trust-service-provider',
    component:TrustServiceProviderComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

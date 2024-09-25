import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminComponent } from './admin/admin.component';
import { TrustPlatformComponent } from './trust-platform/trust-platform.component';
import { TrustServiceProviderComponent } from './trust-service-provider/trust-service-provider.component';
import { CommonService } from './services/common.service';
import { HttpClientModule } from '@angular/common/http';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms'; // Required for two-way data binding


@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    TrustPlatformComponent,
    TrustServiceProviderComponent
  ],
  imports: [
    BrowserModule,
    NgSelectModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [CommonService],
  bootstrap: [AppComponent]
})
export class AppModule { }

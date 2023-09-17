import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { FormPageComponent } from './components/form-page/form-page.component';
import { InputComponent } from './components/input/input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonComponent } from './components/button/button.component';

@NgModule({
    declarations: [
        AppComponent,
        FormPageComponent,
        InputComponent,
        ButtonComponent,
    ],
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        ReactiveFormsModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
        }),
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}

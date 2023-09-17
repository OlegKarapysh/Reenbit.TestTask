import { Component } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CustomValidators } from 'src/app/validators/custom-validators';

@Component({
    selector: 'app-form-page',
    templateUrl: './form-page.component.html',
    styleUrls: ['./form-page.component.sass'],
})
export class FormPageComponent {
    public readonly docxExtension = '.docx';
    public form: FormGroup = new FormGroup({});
    public emailFormControl: FormControl;
    public fileFormControl: FormControl;

    private selectedFile: File | undefined;

    constructor(
        private formBuilder: FormBuilder,
        private notificationService: ToastrService
    ) {
        this.initForm();
        this.emailFormControl = this.form.controls['email'] as FormControl;
        this.fileFormControl = this.form.controls['docxFile'] as FormControl;
    }

    public submitForm(): void {
        console.log(this.form.value.email);
        console.log(this.selectedFile);
        this.notificationService.success(
            'Your file is successfully submitted! Check out your email for SAS token'
        );
    }

    public onFileChange(files: any): void {
        if (files) {
            this.selectedFile = files[0];
        }
    }

    private initForm(): void {
        this.form = this.formBuilder.group({
            email: [
                '',
                [
                    Validators.required,
                    Validators.minLength(5),
                    Validators.maxLength(75),
                    CustomValidators.emailMatch(),
                ],
            ],
            docxFile: [
                '',
                [
                    Validators.required,
                    CustomValidators.fileExtensionMatch(this.docxExtension),
                ],
            ],
        });
    }
}

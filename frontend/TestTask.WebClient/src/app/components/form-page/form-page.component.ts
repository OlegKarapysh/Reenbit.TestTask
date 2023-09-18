import { Component, OnDestroy } from '@angular/core';
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil, tap } from 'rxjs';
import { WebApiService } from 'src/app/services/web-api.service';
import { CustomValidators } from 'src/app/validators/custom-validators';

@Component({
    selector: 'app-form-page',
    templateUrl: './form-page.component.html',
    styleUrls: ['./form-page.component.sass'],
})
export class FormPageComponent implements OnDestroy {
    public readonly docxExtension = '.docx';
    public form: FormGroup = new FormGroup({});
    public emailFormControl: FormControl;
    public fileFormControl: FormControl;
    public isLoading = false;

    private selectedFile: File | undefined;
    private unsubscribe$ = new Subject<void>();

    constructor(
        private formBuilder: FormBuilder,
        private notificationService: ToastrService,
        private webApiService: WebApiService
    ) {
        this.initForm();
        this.emailFormControl = this.form.controls['email'] as FormControl;
        this.fileFormControl = this.form.controls['docxFile'] as FormControl;
    }

    ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    public submitForm(): void {
        if (!this.selectedFile) {
            return;
        }

        this.isLoading = true;
        this.notificationService.info('Your file is uploading...');
        this.webApiService
            .uploadForm(this.selectedFile, this.form.value.email)
            .pipe(
                takeUntil(this.unsubscribe$),
                tap(() => (this.isLoading = false))
            )
            .subscribe(
                () =>
                    this.notificationService.success(
                        'Your file is successfully submitted! Check out your email for SAS token'
                    ),
                (error) => this.notificationService.error(error.error)
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

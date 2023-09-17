import { AbstractControl, ValidatorFn } from '@angular/forms';

export class CustomValidators {
    static emailMatch(): ValidatorFn {
        return (control: AbstractControl) =>
            /^(?!\.)[A-Z0-9._-]+@(?!\.)[A-Z0-9.-]+\.[A-Z]+(?<!\.)$/i.test(
                control.value
            ) && !/[._-]{2,}/.test(control.value)
                ? null
                : { emailMatch: true };
    }

    static fileExtensionMatch(extension: string): ValidatorFn {
        return (control: AbstractControl) => {
            const fileName = control.value as string;
            const fileExtension = fileName.substring(
                fileName.lastIndexOf('.'),
                fileName.length
            );
            return fileExtension === extension
                ? null
                : { fileExtensionMatch: true };
        };
    }
}

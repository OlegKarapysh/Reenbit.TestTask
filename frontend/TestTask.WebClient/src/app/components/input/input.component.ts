import { Component, EventEmitter, Input, Output, Self } from '@angular/core';
import { NgControl, FormControl, ControlValueAccessor } from '@angular/forms';

@Component({
    selector: 'app-input',
    templateUrl: './input.component.html',
    styleUrls: ['./input.component.sass'],
})
export class InputComponent implements ControlValueAccessor {
    @Input() label = '';
    @Input() placeholder = '';
    @Input() type = 'text';
    @Input() accept = '';

    @Output() onChange = new EventEmitter<any>();

    constructor(@Self() public ngControl: NgControl) {
        this.ngControl.valueAccessor = this;
    }

    get control(): FormControl {
        return this.ngControl.control as FormControl;
    }

    public onInputChange($event: any) {
        this.onChange.emit($event);
    }

    registerOnChange(): void {}

    registerOnTouched(): void {}

    writeValue(): void {}
}

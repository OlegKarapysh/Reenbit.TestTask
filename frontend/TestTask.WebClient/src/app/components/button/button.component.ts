import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-button',
    templateUrl: './button.component.html',
    styleUrls: ['./button.component.sass'],
})
export class ButtonComponent {
    @Input() text = '';
    @Input() isDisabled = false;

    @Output() buttonOnClick: EventEmitter<void> = new EventEmitter<void>();

    public handleClick(): void {
        this.buttonOnClick.emit();
    }
}

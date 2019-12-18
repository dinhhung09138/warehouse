import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
    // tslint:disable-next-line: directive-selector
    selector: '[inputRestriction]'
})
export class InputRestrictionDirective {

    // tslint:disable-next-line: no-input-rename
    @Input('restrictionType') restrictionType: string; // number | decimal

    private regex = {
        number: new RegExp(/^\d+$/),
        decimal: new RegExp(/^[0-9]+(\.[0-9]*){0,1}$/g),
        numberAndLetter: new RegExp(/^[a-zA-Z0-9]+$/),
        numberAndLetterAndSpace: new RegExp(/^[a-zA-Z0-9\s]+$/),
        withoutSingleQuotes: new RegExp(/[^\']+$/g),
        phone: new RegExp(/^[0-9-]+$/g),
        letter: new RegExp(/^[a-zA-Z]+$/),
        numberAndLetterAndUnderscore: new RegExp(/^[a-zA-Z0-9_]+$/),
    };
    private specialKey = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Delete'];
    private specialKeyWithSpace = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Delete', 'Space'];
    private specialKeys = {
        number: this.specialKey,
        decimal: this.specialKey,
        numberAndLetter: this.specialKey,
        numberAndLetterAndSpace: this.specialKeyWithSpace,
        withoutSingleQuotes: this.specialKey,
        phone: this.specialKey,
        letter: this.specialKey,
        numberAndLetterAndUnderscore: this.specialKey,
    };

    constructor(private el: ElementRef) {
    }

    @HostListener('keydown', ['$event'])
    onKeyDown(event: KeyboardEvent) {
        if (this.specialKeys[this.restrictionType].indexOf(event.key) !== -1) {
            return;
        }
        const current: string = this.el.nativeElement.value;
        const next: string = current.concat(event.key);
        if (next && !String(next).match(this.regex[this.restrictionType])) {
            event.preventDefault();
        }
    }

}

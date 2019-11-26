import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogModel } from '../models/confirm-dialog.model';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationService {

  constructor(
    private modal: NgbModal
  ) { }

  public open(title?: string, message?: string) {

    const msg = new ConfirmDialogModel();

    if (title) {
      msg.title = title;
    } else {
      msg.title = 'Title';
    }

    if (message) {
      msg.message = message;
    } else {
      msg.message = 'Are you sure?';
    }

    const modalRef = this.modal.open(ConfirmDialogComponent, { centered: true, backdrop: 'static' });
    modalRef.componentInstance.message = msg;

    return modalRef.result;
  }
}

import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDialogModel } from 'src/app/core/models/confirm-dialog.model';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent implements OnInit {

  @Input() message: ConfirmDialogModel;

  constructor(
    public activeModal: NgbActiveModal
  ) { }

  ngOnInit() {
  }

  onConfirmNoButtonClick() {
    this.activeModal.close(false);
  }

  onConfirmYesButtonClick() {
    this.activeModal.close(true);
  }

}

import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'src/app/core/services/confirmation.service';
import { ShowMessageService } from 'src/app/core/services/show-message.service';
import { LoadingService } from 'src/app/core/services/loading.service';

@Component({
  selector: 'app-common-action',
  templateUrl: './common-action.component.html',
  styleUrls: ['./common-action.component.css']
})
export class CommonActionComponent implements OnInit {

  constructor(
    private confirmService: ConfirmationService,
    private showMessageService: ShowMessageService,
    private loading: LoadingService,
  ) { }

  ngOnInit() {
  }

  onOpenModalButtonClick() {
    this.confirmService.open('Confirm', 'Are you sure?').then(res => {
      if (res) {
        console.log('Select yes');
      } else {
        console.log('Select no');
      }
    });
  }

  onShowSuccessMessageButtonClick() {
    this.showMessageService.showSuccess('Message content');
  }

  onWarningMessageButtonClick() {
    this.showMessageService.showWarning('Message content');
  }

  onShowErrorMessageButtonClick() {
    this.showMessageService.showError('Message content');
  }
  onShowLoadingButtonClick(){
    this.loading.showLoading(true);
    setTimeout(() => {
      this.loading.showLoading(false);
    }, 2000);
  }
}

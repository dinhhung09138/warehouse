import { Injectable } from "@angular/core";
import { MessageService } from 'primeng/api';
import { SharedResource } from 'src/app/shared/shared.message';

@Injectable()
export class ShowMessageService {

  constructor(private messageService: MessageService) { }

  showSuccess(message: string) {
    this.messageService.add({severity: 'success', summary: SharedResource.messageTitle.info, detail: message, life: 5000});
  };

  showWarning(message: string) {
    this.messageService.add({severity: 'warning', summary: SharedResource.messageTitle.warning, detail: message, life: 7000});
  }

  showError(message: string) {
    this.messageService.add({severity: 'error', summary: SharedResource.messageTitle.error, detail: message, life: 10000});
  }
}

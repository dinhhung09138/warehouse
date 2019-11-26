import { Injectable } from "@angular/core";
import { MessageService } from 'primeng/api';

@Injectable()
export class ShowMessageService {

  constructor(private messageService: MessageService) { }

  showSuccess(title: string, message: string) {
    this.messageService.add({severity: 'success', summary: title, detail: message});
  }

  showWarning(title: string, message: string) {
    this.messageService.add({severity: 'warning', summary: title, detail: message});
  }

  showError(title: string, message: string) {
    this.messageService.add({severity: 'error', summary: title, detail: message});
  }

}

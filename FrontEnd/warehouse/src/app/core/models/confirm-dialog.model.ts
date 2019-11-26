export class ConfirmDialogModel {
  message?: string;
  title?: string;

  constructor(title?: string, message?: string) {
    this.title = title;
    this.message = message;
  }
}

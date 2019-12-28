import { BehaviorSubject } from 'rxjs'

export class ImageFileService {

  private imagePreview = new BehaviorSubject(null);

  changeImagePreview = this.imagePreview.asObservable();

  constructor() { }

  preview(file) {
    if (file) {
      const mineType = file.type;
      if (mineType.match(/image\/*/) === null) {
        this.imagePreview.next(null);
      }

      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (_) => {
        this.imagePreview.next(reader.result);
      };
    }
  }
}

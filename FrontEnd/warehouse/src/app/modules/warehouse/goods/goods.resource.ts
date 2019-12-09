import { SharedResource } from 'src/app/shared/shared.message';

export class GoodsResource {
  static readonly list = {
    title: 'Goods',
    table: {
      code: 'Code',
      name: 'Name',
      active: 'Active',
    },
    button: {

    },
    message: {
      nodata: SharedResource.table.nodata,
      deleteSuccess: SharedResource.formMessage.deleteSuccess,
    },
  };

  static readonly form = {
    title: 'Goods',
    form: {
      code: 'Code',
      name: 'Name',
      brand: 'Brand',
      color: 'Color',
      size: 'Size',
      unit: 'Unit',
      category: 'Category',
      active: 'Active',
    },
    message: {
      code: 'Code is required',
      name: 'Name is required',
      unit: 'Unit is required',
      category: 'Category is required',
      saveSuccess: SharedResource.formMessage.saveSuccess,
    },
  };
}

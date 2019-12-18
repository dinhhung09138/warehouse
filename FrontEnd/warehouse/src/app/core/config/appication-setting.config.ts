export class AppLicationSetting {

  /** Table page size */
  static readonly table = {
    pageSize: 20,
  };

  /** Modal option config */
  static readonly modalOptions = {
    modalFullScreenOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      size: 'full-screen' as any,
      centered: true,
      scrollable: false
    },
    modalExtraOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      size: 'xl' as any,
      centered: true,
      scrollable: false
    },
    modalLargeOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      size: 'lg' as any,
      centered: true,
      scrollable: false
    },
    modalMediumOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      size: 'md' as any,
      centered: true,
      scrollable: false
    },
    modalSmallOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      centered: true,
      scrollable: false,
      size: 'sm' as any
    },
    modalLoadingOptions: {
      backdrop: 'static' as any,
      ariaLabelledBy: 'modal-basic-title',
      centered: true,
      scrollable: false,
      size: 'loading' as any
    },
  };

  /** Datetime format */
  static readonly dateFormat = {
    day: 'MM/dd/yyyy',
    fullTime: 'MM/dd/yyy HH:mm:ss',
  };

  /* Date Type */
  static readonly dateType = {
    day: 'day',
    fullTime: 'fullTime',
  };

  /** Number format */
  static readonly numberFormat = {
    decimal: '.2-2',
    money: '.0',
  };

  /* Number Type */
  static readonly numberType = {
    decimal: 'decimal',
    money: 'money',
  };

  /** Calendar datetime config */
  static readonly timeZoneSetting = {
    firstDayOfWeek: 0,
    dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
    dayNamesShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
    dayNamesMin: ["Su","Mo","Tu","We","Th","Fr","Sa"],
    monthNames: [ "January","February","March","April","May","June","July","August","September","October","November","December" ],
    monthNamesShort: [ "Jan", "Feb", "Mar", "Apr", "May", "Jun","Jul", "Aug", "Sep", "Oct", "Nov", "Dec" ],
    today: 'Today',
    clear: 'Clear',
    dateFormat: AppLicationSetting.dateFormat.day,
    weekHeader: 'Wk'
  };
}

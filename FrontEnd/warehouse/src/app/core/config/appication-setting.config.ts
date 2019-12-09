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
}

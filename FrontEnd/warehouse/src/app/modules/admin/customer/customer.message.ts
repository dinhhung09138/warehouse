export class CustomerResource {
  static readonly form = {
    title: 'Customer',
    form: {
      userName: 'User Name',
      password: 'Password',
      rememberMe: 'Remember me',
      button: {
        login: 'login',
      },
      validation: {
        requiredName: 'User Name is required',
        requiredPassword: 'Password is required',
      }
    },
  };

  static readonly list = {
    title: 'Customer',
  };
}

export class LoginResource {
  static readonly loginForm = {
    title: 'Login',
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
    companyInfo: {
      name: 'Company info',
    },
  };
}

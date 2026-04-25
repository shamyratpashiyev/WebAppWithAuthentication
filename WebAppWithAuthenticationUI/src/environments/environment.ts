export const environment = {
  production: true,
  baseUrl: 'http://webappwithauthentication.runasp.net',
  refreshTokenPath: 'refresh-token',
  emailConfirmation: {
    userIdQueryString: 'userId',
    tokenQueryString: 'token',
  },
  passwordReset: {
    userIdQueryString: 'userId',
    tokenQueryString: 'token',
  }
};

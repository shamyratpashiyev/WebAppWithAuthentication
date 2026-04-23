export const environment = {
  production: false,
  baseUrl: 'http://localhost:5035',
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

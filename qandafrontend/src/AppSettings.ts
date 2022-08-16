export const server = 'http://localhost:17525';

export const webAPIUrl = `${server}/api`;

export const authSettings = {
  domain: 'dev-ehqqmkp1.auth0.com',
  client_id: 'Pf5owdhL1lSI9J4x30Ajc0QIIScuFKEC',
  redirect_uri: window.location.origin + '/signin-callback',
  scope: 'openid profile QandAAPI email',
  audience: 'https://qanda',
};

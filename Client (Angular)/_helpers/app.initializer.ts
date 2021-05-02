import {AuthService} from '../auth/auth.service';

export function appInitializer(authService: AuthService) {
  return () => new Promise(resolve => {

    authService.initializeUser().then(() => {
      resolve(true);
    });
  });
}

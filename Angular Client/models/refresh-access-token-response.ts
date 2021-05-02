import {AccessToken} from './access-token';
import {User} from './user';

export interface RefreshAccessTokenResponse {
  accessToken: AccessToken;
  user: User;
}




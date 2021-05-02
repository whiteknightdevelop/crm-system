import {User, UserEntity} from './user';
import {AccessToken, AccessTokenEntity} from './access-token';
import {RefreshToken, RefreshTokenEntity} from './refresh-token';

export interface LoginResponse {
  accessToken: AccessToken;
  refreshToken: RefreshToken;
  user: User;
}

export class LoginResponseEntity implements LoginResponse{
  constructor() {
    this.accessToken = new AccessTokenEntity();
    this.refreshToken = new RefreshTokenEntity();
    this.user = new UserEntity();
  }
  accessToken: AccessToken;
  refreshToken: RefreshToken;
  user: User;
}


